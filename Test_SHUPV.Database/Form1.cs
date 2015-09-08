using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SHUPV.Database;
using System.Diagnostics;

namespace Test_SHUPV.Database
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
        private WeatherData wd = null;

        private void button1_Click(object sender, EventArgs e)
        {
            wd = new WeatherData(Convert.ToDouble(txt_lat.Text), Convert.ToDouble(txt_lon.Text));
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                wd.Open();
                sw.Stop();
                MessageBox.Show("Open()完成,共用时 "+ sw.Elapsed.Seconds + " 秒");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_getData_Click(object sender, EventArgs e)
        {
            List<double> tempData = null;
            try
            {
                if (!wd.IsEmpty)
                    label5.Text = "数据不为空";
                tempData = wd.GetData(txt_tableID.Text, txt_lineName.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int index = this.dataGridView1.Rows.Add();
            for (int i = 0; i < tempData.Count(); i++)
            {
                this.dataGridView1.Rows[index].Cells[i].Value = tempData[i].ToString();
            }
        }

	}
}
