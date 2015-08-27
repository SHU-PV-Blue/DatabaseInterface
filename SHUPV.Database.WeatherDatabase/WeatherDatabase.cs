using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace SHUPV.Database
{

#warning 若编译器提示找不到WeatherDatabaseAccount.cs,请向项目创建者索要.

	static public partial class WeatherDatabase
	{
		static public SqlConnection GetSqlConnection()
		{
			string connectionString = "Server = " + _server + "; Database = " + _database + ";Uid = " + _userid + ";Pwd = " + _password + ";";
			return new SqlConnection(connectionString);
		}
	}
}
