﻿//******************************************************************************************
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
using System.Data;

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
            _sqlCon = sqlCon;
		}

		/// <summary>
		/// 插入函数
		/// </summary>
		/// <param name="tableName">表名字</param>
        /// <param name="terms">修改的列和目标值的键值对</param>
		/// <returns>bool 插入结果</returns>
        public bool InsertData(string tableName, Dictionary<string,string> terms)
        {
#warning 未完成
            string queryString = "insert into " + tableName;
            if (terms != null && terms.Count != 0)
            {
                queryString += " (";
                bool isFirst = true;
                foreach (KeyValuePair<string, string> kvp in terms)
                {
                    if (!isFirst)
                        queryString += ", ";
                    isFirst = false;
                    queryString += kvp.Key;
                }
                queryString += ") values (";
                isFirst = true;
                foreach (KeyValuePair<string, string> kvp in terms)
                {
                    if (!isFirst)
                        queryString += ", ";
                    isFirst = false;
                    queryString += "\'" + kvp.Value + "\'";
                }
                queryString += ")";
            }
            try
            {
                SqlCommand cursor = new SqlCommand(queryString, _sqlCon);
                cursor.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        }
        
        /// <summary>
        /// 删除函数
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="queryTerms">删除条件where键值对</param>
        /// <returns>bool 插入结果</returns>
        public bool DeleteData(string tableName, Dictionary<string, string> queryTerms)
        {
#warning 未完成
            return true;  //返回删除结果
        }

        /// <summary>
        /// 修改函数
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="updateTerms">更新列值的键值对</param>
        /// <param name="queryTerms">查询条件where键值对</param>
        /// <returns>bool 插入结果</returns>
        public bool UpdateData(string tableName, Dictionary<string,string> updateTerms, Dictionary<string,string> queryTerms)
        {
#warning 未完成
            return true;  //返回更新结果
        }

        /// <summary>
        /// 查询函数
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="queryTerms">查询条件where键值对</param>
        /// <returns>DataSet 查询数据</returns>
        public DataTable SelectData(string tableName, Dictionary<string,string> queryTerms)
        {
			string queryString = "select * from " + tableName;
			if (queryTerms != null && queryTerms.Count != 0)
			{
				queryString += " where";
				bool isFirst = true;
				foreach(KeyValuePair<string,string> kvp in queryTerms)
				{
					if(!isFirst)
						queryString += " and";
					isFirst =false;
					queryString += " [" + kvp.Key + "] = \'" + kvp.Value + "\'";
				}
			}
			SqlDataAdapter sda = new SqlDataAdapter(queryString, _sqlCon);
			DataTable result = new DataTable(tableName);
			sda.Fill(result);
			return result;//返回查询数据
        }
	}
}
