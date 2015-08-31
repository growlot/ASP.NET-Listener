using System.Collections.Generic;
public class ShopGoalsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string EqpType = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopStatus = "SHOP_STATUS";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Year = "YEAR";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string January = "JANUARY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string February = "FEBRUARY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string March = "MARCH";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string April = "APRIL";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string May = "MAY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string June = "JUNE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string July = "JULY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string August = "AUGUST";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string September = "SEPTEMBER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string October = "OCTOBER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string November = "NOVEMBER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string December = "DECEMBER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "MOD_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "MOD_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
	
	public string RealTableName
	{
		get { return "TSHOP_GOALS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"ShopStatus", new ColumnInformation() { DataType = "string", ModelName = "ShopStatus", ColumnName = "SHOP_STATUS"}},
				{"Year", new ColumnInformation() { DataType = "int", ModelName = "Year", ColumnName = "YEAR"}},
				{"January", new ColumnInformation() { DataType = "int", ModelName = "January", ColumnName = "JANUARY"}},
				{"February", new ColumnInformation() { DataType = "int", ModelName = "February", ColumnName = "FEBRUARY"}},
				{"March", new ColumnInformation() { DataType = "int", ModelName = "March", ColumnName = "MARCH"}},
				{"April", new ColumnInformation() { DataType = "int", ModelName = "April", ColumnName = "APRIL"}},
				{"May", new ColumnInformation() { DataType = "int", ModelName = "May", ColumnName = "MAY"}},
				{"June", new ColumnInformation() { DataType = "int", ModelName = "June", ColumnName = "JUNE"}},
				{"July", new ColumnInformation() { DataType = "int", ModelName = "July", ColumnName = "JULY"}},
				{"August", new ColumnInformation() { DataType = "int", ModelName = "August", ColumnName = "AUGUST"}},
				{"September", new ColumnInformation() { DataType = "int", ModelName = "September", ColumnName = "SEPTEMBER"}},
				{"October", new ColumnInformation() { DataType = "int", ModelName = "October", ColumnName = "OCTOBER"}},
				{"November", new ColumnInformation() { DataType = "int", ModelName = "November", ColumnName = "NOVEMBER"}},
				{"December", new ColumnInformation() { DataType = "int", ModelName = "December", ColumnName = "DECEMBER"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
			};

	public override string ToString() 
	{
		return "wndba.tshop_goals";
	}
}
