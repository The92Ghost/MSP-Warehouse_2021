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

namespace MSP_Warehouse_2021
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        Form1 frm = new Form1();
        SqlConnection myConnection;
        SqlCommand myCommand = default(SqlCommand);
        SqlDataAdapter adapt;


        private void DisplayData()
        {
            myConnection.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from WarehouseTypes", myConnection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection = new SqlConnection(frm.cs);
                myCommand = new SqlCommand("insert into WarehouseTypes(WarehouseName,StockType) values(@WarehouseName,@StockType)", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@WarehouseName", textBox1.Text);
                myCommand.Parameters.AddWithValue("@StockType", textBox2.Text);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                MessageBox.Show("Insert successfully!");
                DisplayData();
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection = new SqlConnection(frm.cs);
                myCommand = new SqlCommand("update WarehouseTypes set StockType=@StockType where WarehouseName=@WarehouseName", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@WarehouseName", textBox1.Text);
                myCommand.Parameters.AddWithValue("@StockType", textBox2.Text);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                MessageBox.Show("Update successfully!");
                DisplayData();
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
