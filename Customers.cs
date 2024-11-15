using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
namespace carandbike1
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(ConnectionString.DBConn);

        private void AutoIdGeneration()
        {
            int num = 0;
            try
            {
                Con.Open();
                var cmd = new SqlCommand("SELECT ISNULL(MAX(CustId), 0) + 1 FROM CustomerTbl", Con);
                var result = cmd.ExecuteScalar();

                num = Convert.ToInt32(result);
                IdTb.Text = num.ToString(); // Display the generated ID
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating ID: {ex.Message}");
            }
            finally
            {
                Con.Close();
            }
        }

        private void Populate()
        {
            using (var da = new SqlDataAdapter("SELECT * FROM CustomerTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                CustomerDGV.DataSource = ds.Tables[0];
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            this.Hide();

            CarDashboard main = new CarDashboard();


            main.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTb.Text) || string.IsNullOrWhiteSpace(AddressTb.Text) || string.IsNullOrWhiteSpace(PhoneTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                using (var cmd = new SqlCommand("INSERT INTO CustomerTbl (CustName, CustAdd, Phone) VALUES (@CustName, @CustAdd, @Phone)", Con))
                {
                    cmd.Parameters.AddWithValue("@CustName", NameTb.Text);
                    cmd.Parameters.AddWithValue("@CustAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@Phone", PhoneTb.Text);

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();

                    MessageBox.Show("Customer Successfully Added");
                    Populate();
                    AutoIdGeneration(); // Generate next ID after each insert
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            Populate();
            AutoIdGeneration();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IdTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "DELETE FROM CustomerTbl WHERE CustId = @CustId";
                using (var cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@CustId", int.Parse(IdTb.Text.Trim()));
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Customer Deleted Successfully");
                Populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && CustomerDGV.SelectedRows.Count > 0)
            {
                IdTb.Text = CustomerDGV.SelectedRows[0].Cells[0].Value.ToString();
                NameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
                AddressTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
                PhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            }
        }
        

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IdTb.Text) || string.IsNullOrWhiteSpace(NameTb.Text) || string.IsNullOrWhiteSpace(AddressTb.Text) || string.IsNullOrWhiteSpace(PhoneTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "UPDATE CustomerTbl SET CustName = @CustName, CustAdd = @CustAdd, Phone = @Phone WHERE CustId = @CustId";
                using (var cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@CustId", int.Parse(IdTb.Text.Trim()));
                    cmd.Parameters.AddWithValue("@CustName", NameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@CustAdd", AddressTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", PhoneTb.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Customer Successfully Updated");
                Populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void IdTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


