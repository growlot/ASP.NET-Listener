using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.MetadataService
{
    public class MetadataModel
    {
        public string TableName { get; }
        public string ClassName { get; }
        public Dictionary<string, MetadataFieldInfo> FieldInfo { get; }
        public Dictionary<string, string> ModelToColumnMappings { get; }
        public Dictionary<string, string> ColumnToModelMappings { get; }
        public Type ActionsContainer { get; }

        public MetadataModel(string tableName, string className, Dictionary<string, string> modelToColumnMappings, Dictionary<string, string> columnToModelMappings, Dictionary<string, MetadataFieldInfo> fieldInfo, Type actionsContainer)
        {
            this.TableName = tableName;
            this.ClassName = className;
            this.ModelToColumnMappings = modelToColumnMappings;
            this.ColumnToModelMappings = columnToModelMappings;
            this.FieldInfo = fieldInfo;
            this.ActionsContainer = actionsContainer;
        }
    }
}
