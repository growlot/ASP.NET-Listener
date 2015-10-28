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
    using Microsoft.CSharp;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Utilities;

    /// <summary>
    /// Implmeents <see cref="IMetadataProvider"/>
    /// </summary>
    public class MetadataProvider : IMetadataProvider
    {
        private static Assembly odataModelAssembly;
        private static Dictionary<string, MetadataEntityModel> oDataModelMappings;

        private readonly WNPDBContext dbContext;
        private readonly IActionConfigurator actionConfigurator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataProvider"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        public MetadataProvider(WNPDBContext dbContext, IActionConfigurator actionConfigurator)
        {
            this.dbContext = dbContext;
            this.actionConfigurator = actionConfigurator;

            if (oDataModelMappings == null)
            {
                this.PrepareModel();
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

        private void PrepareModel()
        {
            oDataModelMappings = new Dictionary<string, MetadataEntityModel>();

            var rawMetadata = this.dbContext.Fetch<WNPMetadataEntry>(Sql.Builder.Select(DBMetadata.ALL).From(DBMetadata.Metadata));
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
                    if (dataType.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                    {
                        dataType = "DateTimeOffset";
                    }
                    else if (dataType.Equals("varchar", StringComparison.OrdinalIgnoreCase))
                    {
                        dataType = "string";
                    }

                    // resolve if it's primary key from database
                    var isPrimary = column.IsPrimaryKey.Equals("Y", StringComparison.OrdinalIgnoreCase);

                    var fieldInfo = new MetadataFieldInfo()
                    {
                        DataType = dataType,
                        IsPrimaryKey = isPrimary
                    };

                    fieldsInfo.Add(customerLabel, fieldInfo);
                    modelToColumnMappings.Add(customerLabel, column.ColumnName);
                    columnToModelMappings.Add(column.ColumnName, customerLabel);
                }

                Type actionsContainer = null;
                if (this.actionConfigurator.IsEntityActionsContainerAvailable(StringUtilities.Invariant($"wndba.{tableName}").ToUpperInvariant()))
                {
                     actionsContainer = this.actionConfigurator.GetEntityActionContainer(StringUtilities.Invariant($"wndba.{tableName}").ToUpperInvariant());
                }

                var oDataModelMapping = new MetadataEntityModel(StringUtilities.Invariant($"wndba.{tableName}").ToUpperInvariant(), modelClassName, modelToColumnMappings, columnToModelMappings, fieldsInfo, actionsContainer);
                oDataModelMappings.Add(StringUtilities.Invariant($"{this.ODataModelNamespace}.{modelClassName}"), oDataModelMapping);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.CodeDom.CodeSnippetTypeMember.set_Text(System.String)", Justification = "Strings are used for code generation and should not be localized.")]
        private void GenerateODataAssembly()
        {
            var codeUnit = new CodeCompileUnit();

            var codeNamespace = new CodeNamespace(this.ODataModelNamespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel.DataAnnotations"));
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

                var codeClass = new CodeTypeDeclaration(modelClassName)
                {
                    IsClass = true,
                    TypeAttributes = TypeAttributes.Public,
                    BaseTypes = { typeof(IODataEntity) }
                };

                codeNamespace.Types.Add(codeClass);

                var entityName = WNPDBHelpers.HumanizeTable(table.TableName);

                var getEntityMethod = new CodeMemberMethod();
                getEntityMethod.Attributes = MemberAttributes.Public;
                getEntityMethod.Name = "GetEntity";
                getEntityMethod.ReturnType = new CodeTypeReference(entityName);
                getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($"var result = new {entityName}();")));

                var setFromEntityMethod = new CodeMemberMethod();
                setFromEntityMethod.Attributes = MemberAttributes.Public;
                setFromEntityMethod.Name = "SetFromEntity";
                setFromEntityMethod.ReturnType = new CodeTypeReference(typeof(void));
                setFromEntityMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(entityName), "entity"));

                foreach (var field in table.FieldInfo)
                {
                    var property = new CodeSnippetTypeMember();
                    if (field.Value.IsPrimaryKey)
                    {
                        property.Text = "[Key]";
                    }

                    var nullable = string.Empty;
                    if (field.Value.DataType == "int")
                    {
                        nullable = "?";
                    }

                    property.Text += StringUtilities.Invariant($" public {field.Value.DataType}{nullable} {field.Key} {{ get; set; }}");

                    codeClass.Members.Add(property);

                    var fieldName = WNPDBHelpers.HumanizeField(table.ModelToColumnMappings[field.Key]);

                    if (field.Value.DataType == "DateTimeOffset")
                    {
                        getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" result.{fieldName} = new DateTime({field.Key}.ToLocalTime().Ticks);")));
                    }
                    else if (field.Value.DataType == "char")
                    {
                        getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" result.{fieldName} = {field.Key}.ToString();")));
                    }
                    else
                    {
                        getEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" result.{fieldName} = {field.Key};")));
                    }

                    setFromEntityMethod.Statements.Add(new CodeSnippetStatement(StringUtilities.Invariant($" this.{field.Key} = ({field.Value.DataType})Converters.Convert(entity.{fieldName}, typeof({field.Value.DataType}));")));
                }

                getEntityMethod.Statements.Add(new CodeSnippetStatement("return result;"));

                codeClass.Members.Add(getEntityMethod);
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
                compilerParameters.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
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