// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class VendorContactTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string VendorId { get; } = "vendor_id";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactIndex { get; } = "contact_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactFname { get; } = "contact_fname";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactLname { get; } = "contact_lname";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactPhoneNo { get; } = "contact_phone_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactEmail { get; } = "contact_email";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactComments { get; } = "contact_comments";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactStatus { get; } = "contact_status";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "mod_by";
	
	public string RealTableName
	{
		get { return "TVENDOR_CONTACT".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
		return "WNDBA.TVENDOR_CONTACT";
	}
}
}
#pragma warning restore 1591
