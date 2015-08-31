using System.Collections.Generic;
public class VendorContactImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string VendorId = "vendor_id";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactIndex = "contact_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactFname = "contact_fname";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactLname = "contact_lname";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactPhoneNo = "contact_phone_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactEmail = "contact_email";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactComments = "contact_comments";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactStatus = "contact_status";
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
		get { return "TVENDOR_CONTACT".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"VendorId", new ColumnInformation() { DataType = "int", ModelName = "VendorId", ColumnName = "vendor_id"}},
				{"ContactIndex", new ColumnInformation() { DataType = "int", ModelName = "ContactIndex", ColumnName = "contact_index"}},
				{"ContactFname", new ColumnInformation() { DataType = "string", ModelName = "ContactFname", ColumnName = "contact_fname"}},
				{"ContactLname", new ColumnInformation() { DataType = "string", ModelName = "ContactLname", ColumnName = "contact_lname"}},
				{"ContactPhoneNo", new ColumnInformation() { DataType = "string", ModelName = "ContactPhoneNo", ColumnName = "contact_phone_no"}},
				{"ContactEmail", new ColumnInformation() { DataType = "string", ModelName = "ContactEmail", ColumnName = "contact_email"}},
				{"ContactComments", new ColumnInformation() { DataType = "string", ModelName = "ContactComments", ColumnName = "contact_comments"}},
				{"ContactStatus", new ColumnInformation() { DataType = "string", ModelName = "ContactStatus", ColumnName = "contact_status"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tvendor_contact";
	}
}
