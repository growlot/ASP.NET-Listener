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
        private static Assembly odataModelAssembly;

        private static Dictionary<string, ODataModelMapping> oDataModelMappings =
            new Dictionary<string, ODataModelMapping>();

        public string ODataModelNamespace => "AMSLLC.Listener.ODataService.ODataModel";

        private List<WNPMetadataEntry> RawMetadata =>
                MemoryCache.Default.GetOrAddExisting("_RawMetadata", 
                    () => dbContext.Fetch<WNPMetadataEntry>(Sql.Builder.Select(DBMetadata.ALL).From(DBMetadata.Metadata)), 
                    DateTime.Now.AddMinutes(1));

        public Assembly ODataModelAssembly => odataModelAssembly ?? (odataModelAssembly = GenerateODataAssembly());

        public ODataModelMapping GetModelMapping(string clrModelName) =>
            oDataModelMappings[$"{ODataModelNamespace}.{clrModelName}"];

        public MetadataServiceImpl(MetadataDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private Assembly GenerateODataAssembly()
        {
            var codeUnit = new CodeCompileUnit();

            //PetaPoco.Mappers.GetMapper(typeof(WNPMetadata)).GetColumnInfo(typeof(WNPMetadata).GetProperty("blabla")).

            var codeNamespace = new CodeNamespace(ODataModelNamespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel.DataAnnotations"));

            // TODO: we should move the IODataEntity marker interface to another library, think about this
            codeNamespace.Imports.Add(new CodeNamespaceImport("AMSLLC.Listener.MetadataService"));

            // only tables defined in metadata with INFO as column name and IsUsed flag will be exposed in oData
            var tables = RawMetadata.Where(metadata => metadata.ColumnName == "INFO" && metadata.IsUsed == "Y");

            foreach (var table in tables)
            {
                var mappingInfo = new Dictionary<string, ODataToDatabaseColumnInfo>();
                var reverseMappingInfo = new Dictionary<string, string>();

                // if there is a redefine in Metadata, use CustomerLabel inst
                var modelClassName = table.CustomerLabel;

                var codeClass = new CodeTypeDeclaration(modelClassName)
                {
                    IsClass = true,
                    TypeAttributes = TypeAttributes.Public,
                    BaseTypes = { typeof(IODataEntity) }
                };

                codeNamespace.Types.Add(codeClass);

                foreach (var columnReadableName in DBMetadata.TableLookup[table.TableName.ToLowerInvariant()].ColumnsLookup.Keys)
                {
                    var property = new CodeSnippetTypeMember();
                    var columnInfo = DBMetadata.TableLookup[table.TableName.ToLowerInvariant()].ColumnsLookup[columnReadableName];
                    var metadataInfo =
                        RawMetadata.FirstOrDefault(
                            metadata =>
                                metadata.TableName.ToLowerInvariant() == table.TableName.ToLowerInvariant() &&
                                string.Equals(metadata.ColumnName, columnInfo.ColumnName, StringComparison.InvariantCultureIgnoreCase) &&
                                metadata.IsUsed == "Y");

                    if (metadataInfo != null)
                    {
                        if (metadataInfo.IsPrimaryKey == "Y")
                            property.Text = "[Key]";

                        var dataType = metadataInfo.DataType;
                        if (dataType.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                            dataType = "DateTimeOffset";

                        property.Text += $" public {dataType} {metadataInfo.CustomerLabel} {{ get; set; }}";
                        mappingInfo.Add(metadataInfo.CustomerLabel, columnInfo.ColumnName);
                        reverseMappingInfo.Add(columnInfo.ColumnName, metadataInfo.CustomerLabel);
                    }
                    else
                    {
                        var dataType = columnInfo.DataType;
                        if (dataType.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                            dataType = "DateTimeOffset";

                        property.Text = $" public {dataType} {columnInfo.ModelName} {{ get; set; }}";
                        mappingInfo.Add(columnInfo.ModelName, columnInfo.ColumnName);
                        reverseMappingInfo.Add(columnInfo.ColumnName, columnInfo.ModelName);
                    }

                    codeClass.Members.Add(property);
                }

                oDataModelMappings.Add($"{ODataModelNamespace}.{modelClassName}",
                    new ODataModelMapping(DBMetadata.TableLookup[table.TableName.ToLowerInvariant()].ToString(), mappingInfo, reverseMappingInfo));
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

            return result.CompiledAssembly;
        }
    }
}