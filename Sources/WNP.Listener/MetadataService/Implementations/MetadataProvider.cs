// <copyright file="MetadataProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Implementations
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AsyncPoco;
    using Microsoft.CSharp;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Utilities;

    /// <summary>
    /// Implements <see cref="IMetadataProvider"/>
    /// </summary>
    public class MetadataProvider : IMetadataProvider
    {
        private static Assembly odataModelAssembly;
        private static Dictionary<string, MetadataEntityModel> oDataModelMappings;

        private readonly WNPDBContext dbContext;
        private readonly IActionConfigurator actionConfigurator;
        private readonly IEntityConfigurator entityConfigurator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataProvider"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="entityConfigurator">The entity configurator.</param>
        public MetadataProvider(WNPDBContext dbContext, IActionConfigurator actionConfigurator, IEntityConfigurator entityConfigurator)
        {
            this.dbContext = dbContext;
            this.actionConfigurator = actionConfigurator;
            this.entityConfigurator = entityConfigurator;

            if (oDataModelMappings == null)
            {
                Task.Run(() => this.PrepareModelAsync()).Wait();
            }

            if (odataModelAssembly == null)
            {
                this.GenerateODataAssembly();
            }
        }

        /// <inheritdoc/>
        public string ODataModelNamespace => "AMSLLC.Listener.Generated";

        /// <inheritdoc/>
        public Assembly ODataModelAssembly => odataModelAssembly;

        /// <inheritdoc/>
        public Type GetEntityType(string edmEntityType) =>
            this.ODataModelAssembly.GetType(edmEntityType);

        /// <inheritdoc/>
        public Type GetEntityTypeBySetName(string edmEntityCollectionType)
        {
            if (string.IsNullOrWhiteSpace(edmEntityCollectionType) || edmEntityCollectionType.Length < 1)
            {
                return null;
            }

            return this.ODataModelAssembly.GetType(edmEntityCollectionType.Substring(0, edmEntityCollectionType.Length - 1));
        }

        /// <inheritdoc/>
        public MetadataEntityModel GetModelMapping(string clrEntityType)
        {
            string fullClrType = StringUtilities.Invariant($"{this.ODataModelNamespace}.{clrEntityType}");
            if (oDataModelMappings.ContainsKey(fullClrType))
            {
                return oDataModelMappings[fullClrType];
            }

            return null;
        }

        /// <inheritdoc/>
        public MetadataEntityModel GetModelMapping(Type clrEntityType)
        {
            if (clrEntityType == null)
            {
                return null;
            }

            return oDataModelMappings[clrEntityType.FullName];
        }

        /// <inheritdoc/>
        public MetadataEntityModel GetModelMappingByTableName(string tableName) =>
            oDataModelMappings.First(modelMappings => modelMappings.Value.TableName == tableName).Value;

        private static void AddVirtualRelation(VirtualRelationInformation relation, MetadataEntityModel model, CodeTypeDeclaration virtualEntityClass)
        {
            foreach (var columnName in relation.ColumnList)
            {
                var modelFieldName = model.ColumnToModelMappings[columnName.ToUpperInvariant()];
                var field = model.FieldInfo[modelFieldName];
                var nullable = string.Empty;
                if (field.DataType == "int"
                        || field.DataType == "DateTimeOffset"
                        || field.DataType == "decimal"
                        || field.DataType == "float"
                        || field.DataType == "char")
                {
                    nullable = "?";
                }

                var key = relation.Discriminator.Contains(columnName) ? "[Key] " : string.Empty;

                var property = new CodeSnippetTypeMember
                {
                    Text = StringUtilities.Invariant($"{key}public {field.DataType}{nullable} {modelFieldName} {{get; set;}}")
                };

                virtualEntityClass.Members.Add(property);
            }
        }

        private static void AddRelations(EntityConfiguration entityConfig, CodeTypeDeclaration codeClass)
        {
            foreach (var relation in entityConfig.Relations)
            {
                var targetEntity =
                    oDataModelMappings.Values.FirstOrDefault(
                        model => model.TableName == relation.TargetTableName);

                if (targetEntity == null)
                {
                    throw new ArgumentException(
                        StringUtilities.Invariant($"Target class for required relation {relation.TargetTableName} not found"));
                }

                var containmentAttribute = string.Empty;
                if (relation.IsContained)
                {
                    containmentAttribute = "[Contained]";
                }

                var fieldEnding = targetEntity.ClassName.EndsWith("s", StringComparison.InvariantCultureIgnoreCase) ? string.Empty : "s";
                var property = new CodeSnippetTypeMember
                {
                    Text =
                        relation.RelationType == RelationType.OneToMany
                            ? StringUtilities.Invariant(
                                $"{containmentAttribute} public List<{targetEntity.ClassName}> {targetEntity.ClassName}{fieldEnding} {{get; set;}}")
                            : StringUtilities.Invariant(
                                $"public {targetEntity.ClassName} {targetEntity.ClassName} {{get; set;}}")
                };

                codeClass.Members.Add(property);
            }
        }

        private async Task PrepareModelAsync()
        {
            oDataModelMappings = new Dictionary<string, MetadataEntityModel>();

            var rawMetadata = await this.dbContext.FetchAsync<WNPMetadataEntry>(Sql.Builder.Select(DBMetadata.ALL).From(DBMetadata.Metadata));
            foreach (var metadataEntry in rawMetadata)
            {
                metadataEntry.TableName = metadataEntry.TableName.ToUpperInvariant();
                metadataEntry.ColumnName = metadataEntry.ColumnName.ToUpperInvariant();
            }

            // only tables defined in metadata with INFO as column name and IsUsed flag will be exposed in oData
            var tables = rawMetadata.Where(metadata => metadata.ColumnName == "INFO" && metadata.IsUsed == "Y");

            foreach (var table in tables)
            {
                var modelToColumnMappings = new Dictionary<string, string>();
                var columnToModelMappings = new Dictionary<string, string>();
                var fieldsInfo = new Dictionary<string, MetadataFieldInfo>();

                var modelClassName = table.CustomerLabel.Replace(" ", string.Empty);
                var tableName = table.TableName.ToUpperInvariant();

                foreach (var column in rawMetadata.Where(metadata => metadata.TableName == tableName && metadata.ColumnName != "INFO" && metadata.IsUsed == "Y"))
                {
                    // removing spaces from customer label definition
                    var customerLabel = column.CustomerLabel.Replace(" ", string.Empty);

                    // resolve data type from database
                    var dataType = column.DataType;
                    if (dataType.Equals("DateTime", StringComparison.OrdinalIgnoreCase)
                        || dataType.Equals("date", StringComparison.OrdinalIgnoreCase))
                    {
                        dataType = "DateTimeOffset";
                    }
                    else if (dataType.Equals("varchar", StringComparison.OrdinalIgnoreCase)
                        || dataType.Equals("nvarchar", StringComparison.OrdinalIgnoreCase))
                    {
                        dataType = "string";
                    }

                    // resolve if it's primary key from database
                    var isPrimary = column.IsPrimaryKey.Equals("Y", StringComparison.OrdinalIgnoreCase);

                    var fieldInfo = new MetadataFieldInfo()
                    {
                        DataType = dataType,
                        ClrDataType = Converters.ConvertStringToType(dataType),
                        IsPrimaryKey = isPrimary
                    };

                    fieldsInfo.Add(customerLabel, fieldInfo);
                    modelToColumnMappings.Add(customerLabel, column.ColumnName);
                    columnToModelMappings.Add(column.ColumnName, customerLabel);
                }

                var fqTableName = StringUtilities.Invariant($"wndba.{tableName}").ToUpperInvariant();

                var actionsContainer =
                    this.actionConfigurator.GetEntityActionContainer(
                        fqTableName);

                Func<KeyValuePair<string, MetadataFieldInfo>, bool> predicate =
                    fi => fi.Value.IsPrimaryKey && modelToColumnMappings[fi.Key].ToUpperInvariant() != "OWNER";

                var entityConfiguration = this.entityConfigurator.GetEntityConfiguration(fqTableName) ?? new EntityConfiguration(fqTableName);
                entityConfiguration.HasKey(fieldsInfo.Where(predicate).Select(fi => modelToColumnMappings[fi.Key]).ToArray());

                var oDataModelMapping = new MetadataEntityModel(
                    fqTableName,
                    modelClassName,
                    modelToColumnMappings,
                    columnToModelMappings,
                    fieldsInfo,
                    actionsContainer,
                    entityConfiguration);

                oDataModelMappings.Add(
                    StringUtilities.Invariant($"{this.ODataModelNamespace}.{modelClassName}"),
                    oDataModelMapping);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.CodeDom.CodeSnippetTypeMember.set_Text(System.String)", Justification = "Strings are used for code generation and should not be localized.")]
        private void GenerateODataAssembly()
        {
            var codeUnit = new CodeCompileUnit();

            var codeNamespace = new CodeNamespace(this.ODataModelNamespace);

            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel.DataAnnotations"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.OData"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.OData.Builder"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("AMSLLC.Listener.Utilities"));

            // TODO: we should move the IODataEntity marker interface to another library, think about this
            // Do we really need marker interface? All types in generated assembly will be ODataEntity types.
            codeNamespace.Imports.Add(new CodeNamespaceImport("AMSLLC.Listener.MetadataService"));

            // TODO: we should remove dependence on Persistence assemblies.
            codeNamespace.Imports.Add(new CodeNamespaceImport("AMSLLC.Listener.Persistence.WNP"));

            // only tables defined in metadata with INFO as column name and IsUsed flag will be exposed in oData
            foreach (var table in oDataModelMappings.Values)
            {
                // if there is a redefine in Metadata, use CustomerLabel inst
                var modelClassName = table.ClassName;

                var entityConfig = table.EntityConfiguration;

                var codeClass = new CodeTypeDeclaration(modelClassName)
                {
                    IsClass = true,
                    TypeAttributes = TypeAttributes.Public,
                    BaseTypes = { typeof(IODataEntity) }
                };

                if (entityConfig.IsContained)
                {
                    codeClass.BaseTypes.Add(typeof(IContainedEntity));
                }

                codeNamespace.Types.Add(codeClass);

                var entityName = WnpDbHelpers.HumanizeTable(table.TableName);

                var getEntityMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public,
                    Name = "GetEntity",
                    ReturnType = new CodeTypeReference(entityName)
                };

                getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($"var result = new {entityName}();")));

                var getEntityDeltaMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public,
                    Name = "GetEntityDelta"
                };

                getEntityDeltaMethod.Parameters.Add(new CodeParameterDeclarationExpression(StringUtilities.Invariant($"Delta<{modelClassName}>"), "edmDelta"));
                getEntityDeltaMethod.ReturnType = new CodeTypeReference(StringUtilities.Invariant($"Delta<{entityName}>"));
                getEntityDeltaMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($"var result = new Delta<{entityName}>();")));
                getEntityDeltaMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($"var changedProperties = edmDelta.GetChangedPropertyNames();")));

                var setFromEntityMethod = new CodeMemberMethod
                {
                    Attributes = MemberAttributes.Public,
                    Name = "SetFromEntity",
                    ReturnType = new CodeTypeReference(typeof(void))
                };

                setFromEntityMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(entityName), "entity"));

                // Add ordinary relations
                AddRelations(entityConfig, codeClass);

                // Add virtual relations
                var fieldsToSkip = new List<string>();
                foreach (var relation in table.EntityConfiguration.VirtualRelations)
                {
                    var virtualEntityClass = new CodeTypeDeclaration(relation.VirtualEntityName)
                    {
                        IsClass = true,
                        TypeAttributes = TypeAttributes.Public,
                        BaseTypes = { typeof(IVirtualODataEntity) }
                    };

                    AddVirtualRelation(relation, table, virtualEntityClass);

                    var property = new CodeSnippetTypeMember
                                       {
                                           Text =
                                               relation.RelationType == RelationType.OneToMany
                                                   ? StringUtilities.Invariant(
                                                       $"[Contained] public List<{relation.VirtualEntityName}> {relation.VirtualEntityName}s {{get; set;}}")
                                                   : StringUtilities.Invariant(
                                                       $"[Contained] public {relation.VirtualEntityName} {relation.VirtualEntityName} {{get; set;}}")
                                       };

                    codeNamespace.Types.Add(virtualEntityClass);
                    codeClass.Members.Add(property);

                    fieldsToSkip.AddRange(relation.ColumnList.Select(s => table.ColumnToModelMappings[s.ToUpperInvariant()]));
                }

                // Add regular fields
                foreach (var field in table.FieldInfo)
                {
                    if (fieldsToSkip.Contains(field.Key))
                    {
                        continue;
                    }

                    var property = new CodeSnippetTypeMember();

                    // we don't want to expose Owner as OData key, so if this is part of
                    // composite key we should handle this inside the controller
                    var originalColumnName = table.ModelToColumnMappings[field.Key];
                    var isOwnerColumn = originalColumnName.ToUpperInvariant().Equals("OWNER");

                    if (entityConfig.Key.Contains(table.ModelToColumnMappings[field.Key], StringComparer.OrdinalIgnoreCase) && !isOwnerColumn)
                    {
                        property.Text = "[Key]";
                    }

                    var nullable = string.Empty;
                    if (field.Value.DataType == "int"
                        || field.Value.DataType == "DateTimeOffset"
                        || field.Value.DataType == "decimal"
                        || field.Value.DataType == "float"
                        || field.Value.DataType == "char")
                    {
                        nullable = "?";
                    }

                    property.Text += StringUtilities.Invariant($" public {field.Value.DataType}{nullable} {field.Key} {{ get; set; }}");

                    codeClass.Members.Add(property);

                    var fieldName = WnpDbHelpers.HumanizeField(table.ModelToColumnMappings[field.Key]);

                    if (field.Value.DataType == "DateTimeOffset")
                    {
                        getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" if ({field.Key}.HasValue) result.{fieldName} = new DateTime({field.Key}.Value.ToLocalTime().Ticks);")));
                    }
                    else if (field.Value.DataType == "char")
                    {
                        getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" result.{fieldName} = {field.Key}.ToString();")));
                    }
                    else
                    {
                        getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" result.{fieldName} = {field.Key};")));
                    }

                    getEntityDeltaMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($@"
                        if (changedProperties.Contains(""{field.Key}""))
                            {{
                                object value;
                                edmDelta.TryGetPropertyValue(""{field.Key}"", out value);
                                result.TrySetPropertyValue(""{fieldName}"", value);
                            }}
                    ")));

                    setFromEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" this.{field.Key} = ({field.Value.DataType}{nullable})Converters.Convert(entity.{fieldName}, typeof({field.Value.DataType}{nullable}));")));
                }

                getEntityMethod.Statements.Add(new CodeSnippetStatement("return result;"));
                getEntityDeltaMethod.Statements.Add(new CodeSnippetStatement("return result;"));

                codeClass.Members.Add(getEntityMethod);
                codeClass.Members.Add(getEntityDeltaMethod);
                codeClass.Members.Add(setFromEntityMethod);
            }

            codeUnit.Namespaces.Add(codeNamespace);

            using (var codeProvider = new CSharpCodeProvider())
            {
                var compilerParameters = new CompilerParameters
                {
                    GenerateExecutable = false,
                    GenerateInMemory = true,
                };

                using (var codeStringWriter = new StringWriter(CultureInfo.InvariantCulture))
                {
                    var codeGenerator = codeProvider.CreateGenerator(codeStringWriter);

                    codeGenerator.GenerateCodeFromCompileUnit(codeUnit, codeStringWriter, new CodeGeneratorOptions());

                    Console.Write(codeStringWriter.GetStringBuilder().ToString());
                }

                compilerParameters.ReferencedAssemblies.Add("System.dll");
                compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
                compilerParameters.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
                compilerParameters.ReferencedAssemblies.Add("System.Web.OData.dll");
                compilerParameters.ReferencedAssemblies.Add("AMSLLC.Listener.MetadataService.dll");
                compilerParameters.ReferencedAssemblies.Add("AMSLLC.Listener.Persistence.WNP.dll");
                compilerParameters.ReferencedAssemblies.Add("AMSLLC.Listener.Utilities.dll");
                var result = codeProvider.CompileAssemblyFromDom(compilerParameters, codeUnit);

                if (result.Errors.Count > 0)
                {
                    throw new InvalidProgramException(result.Errors.ToString());
                }

                odataModelAssembly = result.CompiledAssembly;
            }
        }
    }
}