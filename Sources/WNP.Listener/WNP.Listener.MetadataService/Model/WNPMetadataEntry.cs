using PetaPoco;

namespace WNP.Listener.MetadataService.Model
{
    public class WNPMetadataEntry
    {
        public int Owner { get; set; } // owner (Primary key)

        [Column("table_name")]
        public string TableName { get; set; } // table_name (Primary key)
        public string ColumnName { get; set; } // column_name (Primary key)
        public string DataType { get; set; } // data_type
        public int? DataLength { get; set; } // data_length
        public int? DataPrecision { get; set; } // data_precision
        public string IsUsed { get; set; } // is_used
        public string IsRequiredOnNew { get; set; } // is_required_on_new
        public string IsRequiredAlways { get; set; } // is_required_always
        public string IsPrimaryKey { get; set; } // is_primary_key
        public string IsForeignKey { get; set; } // is_foreign_key
        public string IsIdentity { get; set; } // is_identity
        public string NullIfBlank { get; set; } // null_if_blank
        public string InitIfNull { get; set; } // init_if_null
        public string ValueIfNull { get; set; } // value_if_null
        public string DataRegex { get; set; } // data_regex
        public string DataFormat { get; set; } // data_format
        public string CustomerLabel { get; set; } // customer_label
        public string DataDescription { get; set; } // data_description
        public string DataExample { get; set; } // data_example
        public int Id { get; set; } // id
    }
}