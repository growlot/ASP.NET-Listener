using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using Microsoft.CSharp;
using WNP.Listener.MetadataService.Model;
using static DBMetadata;
using static PetaPoco.Sql;

namespace WNP.Listener.MetadataService.Impl
{
    using Utilities;

    public class MetadataServiceImpl : IMetadataService
    {
        private readonly IODataEntityConfiguration _entityConfiguration;
        private readonly MetadataDbContext _dbContext;
        private static Assembly _odataModelAssembly;

        private static Dictionary<string, ODataModelMapping> _oDataModelMappings =
            new Dictionary<string, ODataModelMapping>();

        public Dictionary<string, ODataModelMapping> ODataModelMappings => _oDataModelMappings;

        public string ODataModelNamespace => "WNP.Listener.ODataService.ODataModel";

        public List<WNPMetadataEntry> RawMetadata =>
                MemoryCache.Default.GetOrAddExisting("_RawMetadata", 
                    () => _dbContext.Fetch<WNPMetadataEntry>(Builder.Select(ALL).From(Metadata)), 
                    DateTime.Now.AddMinutes(1));

        public Assembly ODataModelAssembly => _odataModelAssembly ?? (_odataModelAssembly = GenerateODataAssembly());

        public MetadataServiceImpl(MetadataDbContext dbContext, IODataEntityConfiguration entityConfiguration)
        {
            _dbContext = dbContext;
            _entityConfiguration = entityConfiguration;
        }

        public class ODataToDatabaseColumnInfo
        {
            public string DatabaseColumnName { get; set; }
        }

        public class ODataModelMapping
        {
            public Dictionary<string, ODataToDatabaseColumnInfo> Mappings { get; }
            public Dictionary<string, string> ReverseMappings { get; }
            public string TableName { get; }

            public ODataModelMapping(string tableName, Dictionary<string, ODataToDatabaseColumnInfo> mappings, Dictionary<string, string> reverseMappings)
            {
                Mappings = mappings;
                ReverseMappings = reverseMappings;
                TableName = tableName;
            }
        }

        private Assembly GenerateODataAssembly()
        {
            var codeUnit = new CodeCompileUnit();

            //PetaPoco.Mappers.GetMapper(typeof(WNPMetadata)).GetColumnInfo(typeof(WNPMetadata).GetProperty("blabla")).

            var codeNamespace = new CodeNamespace(ODataModelNamespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel.DataAnnotations"));

            // TODO: we should move the IODataEntity marker interface to another library, think about this
            codeNamespace.Imports.Add(new CodeNamespaceImport("WNP.Listener.MetadataService"));

            //"WNP.Listener.ODataService.ODataModel.ElectricMeter" => { "Id" =>}            

            foreach (var tableName in _entityConfiguration.Keys)
            {
                var mappingInfo = new Dictionary<string, ODataToDatabaseColumnInfo>();
                var reverseMappingInfo = new Dictionary<string, string>();

                // default name for entity is configured in IODataEntityConfiguration
                var modelClassName = _entityConfiguration[tableName].DefaultEntityName;
                var metadataEntry = RawMetadata.FirstOrDefault(metadata => metadata.ColumnName == "INFO");

                // if there is a redefine in Metadata, use CustomerLabel inst
                if (metadataEntry != null)
                    modelClassName = metadataEntry.CustomerLabel;

                var codeClass = new CodeTypeDeclaration(modelClassName)
                {
                    IsClass = true,
                    TypeAttributes = TypeAttributes.Public,
                    BaseTypes = { typeof(IODataEntity) }
                };

                codeNamespace.Types.Add(codeClass);

                foreach (var columnReadableName in TableLookup[tableName].ColumnsLookup.Keys)
                {
                    var property = new CodeSnippetTypeMember();
                    var columnInfo = TableLookup[tableName].ColumnsLookup[columnReadableName];
                    var metadataInfo =
                        RawMetadata.FirstOrDefault(
                            metadata =>
                                metadata.TableName.ToLowerInvariant() == tableName &&
                                string.Equals(metadata.ColumnName, columnInfo.ColumnName, StringComparison.InvariantCultureIgnoreCase) &&
                                metadata.IsUsed == "Y");

                    if (metadataInfo != null)
                    {
                        if (metadataInfo.IsPrimaryKey == "Y")
                            property.Text = "[Key]";

                        property.Text += $" public {metadataInfo.DataType} {metadataInfo.CustomerLabel} {{ get; set; }}";
                        mappingInfo.Add(metadataInfo.CustomerLabel, new ODataToDatabaseColumnInfo() {DatabaseColumnName = columnInfo.ColumnName});
                        reverseMappingInfo.Add(columnInfo.ColumnName, metadataInfo.CustomerLabel);
                    }
                    else
                    {
                        property.Text = $" public {columnInfo.DataType} {columnInfo.ModelName} {{ get; set; }}";
                        mappingInfo.Add(columnInfo.ModelName, new ODataToDatabaseColumnInfo() { DatabaseColumnName = columnInfo.ColumnName });
                        reverseMappingInfo.Add(columnInfo.ColumnName, columnInfo.ModelName);
                    }

                    codeClass.Members.Add(property);
                }

                ODataModelMappings.Add($"{ODataModelNamespace}.{modelClassName}",
                    new ODataModelMapping(TableLookup[tableName].ToString(), mappingInfo, reverseMappingInfo));
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
            compilerParameters.ReferencedAssemblies.Add("WNP.Listener.MetadataService.dll");
            var result = codeProvider.CompileAssemblyFromDom(compilerParameters, codeUnit);

            if (result.Errors.Count > 0)
                throw new Exception();

            return result.CompiledAssembly;
        }
    }
}