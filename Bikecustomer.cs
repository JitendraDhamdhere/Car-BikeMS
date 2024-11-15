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
using static Azure.Core.HttpHeader;

namespace carandbike1
{
    public partial class Bikecustomer : Form
    {
        public Bikecustomer()
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
                var cmd = new SqlCommand("SELECT ISNULL(MAX(CustId), 0) + 1 FROM BikeCustomerTbl", Con);
                var result = cmd.ExecuteScalar();

                num = Convert.ToInt32(result);
                BIdTb.Text = num.ToString(); // Display the generated ID
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
            using (var da = new SqlDataAdapter("SELECT * FROM BikeCustomerTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                BCustomerDGV.DataSource = ds.Tables[0];
            }
        }



        private void Bikecustomer_Load(object sender, EventArgs e)
        {
            Populate();
            AutoIdGeneration();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(BNameTb.Text) || string.IsNullOrWhiteSpace(BAddressTb.Text) || string.IsNullOrWhiteSpace(BPhoneTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                using (var cmd = new SqlCommand("INSERT INTO BikeCustomerTbl (CustName, CustAdd, Phone) VALUES (@CustName, @CustAdd, @Phone)", Con))
                {
                    cmd.Parameters.AddWithValue("@CustName", BNameTb.Text);
                    cmd.Parameters.AddWithValue("@CustAdd", BAddressTb.Text);
                    cmd.Parameters.AddWithValue("@Phone", BPhoneTb.Text);

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

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(BIdTb.Text) || string.IsNullOrWhiteSpace(BNameTb.Text) || string.IsNullOrWhiteSpace(BAddressTb.Text) || string.IsNullOrWhiteSpace(BPhoneTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "UPDATE BikeCustomerTbl SET CustName = @CustName, CustAdd = @CustAdd, Phone = @Phone WHERE CustId = @CustId";
                using (var cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@CustId", int.Parse(BIdTb.Text.Trim()));
                    cmd.Parameters.AddWithValue("@CustName", BNameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@CustAdd", BAddressTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", BPhoneTb.Text.Trim());
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
        

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(BIdTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "DELETE FROM BikeCustomerTbl WHERE CustId = @CustId";
                using (var cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@CustId", int.Parse(BIdTb.Text.Trim()));
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

        private void BCustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BIdTb.Text = BCustomerDGV.SelectedRows[0].Cells[0].Value.ToString();
            BNameTb.Text = BCustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            BAddressTb.Text = BCustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            BPhoneTb.Text = BCustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            BikeDashboard main = new BikeDashboard();
            main.Show();
        }
    }
}


