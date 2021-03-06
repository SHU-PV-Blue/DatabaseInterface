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
using System.Data.OleDb;

namespace SHUPV.Database.Core
{
	/// <summary>
	/// 核心数据库接口,提供对目标数据库操作的基本操作
	/// </summary>
	public class DatabaseCore
	{
		/// <summary>
		/// SQL server数据库连接，仅是对外部给的数据库连接的引用，不负责维护
		/// </summary>
		SqlConnection _sqlCon;

		/// <summary>
		/// ACCESS数据库连接，仅是对外部给的数据库连接的引用，不负责维护
		/// </summary>
		OleDbConnection _oleCon;

		/// <summary>
		/// 数据库连接类型枚举
		/// </summary>
		enum ConnectionType { SQL, OLE };

		/// <summary>
		/// 数据库连接类型
		/// </summary>
		ConnectionType _connectionType;

		/// <summary>
		/// SQL Sever数据库连接构造函数
		/// </summary>
		/// <param name="sqlCon">已打开的数据库连接</param>
		public DatabaseCore(SqlConnection sqlCon) 
		{
            _sqlCon = sqlCon;
			_connectionType = ConnectionType.SQL;
		}

		/// <summary>
		/// Access数据库连接构造函数
		/// </summary>
		/// <param name="oleCon">已打开的数据库连接</param>
		public DatabaseCore(OleDbConnection oleCon)
		{
			_oleCon = oleCon;
			_connectionType = ConnectionType.OLE;
		}

		/// <summary>
		/// 插入函数
		/// </summary>
		/// <param name="tableName">表名字</param>
        /// <param name="terms">修改的列和目标值的键值对</param>
		/// <returns>插入是否成功</returns>
        public bool InsertData(string tableName, Dictionary<string,string> terms)
        {
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
                    queryString += "[" + kvp.Key + "]";
                }
                queryString += ") values (";
                isFirst = true;
                foreach (KeyValuePair<string, string> kvp in terms)
                {
                    if (!isFirst)
                        queryString += ", ";
                    isFirst = false;
                    queryString += kvp.Value;
                }
                queryString += ")";
            }
            try
            {
				switch(_connectionType)
				{
					case ConnectionType.SQL:
						{
							SqlCommand cursor = new SqlCommand(queryString, _sqlCon);
							cursor.ExecuteNonQuery();
							break;
						}
					case ConnectionType.OLE:
						{
							OleDbCommand cursor = new OleDbCommand(queryString, _oleCon);
							cursor.ExecuteNonQuery();
							break;
						}
				}
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
        /// <returns>删除是否成功</returns>
        public bool DeleteData(string tableName, Dictionary<string, string> queryTerms)
        {
            string queryString = "delete from " + tableName;
            if (queryTerms != null && queryTerms.Count != 0)
            {
                queryString += " where";
                bool isFirst = true;
                foreach (KeyValuePair<string, string> kvp in queryTerms)
                {
                    if (!isFirst)
                        queryString += " and";
                    isFirst = false;
                    queryString += " [" + kvp.Key + "] = " + kvp.Value;
                }
            }
            try
            {
				switch (_connectionType)
				{
					case ConnectionType.SQL:
						{
							SqlCommand cursor = new SqlCommand(queryString, _sqlCon);
							cursor.ExecuteNonQuery();
							break;
						}
					case ConnectionType.OLE:
						{
							OleDbCommand cursor = new OleDbCommand(queryString, _oleCon);
							cursor.ExecuteNonQuery();
							break;
						}
				}
				return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改函数
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="updateTerms">更新的列和值的键值对</param>
        /// <param name="queryTerms">查询条件where键值对</param>
        /// <returns>更新是否成功</returns>
        public bool UpdateData(string tableName, Dictionary<string,string> updateTerms, Dictionary<string,string> queryTerms)
        {
            string queryString = "update " + tableName;
            if (updateTerms != null && updateTerms.Count != 0)
            {
                queryString += " set";
                bool isFirst = true;
                foreach (KeyValuePair<string, string> kvp in updateTerms)
                {
                    if (!isFirst)
                        queryString += ",";
                    isFirst = false;
                    queryString += " [" + kvp.Key + "] = " + kvp.Value;
                }
            }
            if (queryTerms != null && queryTerms.Count != 0)
            {
                queryString += " where";
                bool isFirst = true;
                foreach (KeyValuePair<string, string> kvp in queryTerms)
                {
                    if (!isFirst)
                        queryString += " and";
                    isFirst = false;
                    queryString += " [" + kvp.Key + "] = " + kvp.Value;
                }
            }
            try
            {
				switch (_connectionType)
				{
					case ConnectionType.SQL:
						{
							SqlCommand cursor = new SqlCommand(queryString, _sqlCon);
							cursor.ExecuteNonQuery();
							break;
						}
					case ConnectionType.OLE:
						{
							OleDbCommand cursor = new OleDbCommand(queryString, _oleCon);
							cursor.ExecuteNonQuery();
							break;
						}
				}
				return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 查询函数
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="queryTerms">查询条件where键值对</param>
        /// <returns>查询返回的数据</returns>
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
					queryString += " [" + kvp.Key + "] = " + kvp.Value;
				}
			}
			DataTable result = new DataTable(tableName);
			switch (_connectionType)
			{
				case ConnectionType.SQL:
					{
						SqlDataAdapter sda = new SqlDataAdapter(queryString, _sqlCon);
						sda.Fill(result);
						break;
					}
				case ConnectionType.OLE:
					{
						OleDbDataAdapter odda = new OleDbDataAdapter(queryString, _oleCon);
						odda.Fill(result);
						break;
					}
			}
			return result;//返回查询数据
        }
	}
}
