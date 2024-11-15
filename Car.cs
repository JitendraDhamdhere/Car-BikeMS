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
using System.Diagnostics;
using System.Security.Cryptography;
namespace carandbike1
{
    public partial class Car : Form
    {
        public Car()
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
                var cmd = new SqlCommand("SELECT MAX(RegNum) + 1 FROM CarTbl", Con);
                var result = cmd.ExecuteScalar();

                if (Convert.IsDBNull(result))
                {
                    num = 1;
                }
                else
                {
                    num = Convert.ToInt32(result);
                }

                RegNumTb.Text = num.ToString();
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
            using (var da = new SqlDataAdapter("SELECT * FROM CarTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                CarDGV.DataSource = ds.Tables[0];
            }
        }
        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BrandTb.Text) || string.IsNullOrWhiteSpace(ModelTb.Text) || string.IsNullOrWhiteSpace(PriceTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                using (var cmd = new SqlCommand("INSERT INTO CarTbl (Brand, Model, Available, Price) VALUES (@Brand, @Model, @Available, @Price)", Con))
                {
                    // Remove this line
                    // cmd.Parameters.AddWithValue("@RegNum", RegNumTb.Text);

                    cmd.Parameters.AddWithValue("@Brand", BrandTb.Text);
                    cmd.Parameters.AddWithValue("@Model", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@Available", AvailableCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Price", PriceTb.Text);

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Car Successfully Added");
                    Populate();
                    AutoIdGeneration(); // Generate next ID after each insert
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Uid_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && CarDGV.SelectedRows.Count > 0)
            {
                RegNumTb.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
                BrandTb.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
                ModelTb.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
                AvailableCb.SelectedItem = CarDGV.SelectedRows[0].Cells[3].Value.ToString();
                PriceTb.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }


        private void Car_Load(object sender, EventArgs e)
        {
            Populate();
            AutoIdGeneration();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegNumTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "DELETE FROM CarTbl WHERE RegNum = @RegNum";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@RegNum", int.Parse(RegNumTb.Text.Trim()));
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Car Deleted Successfully");
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(RegNumTb.Text) || string.IsNullOrWhiteSpace(BrandTb.Text) || string.IsNullOrWhiteSpace(ModelTb.Text) || string.IsNullOrWhiteSpace(PriceTb.Text) || AvailableCb.SelectedItem == null)
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "UPDATE CarTbl SET Brand = @Brand, Model = @Model, Available = @Available, Price = @Price WHERE RegNum = @RegNum";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@RegNum", int.Parse(RegNumTb.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Brand", BrandTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Model", ModelTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Available", AvailableCb.SelectedItem.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Price", PriceTb.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Car Successfully Updated");
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            CarDashboard main = new CarDashboard();
            main.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Populate();
        }

        private void Search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = Search.SelectedItem.ToString() == "Available" ? "Yes" : "No";
            try
            {
                Con.Open();
                string query = "SELECT * FROM CarTbl WHERE Available = @Available";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                da.SelectCommand.Parameters.AddWithValue("@Available", flag);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CarDGV.DataSource = dt;
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

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}


    







