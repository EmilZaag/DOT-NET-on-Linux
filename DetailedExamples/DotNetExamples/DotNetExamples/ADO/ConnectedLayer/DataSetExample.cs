using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace ADO.ConnectedLayer
{
	public class DataSetExample
	{
		public static int Example ()
		{

			var connectionDetails = 
				ConfigurationManager.ConnectionStrings ["MyDatabase"];

			var providerName = connectionDetails.ProviderName;
			var connectionString = connectionDetails.ConnectionString;

			var dbFactory = DbProviderFactories.GetFactory (providerName);

			using (var cn = dbFactory.CreateConnection ()) {     
				cn.ConnectionString = connectionString;     
				cn.Open (); 

				var cmd = cn.CreateCommand ();

				cmd.CommandText = "Select * from MyTable";
				cmd.CommandType = CommandType.Text;
				cmd.Connection = cn;

				var dtable = new DataTable ();   

				using (var dr = cmd.ExecuteReader ()) {    
					dtable.Load (dr);     
				}

				return dtable.Rows.Count;
			}
		}
	}
}

