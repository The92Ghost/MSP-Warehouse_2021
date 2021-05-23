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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Form1 frm = new Form1();
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataAdapter adapt;

        

        private void displayData()
        {
            myconnection.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("Select Username from Registration where Username='" + textBox1.Text + "'", myconnection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            myconnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myconnection = new SqlConnection(frm.cs);
                mycommand = new SqlCommand("Insert into Registration values('" + textBox1.Text + "','" + textBox2.Text + "')", myconnection);
                myconnection.Open();
                mycommand.Parameters.AddWithValue("@Username", textBox1.Text);
                mycommand.Parameters.AddWithValue("@Password", textBox2.Text);
                mycommand.ExecuteNonQuery();
                myconnection.Close();
                MessageBox.Show("Message insert successfully");
                displayData();
                if (myconnection.State == ConnectionState.Open)
                {
                    myconnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet2.Registration' table. You can move, or remove it, as needed.
            this.registrationTableAdapter2.Fill(this.database1DataSet2.Registration);

        }
    }
}
