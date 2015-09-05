using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using SHUPV.Database;

namespace SHUPV.Database
{
	public class WeatherInfo
	{

		public bool IsEmpty { get { return _isEmpty; } }

		public WeatherInfo(double latitude, double longitude)
		{
			_lat = latitude;
			_lon = longitude;
			_sqlCon = WeatherDatabase.GetSqlConnection();
		}

		public void Open()
		{
			try
			{
				_sqlCon.Open();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		double _lat;
		double _lon;
		SqlConnection _sqlCon;
		bool _isEmpty;
	}
}
