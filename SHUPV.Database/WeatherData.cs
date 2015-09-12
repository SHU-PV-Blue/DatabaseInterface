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
				finally
				{
					_sqlCon.Close();											//关闭数据库
					_sqlCon.Dispose();											//释放数据库
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

		//各种功能函数
	}
}
