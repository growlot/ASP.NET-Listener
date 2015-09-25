using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.MetadataService
{
    public class ODataModelMapping
    {
        public Dictionary<string, string> ModelToColumnMappings { get; }
        public Dictionary<string, string> ColumnToModelMappings { get; }
        public string TableName { get; }

        public ODataModelMapping(string tableName, Dictionary<string, string> modelToColumnMappings, Dictionary<string, string> columnToModelMappings)
        {
            ModelToColumnMappings = modelToColumnMappings;
            ColumnToModelMappings = columnToModelMappings;
            TableName = tableName;
        }
    }

}
