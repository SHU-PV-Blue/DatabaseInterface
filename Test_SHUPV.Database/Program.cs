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
			double a,b;
			wd.GetNearestCoordinate(1, 1 ,out a, out b);
			Console.WriteLine( a + "   " + b);
		}
	}
}
