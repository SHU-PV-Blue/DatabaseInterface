//******************************************************************************************
//
// 文件名(File Name): WeatherInfo.cs
// 
// 描述(Description): 包含类SHUPV.Database.WeatherInfo的定义
//
// 引用(Using): SHUPV.Database.Connection.dll
//
// 作者(Author): 杨永华,宋建鑫
//
// 日期(Create Date): 2015/09/05
//
//******************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SHUPV.Database.Connection;

namespace SHUPV.Database
{
	/// <summary>
	/// 天气信息数据接口,访问数据库并提供用户索要的数据
	/// </summary>
	public class WeatherInfo
	{
		/// <summary>
		/// 纬度
		/// </summary>
		double _latitude;
		/// <summary>
		/// 经度
		/// </summary>
		double _longitude;
		/// <summary>
		/// 数据库连接
		/// </summary>
		SqlConnection _sqlCon;
		/// <summary>
		/// 天气数据在内存中的缓存
		/// </summary>
		DataSet _dataSet;
		/// <summary>
		/// 表示天气数据是否填充至_dataSet
		/// </summary>
		bool _isEmpty;


		/// <summary>
		/// 指明本对象是否已从数据库获得数据
		/// </summary>
		public bool IsEmpty { get { return _isEmpty; } }


		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="latitude">该地的纬度</param>
		/// <param name="longitude">该地的经度</param>
		public WeatherInfo(double latitude, double longitude)
		{
			_latitude = latitude;
			_longitude = longitude;
			_sqlCon = WeatherDatabase.GetSqlConnection();
		}

		/// <summary>
		/// 连接数据库并缓存天气数据
		/// </summary>
		public void Open()
		{

		}

		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="tableID">数据所在表的缩写</param>
		/// <param name="lineName">数据所在行的名称</param>
		/// <returns>12个浮点数,对应12个月,若为double.NaN说明该月无记录</returns>
		public List<double> GetData(string tableID, string lineName)
		{
			List<double> result = new List<double>();

			return result;
		}
	}
}
