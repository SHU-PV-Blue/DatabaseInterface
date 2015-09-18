//******************************************************************************************
//
// 文件名(File Name): WeatherData.cs
// 
// 描述(Description): 包含类SHUPV.Database.WeatherData的定义
//
// 引用(Using):
//
// 作者(Author):
//
// 日期(Create Date): 2015/09/12
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
using SHUPV.Database.Core;

namespace SHUPV.Database
{
	/// <summary>
	/// 天气信息数据接口,访问数据库并提供用户索要的数据
	/// </summary>
	public class WeatherData :IDisposable
	{
		/// <summary>
		/// 数据库连接,静态变量,所有本类对象共享,共同维护
		/// </summary>
		static SqlConnection _sqlCon;
		
		/// <summary>
		/// 对象计数器,记录本类有多少个实例化对象
		/// </summary>
		static int _objectCount = 0;

		/// <summary>
		/// 构造函数
		/// </summary>
		public WeatherData()
		{
			if (_objectCount == 0)												//如果本类从未实例化
			{
				_sqlCon = WeatherDatabase.GetSqlConnection();					//获取数据库连接
				try
				{
					_sqlCon.Open();												//尝试打开数据库
				}
				catch(Exception ex)												//捕捉异常
				{
					throw ex;													//重抛
				}
			}
			++_objectCount;														//对象计数器加一
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			if (_objectCount == 1)												//如果本类仅剩最后一个实例
			{
				try
				{
					_sqlCon.Close();											//尝试关闭数据库
					_sqlCon.Dispose();											//尝试释放数据库
				}
				catch (Exception ex)											//捕捉异常
				{
					throw ex;													//重抛
				}
				finally
				{
					_objectCount = 0;											//对象计数器置零
				}
			}
			--_objectCount;														//对象计数器减一
		}

		/// <summary>
		/// 获得所有类别名
		/// </summary>
		/// <returns>所有类别名</returns>
		public List<string>GetPartNames()
		{
			DatabaseCore dc = new DatabaseCore(_sqlCon);
			DataTable dt = dc.SelectData("dbo.Parts", null);
			List<string> result = new List<string>();
			foreach (DataRow dr in dt.Rows)
			{
				result.Add(dr["PartName"].ToString());
			}
			return result;
		}

		/// <summary>
		/// 获得属于类别partName的所有数据表名
		/// </summary>
		/// <param name="partName">类别名</param>
		/// <returns>属于类别partName的所有数据表名</returns>
		public List<string>GetTableNames(string partName)
		{
#warning 未完成
			return new List<string>();
		}

		/// <summary>
		/// 获得属于类别tableName的所有数据行名
		/// </summary>
		/// <param name="latitude">纬度</param>
		/// <param name="longitude">经度</param>
		/// <param name="partName">类别名</param>
		/// <param name="tableName">表名</param>
		/// <returns>属于类别tableName的所有数据行名</returns>
		public List<string> GetLineNames(double latitude, double longitude, string partName, string tableName)
		{
#warning 未完成
			//这个函数先不急着写
			//之所以有经纬度参数是因为我发现同一张表，不同的经纬度，数据行可能不一样，有的多有的少
			return new List<string>();
		}

		/// <summary>
		/// 获得指定的气象数据表数据
		/// </summary>
		/// <param name="latitude">纬度</param>
		/// <param name="longitude">经度</param>
		/// <param name="partName">类别名</param>
		/// <param name="tableName">表名</param>
		/// <returns>指定的气象数据表数据</returns>
		public DataTable GetTableData(double latitude, double longitude, string partName, string tableName)
		{
#warning 未完成
			double nearestLatitude;
			double nearestLongitude;
			GetNearestCoordinate(latitude, longitude, out nearestLatitude, out nearestLongitude);
			DatabaseCore dc = new DatabaseCore(_sqlCon);

			string partID;
			string tableID;
			DataTable dataTable;

			Dictionary<string, string> queryTerms = new Dictionary<string, string>();
			queryTerms.Add("PartName", partName);
			DataTable dt = dc.SelectData("dbo.Parts", queryTerms);
			if (dt.Rows.Count != 1)
				throw new Exception("the result of select is abnormal");
			partID = dt.Rows[0]["PartID"].ToString();

			queryTerms.Clear();
			queryTerms.Add("PartID", partID);
			queryTerms.Add("TableName", tableName);
			dt = dc.SelectData("dbo.Tables", queryTerms);
			if (dt.Rows.Count != 1)
				throw new Exception("the result of select is abnormal");
			tableID = dt.Rows[0]["TableID"].ToString();

			queryTerms.Clear();
			queryTerms.Add("TableID", tableID);
			queryTerms.Add("Lat", nearestLatitude.ToString());
			queryTerms.Add("Lon", nearestLongitude.ToString());
			dt = dc.SelectData("dbo.Lines", queryTerms);
			if (dt.Rows.Count <= 0)
				throw new Exception("the result of select is abnormal");
			dataTable = dt;

			return dataTable;
		}

		/// <summary>
		/// 获得指定的气象数据行数据
		/// </summary>
		/// <param name="latitude">纬度</param>
		/// <param name="longitude">经度</param>
		/// <param name="partName">类别名</param>
		/// <param name="tableName">表名</param>
		/// <param name="lineName">行名</param>
		/// <returns>指定的气象数据行数据</returns>
		public List<string> GetLineData(double latitude, double longitude, string partName, string tableName, string lineName)
		{
#warning 未完成
			//这个函数先不急着写
			return new List<string>();
		}

		/// <summary>
		/// 获得数据库里最接近的坐标点
		/// </summary>
		/// <param name="inputLatitude">输入的纬度</param>
		/// <param name="inputLongitude">输入的经度</param>
		/// <param name="nearestLatitude">数据库里最接近的纬度</param>
		/// <param name="nearestLongitude">数据库里最接近的经度</param>
		/// <returns>是否获得成功</returns>
		public bool GetNearestCoordinate(double inputLatitude, double inputLongitude, out double nearestLatitude, out double nearestLongitude)
		{
			nearestLatitude = double.NaN;
			nearestLongitude = double.NaN;
			if (inputLatitude < -90 || inputLatitude > 90)
				return false;
			if (inputLongitude < -180 || inputLongitude > 180)
				return false;

			if (inputLatitude == 90)
				nearestLatitude = 89.5;
			else
				nearestLatitude = Math.Truncate(inputLatitude) + 0.5;

			if (inputLongitude == 180)
				nearestLongitude = 179.5;
			else
				nearestLongitude = Math.Truncate(inputLongitude) + 0.5;
				
			return true;
		}

	}
}
