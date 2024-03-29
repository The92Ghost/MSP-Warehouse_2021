﻿using System;
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
    public partial class Form3 : Form
    {
        public Form3()
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
            adapt = new SqlDataAdapter("select * from Items", myConnection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection = new SqlConnection(frm.cs);
                myCommand = new SqlCommand("insert into Items(IDItem,ItemName,ItemDescription) values(@IDItem,@ItemName,@ItemDescription)", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@IDItem", textBox1.Text);
                myCommand.Parameters.AddWithValue("@ItemName", textBox2.Text);
                myCommand.Parameters.AddWithValue("@ItemDescription", textBox3.Text);
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
                myCommand = new SqlCommand("update Items set ItemName=@ItemName, ItemDescription=@ItemDescription where IDItem=@IDItem", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@IDItem", textBox1.Text);
                myCommand.Parameters.AddWithValue("@ItemName", textBox2.Text);
                myCommand.Parameters.AddWithValue("@ItemDescription", textBox3.Text);
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
            try
            {
                myConnection = new SqlConnection(frm.cs);
                myCommand = new SqlCommand("delete Items where IDItem=@IDItem", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@IDItem", textBox1.Text);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                MessageBox.Show("Delete successfully!");
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet3.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.database1DataSet3.Items);

        }
    }
}
