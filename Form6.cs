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
using System.IO;

namespace MSP_Warehouse_2021
{
    public partial class Form6 : Form
    {
        public Form6()
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
            adapt = new SqlDataAdapter("select * from Inventory", myConnection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection = new SqlConnection(frm.cs);
                myCommand = new SqlCommand("insert into Inventory(IDItem,ItemName,Quantity,MinQuantity,Price,Tax,ArticleGroup,Measure,WarehouseCode) values(@IDItem,@ItemName,@Quantity,@MinQuantity,@Price,@Tax,@ArticleGroup,@Measure,@WarehouseCode)", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@IDItem", textBox1.Text);
                myCommand.Parameters.AddWithValue("@ItemName", textBox2.Text);
                myCommand.Parameters.AddWithValue("@Quantity", textBox3.Text);
                myCommand.Parameters.AddWithValue("@MinQuantity", textBox4.Text);
                myCommand.Parameters.AddWithValue("@Price", textBox5.Text);
                myCommand.Parameters.AddWithValue("@Tax", textBox6.Text);
                myCommand.Parameters.AddWithValue("@ArticleGroup", textBox7.Text);
                myCommand.Parameters.AddWithValue("@Measure", textBox8.Text);
                myCommand.Parameters.AddWithValue("@WarehouseCode", textBox9.Text);
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
                myCommand = new SqlCommand("update Inventory set ItemName=@ItemName, Quantity=@Quantity, MinQuantity=@MinQuantity, Price=@Price, Tax=@Tax, ArticleGroup=@ArticleGroup, Measure=@Measure, WarehouseCode=@WarehouseCode where IDItem=@IDItem", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@IDItem", textBox1.Text);
                myCommand.Parameters.AddWithValue("@ItemName", textBox2.Text);
                myCommand.Parameters.AddWithValue("@Quantity", textBox3.Text);
                myCommand.Parameters.AddWithValue("@MinQuantity", textBox4.Text);
                myCommand.Parameters.AddWithValue("@Price", textBox5.Text);
                myCommand.Parameters.AddWithValue("@Tax", textBox6.Text);
                myCommand.Parameters.AddWithValue("@ArticleGroup", textBox7.Text);
                myCommand.Parameters.AddWithValue("@Measure", textBox8.Text);
                myCommand.Parameters.AddWithValue("@WarehouseCode", textBox9.Text);
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
                myCommand = new SqlCommand("delete Inventory where IDItem=@IDItem", myConnection);
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
            try
            {
                var discountRate = 0.07; //could be Convert.ToInt32(textBox1.Text) or something else
                var WarehouseId = 1;    //again, could be Convert.ToInt32(textBox2.Text) or something else
                myConnection = new SqlConnection(frm.cs);
                myCommand = new SqlCommand("update Inventory set Price=Price*(1-@DiscountRate)" +
                                            "where WarehouseCode IN (1,3)", myConnection);
                myConnection.Open();
                myCommand.Parameters.AddWithValue("@DiscountRate", discountRate);
                myCommand.Parameters.AddWithValue("@WarehouseId", WarehouseId);
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
            button4.Enabled = false;
            button4.BackColor = Color.Red;
        }

        private void DataTableToTextFile(DataTable dt, string outputFilePath)
        {
            int[] maxLengths = new int[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                maxLengths[i] = dt.Columns[i].ColumnName.Length;
                foreach (DataRow row in dt.Rows)
                {
                    if (!row.IsNull(i))
                    {
                        int length = row[i].ToString().Length;
                        if (length > maxLengths[i])
                        {
                            maxLengths[i] = length;
                        }
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(outputFilePath, false))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2));
                }
                sw.WriteLine();
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            sw.Write(row[i].ToString().PadRight(maxLengths[i] + 2));
                        }
                        else
                        {
                            sw.Write(new string(' ', maxLengths[i] + 2));
                        }
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = null;
            Form1 frm = new Form1();
            connectionString = frm.cs;

            DataTable dt = new DataTable();
            foreach (DataGridViewTextBoxColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.Name, column.ValueType);
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewTextBoxColumn column in dataGridView1.Columns)
                {
                    if (row.Cells[column.Name].Value != null)
                    {
                        dr[column.Name] = row.Cells[column.Name].Value.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filePath = saveFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog1.FileName;
            }
            DataTableToTextFile(dt, filePath);
            MessageBox.Show("File created!");
        }

        private void view()
        {
            myConnection.Open();
            DataTable dt1 = new DataTable();
            adapt = new SqlDataAdapter("select * from Inventory where WarehouseCode IN (1,3)", myConnection);
            adapt.Fill(dt1);
            dataGridView1.DataSource = dt1;
            myConnection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var WarehouseID = 1;
            myConnection = new SqlConnection(frm.cs);
            myCommand = new SqlCommand("select * from Inventory where WarehouseCode=WarehouseID", myConnection);
            myConnection.Open();
            myCommand.Parameters.AddWithValue("@WarehouseID", WarehouseID);
            myConnection.Close();
            MessageBox.Show("File created!");
            view();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Dispose();
            }

            string connectionString = null;
            Form1 frm1 = new Form1();
            connectionString = frm1.cs;

            DataTable dt = new DataTable();
            foreach (DataGridViewTextBoxColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.Name, column.ValueType);
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewTextBoxColumn column in dataGridView1.Columns)
                {
                    if (row.Cells[column.Name].Value != null)
                    {
                        dr[column.Name] = row.Cells[column.Name].Value.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filePath = saveFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog1.FileName;
            }
            DataTableToTextFile(dt, filePath);
            MessageBox.Show("File created!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }


        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet8.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.database1DataSet8.Inventory);

        }
    }
}
