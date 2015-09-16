using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHUPV.Database;

namespace Test_SHUPV.Database
{
	class Program
	{
		static void Main(string[] args)
		{
			WeatherData wd = new WeatherData();
			Console.WriteLine("获得表名：");
			List<string> ls = wd.GetPartNames();
			foreach (string str in ls)
				Console.WriteLine(str);
		}
	}

}
