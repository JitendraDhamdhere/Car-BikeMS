using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace carandbike1
{
    public partial class Bike : Form
    {
        public Bike()
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
                var cmd = new SqlCommand("SELECT MAX(RegNum) + 1 FROM BikeTbl", Con);
                var result = cmd.ExecuteScalar();

                if (Convert.IsDBNull(result))
                {
                    num = 1;
                }
                else
                {
                    num = Convert.ToInt32(result);
                }

                BikeRegNumTb.Text = num.ToString();
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
            using (var da = new SqlDataAdapter("SELECT * FROM BikeTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                BikeDGV.DataSource = ds.Tables[0];
            }
        }
        private void Bike_Load(object sender, EventArgs e)
        {
            Populate();
            AutoIdGeneration();
        }




        // Populate textboxes with selected row data from DataGridView
        private void BikeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = BikeDGV.Rows[e.RowIndex];
                BikeRegNumTb.Text = row.Cells[0].Value.ToString();
                BikeBrandTb.Text = row.Cells[1].Value.ToString();
                BikeModelTb.Text = row.Cells[2].Value.ToString();
                BikeAvailableCb.SelectedItem = row.Cells[3].Value.ToString();
                BikePriceTb.Text = row.Cells[4].Value.ToString();
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BikeBrandTb.Text) || string.IsNullOrWhiteSpace(BikeModelTb.Text) || string.IsNullOrWhiteSpace(BikePriceTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                using (var cmd = new SqlCommand("INSERT INTO BikeTbl (Brand, Model, Available, Price) VALUES (@Brand, @Model, @Available, @Price)", Con))
                {
                    // Remove this line
                    // cmd.Parameters.AddWithValue("@RegNum", RegNumTb.Text);

                    cmd.Parameters.AddWithValue("@Brand", BikeBrandTb.Text);
                    cmd.Parameters.AddWithValue("@Model", BikeModelTb.Text);
                    cmd.Parameters.AddWithValue("@Available", BikeAvailableCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Price", BikePriceTb.Text);

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Bike Successfully Added");
                    Populate();
                    AutoIdGeneration(); // Generate next ID after each insert
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BikeRegNumTb.Text) || string.IsNullOrWhiteSpace(BikeBrandTb.Text) || string.IsNullOrWhiteSpace(BikeModelTb.Text) || string.IsNullOrWhiteSpace(BikePriceTb.Text) || BikeAvailableCb.SelectedItem == null)
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "UPDATE BikeTbl SET Brand = @Brand, Model = @Model, Available = @Available, Price = @Price WHERE RegNum = @RegNum";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@RegNum", int.Parse(BikeRegNumTb.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Brand", BikeBrandTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Model", BikeModelTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Available", BikeAvailableCb.SelectedItem.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Price", BikePriceTb.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Bike Successfully Updated");
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
            if (string.IsNullOrWhiteSpace(BikeRegNumTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = "DELETE FROM BikeTbl WHERE RegNum = @RegNum";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@RegNum", int.Parse(BikeRegNumTb.Text.Trim()));
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Bike Deleted Successfully");
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
            BikeDashboard main = new BikeDashboard();
            main.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Populate();
        }
    }
}
