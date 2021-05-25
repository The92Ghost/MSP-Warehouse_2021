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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\The92Ghost\MSP-Warehouse_2021\Database1.mdf;Integrated Security=True;Connect Timeout=30"; 

        public SqlConnection myconnection = default(SqlConnection);
        public SqlCommand mycommand = default(SqlCommand);

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myconnection = new SqlConnection(cs);
                mycommand = new SqlCommand("Select * from Registration where Username=@Username and Password=@Password", myconnection);
                SqlParameter uUsername = new SqlParameter("@Username", SqlDbType.VarChar);
                SqlParameter uPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                uUsername.Value = textBox1.Text;
                uPassword.Value = textBox2.Text;
                mycommand.Parameters.Add(uUsername);
                mycommand.Parameters.Add(uPassword);
                mycommand.Connection.Open();
                SqlDataReader myreader = mycommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (myreader.Read() == true)
                {
                    MessageBox.Show("You have successful login in user " + textBox1.Text);
                    this.Hide();
                    Form7 frm = new Form7();
                    frm.Show();

                }
                else
                {
                    MessageBox.Show("Login failed. Try again.", "Login denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }

                if (myconnection.State == ConnectionState.Open)
                {
                    myconnection.Dispose();
                }



            }
            catch (ExecutionEngineException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }
    }
}
