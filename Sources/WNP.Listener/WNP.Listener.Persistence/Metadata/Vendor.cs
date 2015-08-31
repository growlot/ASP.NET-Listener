using System.Collections.Generic;
public class VendorImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string VendorId = "vendor_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Mfr = "mfr";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorDesc = "vendor_desc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorShipAddr1 = "vendor_ship_addr1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorShipAddr2 = "vendor_ship_addr2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorCity = "vendor_city";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorState = "vendor_state";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorZip = "vendor_zip";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
	
	public string RealTableName
	{
		get { return "TVENDOR".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"VendorId", new ColumnInformation() { DataType = "int", ModelName = "VendorId", ColumnName = "vendor_id"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "mfr"}},
				{"VendorDesc", new ColumnInformation() { DataType = "string", ModelName = "VendorDesc", ColumnName = "vendor_desc"}},
				{"VendorShipAddr1", new ColumnInformation() { DataType = "string", ModelName = "VendorShipAddr1", ColumnName = "vendor_ship_addr1"}},
				{"VendorShipAddr2", new ColumnInformation() { DataType = "string", ModelName = "VendorShipAddr2", ColumnName = "vendor_ship_addr2"}},
				{"VendorCity", new ColumnInformation() { DataType = "string", ModelName = "VendorCity", ColumnName = "vendor_city"}},
				{"VendorState", new ColumnInformation() { DataType = "string", ModelName = "VendorState", ColumnName = "vendor_state"}},
				{"VendorZip", new ColumnInformation() { DataType = "string", ModelName = "VendorZip", ColumnName = "vendor_zip"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tvendor";
	}
}
