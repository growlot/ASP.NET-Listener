using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using AMSLLC.Listener.MetadataService.Model;
using AMSLLC.Listener.Persistence;
using AMSLLC.Listener.Persistence.Metadata;
using AMSLLC.Listener.Utilities;
using Microsoft.CSharp;

namespace AMSLLC.Listener.MetadataService.Impl
{
    public class MetadataServiceImpl : IMetadataService
    {
        private readonly MetadataDbContext dbContext;
        private readonly IActionConfigurator _actionConfigurator;

        private static Assembly odataModelAssembly;
        private static Dictionary<string, MetadataModel> oDataModelMappings;

        public string ODataModelNamespace => "Listener"; //AMSLLC.Listener.ODataService.ODataModel

        public Assembly ODataModelAssembly => odataModelAssembly;

        public Type GetEntityType(string typeName) =>
            this.ODataModelAssembly.GetType(typeName);

        public Type GetEntityTypeBySetName(string entitySetName) =>
            this.ODataModelAssembly.GetType(entitySetName.Substring(0, entitySetName.Length - 1));

        public MetadataModel GetModelMapping(string clrModelName)
        {
            string fullClrType = $"{this.ODataModelNamespace}.{clrModelName}";
            if (oDataModelMappings.ContainsKey(fullClrType))
            {
                return oDataModelMappings[fullClrType];
            }

            return null;
        }

        public MetadataModel GetModelMapping(Type clrModel) =>
            oDataModelMappings[clrModel.FullName];

        public MetadataModel GetModelMappingByTableName(string tableName) =>
            oDataModelMappings.First(modelMappings => modelMappings.Value.TableName == tableName).Value;

        public MetadataServiceImpl(MetadataDbContext dbContext, IActionConfigurator actionConfigurator)
        {
            this.dbContext = dbContext;
            this._actionConfigurator = actionConfigurator;

            if (oDataModelMappings == null)
                this.PrepareModel();

            if (odataModelAssembly == null)
                this.GenerateODataAssembly();
        }

        private void PrepareModel()
        {
            oDataModelMappings = new Dictionary<string, MetadataModel>();

            var RawMetadata = this.dbContext.Fetch<WNPMetadataEntry>(Sql.Builder.Select(DBMetadata.ALL).From(DBMetadata.Metadata));
            foreach (var metadataEntry in RawMetadata)
            {
                metadataEntry.TableName = metadataEntry.TableName.ToLowerInvariant();
                metadataEntry.ColumnName = metadataEntry.ColumnName.ToLowerInvariant();
            }

            // only tables defined in metadata with INFO as column name and IsUsed flag will be exposed in oData
            var tables = RawMetadata.Where(metadata => metadata.ColumnName == "info" && metadata.IsUsed == "Y");

            foreach (var table in tables)
            {
                var modelToColumnMappings = new Dictionary<string, string>();
                var columnToModelMappings = new Dictionary<string, string>();
                var fieldsInfo = new Dictionary<string, MetadataFieldInfo>();

                var modelClassName = table.CustomerLabel.Replace(" ", string.Empty);
                var tableName = table.TableName.ToLowerInvariant();

                foreach (var column in RawMetadata.Where(metadata => metadata.TableName == tableName && metadata.ColumnName != "info" && metadata.IsUsed == "Y"))
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
                if (this._actionConfigurator.IsEntityActionsContainerAvailable($"wndba.{tableName}"))
                    actionsContainer = this._actionConfigurator.GetEntityActionContainer($"wndba.{tableName}");

                var oDataModelMapping = new MetadataModel($"wndba.{tableName}", modelClassName, modelToColumnMappings, columnToModelMappings, fieldsInfo, actionsContainer);
                oDataModelMappings.Add($"{this.ODataModelNamespace}.{modelClassName}", oDataModelMapping);
            }
        }

        private void GenerateODataAssembly()
        {
            var codeUnit = new CodeCompileUnit();

            var codeNamespace = new CodeNamespace(this.ODataModelNamespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel.DataAnnotations"));

            // TODO: we should move the IODataEntity marker interface to another library, think about this
            // Do we really need marker interface? All types in generated assembly will be ODataEntity types.
            codeNamespace.Imports.Add(new CodeNamespaceImport("AMSLLC.Listener.MetadataService"));

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

                foreach (var field in table.FieldInfo)
                {
                    var property = new CodeSnippetTypeMember();
                    if (field.Value.IsPrimaryKey)
                        property.Text = "[Key]";

                    property.Text += $" public {field.Value.DataType} {field.Key} {{ get; set; }}";

                    codeClass.Members.Add(property);
                }
            }

            codeUnit.Namespaces.Add(codeNamespace);

            var codeProvider = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
            };

            var codeStringWriter = new StringWriter();
            var codeGenerator = codeProvider.CreateGenerator(codeStringWriter);

            codeGenerator.GenerateCodeFromCompileUnit(codeUnit, codeStringWriter, new CodeGeneratorOptions());

            Console.Write(codeStringWriter.GetStringBuilder().ToString());

            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
            compilerParameters.ReferencedAssemblies.Add("AMSLLC.Listener.MetadataService.dll");
            var result = codeProvider.CompileAssemblyFromDom(compilerParameters, codeUnit);

            if (result.Errors.Count > 0)
                throw new Exception();

            odataModelAssembly = result.CompiledAssembly;
        }
    }
}