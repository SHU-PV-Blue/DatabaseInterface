using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHUPV.Database;
using System.Data;

namespace Test_SHUPV.Database
{
	class Program
	{
		static void Main(string[] args)
		{
			TestGetTableData();
		}
		
		static void TestGetPartNames()
		{
			WeatherData wd = new WeatherData();
			Console.WriteLine("获得表名：");
			List<string> ls = wd.GetPartNames();
			foreach (string str in ls)
				Console.WriteLine(str);
		}

		static void TestGetTableData()
		{
			WeatherData wd = new WeatherData();
			Console.WriteLine("获得数据：");
			DataTable dt = wd.GetTableData(10,10,
				"Parameters for Sizing Battery or other Energy-storage Systems",
				"Equivalent Number Of NO-SUN Or BLACK Days");
			dt.Columns.Remove("LineID");
			dt.Columns.Remove("TableID");
			foreach (DataRow dr in dt.Rows)
			{
				for(int i = 0; i < dt.Columns.Count; ++i)
					Console.Write(dr[i] + " ");
				Console.WriteLine();
			}
				
		}
	}

}
