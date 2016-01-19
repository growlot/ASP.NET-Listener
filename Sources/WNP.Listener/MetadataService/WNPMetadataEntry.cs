// <copyright file="WNPMetadataEntry.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using AsyncPoco;

    /// <summary>
    /// Entry from WNP Metadata table
    /// </summary>
    public class WNPMetadataEntry
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; set; } // owner (Primary key)

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        [Column("table_name")]
        public string TableName { get; set; } // table_name (Primary key)

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string ColumnName { get; set; } // column_name (Primary key)

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get; set; } // data_type

        /// <summary>
        /// Gets or sets the length of the data.
        /// </summary>
        /// <value>
        /// The length of the data.
        /// </value>
        public int? DataLength { get; set; } // data_length

        /// <summary>
        /// Gets or sets the data precision.
        /// </summary>
        /// <value>
        /// The data precision.
        /// </value>
        public int? DataPrecision { get; set; } // data_precision

        /// <summary>
        /// Gets or sets the is used.
        /// </summary>
        /// <value>
        /// The is used.
        /// </value>
        public string IsUsed { get; set; } // is_used

        /// <summary>
        /// Gets or sets the is required on new.
        /// </summary>
        /// <value>
        /// The is required on new.
        /// </value>
        public string IsRequiredOnNew { get; set; } // is_required_on_new

        /// <summary>
        /// Gets or sets the is required always.
        /// </summary>
        /// <value>
        /// The is required always.
        /// </value>
        public string IsRequiredAlways { get; set; } // is_required_always

        /// <summary>
        /// Gets or sets the is primary key.
        /// </summary>
        /// <value>
        /// The is primary key.
        /// </value>
        public string IsPrimaryKey { get; set; } // is_primary_key

        /// <summary>
        /// Gets or sets the is foreign key.
        /// </summary>
        /// <value>
        /// The is foreign key.
        /// </value>
        public string IsForeignKey { get; set; } // is_foreign_key

        /// <summary>
        /// Gets or sets the is identity.
        /// </summary>
        /// <value>
        /// The is identity.
        /// </value>
        public string IsIdentity { get; set; } // is_identity

        /// <summary>
        /// Gets or sets the null if blank.
        /// </summary>
        /// <value>
        /// The null if blank.
        /// </value>
        public string NullIfBlank { get; set; } // null_if_blank

        /// <summary>
        /// Gets or sets the initialize if null.
        /// </summary>
        /// <value>
        /// The initialize if null.
        /// </value>
        public string InitIfNull { get; set; } // init_if_null

        /// <summary>
        /// Gets or sets the value if null.
        /// </summary>
        /// <value>
        /// The value if null.
        /// </value>
        public string ValueIfNull { get; set; } // value_if_null

        /// <summary>
        /// Gets or sets the data regex.
        /// </summary>
        /// <value>
        /// The data regex.
        /// </value>
        public string DataRegex { get; set; } // data_regex

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>
        /// The data format.
        /// </value>
        public string DataFormat { get; set; } // data_format

        /// <summary>
        /// Gets or sets the customer label.
        /// </summary>
        /// <value>
        /// The customer label.
        /// </value>
        public string CustomerLabel { get; set; } // customer_label

        /// <summary>
        /// Gets or sets the data description.
        /// </summary>
        /// <value>
        /// The data description.
        /// </value>
        public string DataDescription { get; set; } // data_description

        /// <summary>
        /// Gets or sets the data example.
        /// </summary>
        /// <value>
        /// The data example.
        /// </value>
        public string DataExample { get; set; } // data_example

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; } // id
    }
}