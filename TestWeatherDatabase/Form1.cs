using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using SHUPV.Database;

namespace TestWeatherDatabase
{
	public partial class Form1 : Form
	{
		SqlConnection sqlCon;
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			sqlCon = WeatherDatabase.GetSqlConnection();
			try
			{
				sqlCon.Open();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			MessageBox.Show("连接数据库成功!");
			button1.Enabled = false;
			button2.Enabled = true;
			button3.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string sqlCmdText = "select * from Lines where [Lat] = 12.5 and [lon] = 9.5";
			SqlCommand sqlCmd = new SqlCommand(sqlCmdText, sqlCon);
			SqlDataReader dataReader;
			try
			{
				dataReader = sqlCmd.ExecuteReader();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			dataReader.Read();
			MessageBox.Show(dataReader["TableID"].ToString());
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				sqlCon.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			MessageBox.Show("关闭数据库成功!");
			button1.Enabled = true;
			button2.Enabled = false;
			button3.Enabled = false;
		}
	}
}
