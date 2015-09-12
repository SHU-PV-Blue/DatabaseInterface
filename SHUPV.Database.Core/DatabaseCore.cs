//******************************************************************************************
//
// 文件名(File Name): DatabaseCore.cs
// 
// 描述(Description): 包含类SHUPV.Database.DatabaseCore的定义
//
// 引用(Using):
//
// 作者(Author):
//
// 日期(Create Date):
//
//******************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SHUPV.Database.Core
{
	/// <summary>
	/// 核心数据库接口,提供对目标数据库操作的基本操作
	/// </summary>
	public class DatabaseCore
	{
		/// <summary>
		/// 数据库连接，仅是对外部给的数据库连接的引用，不负责维护
		/// </summary>
		SqlConnection _sqlCon;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="sqlCon">已打开的数据库连接</param>
		public DatabaseCore(SqlConnection sqlCon)
		{
#warning 未完成
		}

		//增
		//删
		//查
		//改
	}
}
