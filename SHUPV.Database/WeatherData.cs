//******************************************************************************************
//
// 文件名(File Name): WeatherData.cs
// 
// 描述(Description): 包含类SHUPV.Database.WeatherData的定义
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
	public class WeatherData
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
		public WeatherData(double latitude, double longitude)
		{
			_latitude = latitude;												//初始化纬度
			_longitude = longitude;												//初始化经度
			_sqlCon = WeatherDatabase.GetSqlConnection();					//获取数据库连接
			_isEmpty = true;													//数据缓存为空
		}

		/// <summary>
		/// 连接数据库并缓存天气数据
		/// </summary>
		public void Open()
		{
			//打开数据库,将指定位置的所有有关数据全部缓存到_dataSet中,若成功,_isEmpty置假,否则抛出相应异常
            _isEmpty = false;
            string sqlString = "select * from Lines where Lat =" + _latitude.ToString() + "and Lon = " + _longitude.ToString();
            _dataSet = new DataSet();
            try
            {
                _sqlCon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlString, _sqlCon);

                sda.Fill(_dataSet, "tempTable");
                if (_dataSet.Tables[0].Rows.Count > 0)
                    _isEmpty = false;
                else
                    _isEmpty = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally{
                _sqlCon.Close();
                _sqlCon.Dispose();
            }
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="tableID">数据所在表的缩写</param>
		/// <param name="lineName">数据所在行的名称</param>
		/// <returns>12个浮点数,对应12个月,若为double.NaN说明该月无记录</returns>
		public List<double> GetData(string tableID, string lineName)
		{
            //从_dataSet中取出用户想要的数据,若成功,则返回数据,否则抛出相应异常
			List<double> result = new List<double>();

            DataRow[] dr = _dataSet.Tables["tempTable"].Select("TableID ='" + tableID + "' and LineName = '" + lineName + "'");
            int j = _dataSet.Tables["tempTable"].Columns.Count;
            
            for (int i = j - 12; i < j; i++)
            {
                if (dr[0][i] == DBNull.Value)
                    result.Add(Double.NaN);
                else
                    result.Add(Convert.ToDouble(dr[0][i]));
            }

            return result;
		}
	}
}
