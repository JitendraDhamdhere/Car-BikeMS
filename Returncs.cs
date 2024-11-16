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
    public partial class Returncs : Form
    {
        public Returncs()
        {
            InitializeComponent();
        }
        private SqlConnection Con = new SqlConnection(ConnectionString.DBConn);
        private void AutoIdGeneration()
        {
            int num = 0;
            try
            {
                Con.Open();
                var cmd = new SqlCommand("SELECT MAX(ReturnId) + 1 FROM ReturnTbl", Con);
                var result = cmd.ExecuteScalar();

                if (Convert.IsDBNull(result))
                {
                    num = 1;
                }
                else
                {
                    num = Convert.ToInt32(result);
                }

                IdTb.Text = num.ToString();
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
        private void populate()
        {
            using (var da = new SqlDataAdapter("SELECT * FROM RentalTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                RentDGV.DataSource = ds.Tables[0];
            }
            using (var da = new SqlDataAdapter("SELECT * FROM ReturnTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                ReturnDGV.DataSource = ds.Tables[0];
            }
        }

        private void populateRet()
        {
            using (var da = new SqlDataAdapter("SELECT * FROM ReturnTbl", Con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                ReturnDGV.DataSource = ds.Tables[0];
            }
        }

        private void Deleteonreturn()
        {
            if (RentDGV.SelectedRows.Count > 0)
            {

                using (var cmd = new SqlCommand("DELETE FROM RentalTbl WHERE CarReg = @RentId", Con))
                {
                    cmd.Parameters.AddWithValue("@RentId", CarIdTb.Text);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                }

                populate();
                AutoIdGeneration();
            }
        }
        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RentDGV.SelectedRows.Count > 0)
            {
                CarIdTb.Text = RentDGV.SelectedRows[0].Cells[0].Value.ToString();
                CustNameTb.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
                ReturnDate.Text = RentDGV.SelectedRows[0].Cells[4].Value.ToString();

                DateTime rentReturnDate = Convert.ToDateTime(ReturnDate.Text);
                DateTime currentDate = DateTime.Now;
                TimeSpan timeDiff = currentDate - rentReturnDate;

                int delayDays = Math.Max(0, (int)timeDiff.TotalDays);

                DelayTb.Text = delayDays > 0 ? delayDays.ToString() : "No Delay";
                FineTb.Text = delayDays > 0 ? (delayDays * 250).ToString() : "0";
            }
        }

        private void Returncs_Load(object sender, EventArgs e)
        {
            populate();
            populateRet();
            AutoIdGeneration();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            CarDashboard form = new CarDashboard();

            form.Show();
        }





        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IdTb.Text) || string.IsNullOrWhiteSpace(CustNameTb.Text) ||
       string.IsNullOrWhiteSpace(FineTb.Text) || string.IsNullOrWhiteSpace(DelayTb.Text) ||
       string.IsNullOrWhiteSpace(ReturnDate.Text))
            {
                MessageBox.Show("Missing information");
                return;
            }

            try
            {
                // Insert return details into ReturnTbl
                using (var insertCmd = new SqlCommand("INSERT INTO ReturnTbl (CarReg, CustName, ReturnDate, Delay, Fine) VALUES (@CarId, @CustName, @ReturnDate, @Delay, @Fine)", Con))
                {
                    insertCmd.Parameters.AddWithValue("@CarId", CarIdTb.Text);
                    insertCmd.Parameters.AddWithValue("@CustName", CustNameTb.Text);
                    insertCmd.Parameters.AddWithValue("@ReturnDate", DateTime.Parse(ReturnDate.Text)); // Ensure DateTime parsing
                    insertCmd.Parameters.AddWithValue("@Delay", int.Parse(DelayTb.Text)); // Convert Delay to integer
                    insertCmd.Parameters.AddWithValue("@Fine", decimal.Parse(FineTb.Text)); // Convert Fine to decimal

                    Con.Open(); // Open connection
                    insertCmd.ExecuteNonQuery(); // Execute INSERT query
                    Con.Close(); // Close connection after execution
                }

                // Update availability in CarTbl
                using (var updateCmd = new SqlCommand("UPDATE CarTbl SET Available = 'YES' WHERE RegNum = @CarId", Con))
                {
                    updateCmd.Parameters.AddWithValue("@CarId", CarIdTb.Text);

                    Con.Open(); // Reopen connection for UPDATE
                    updateCmd.ExecuteNonQuery(); // Execute UPDATE query
                    Con.Close(); // Close connection after execution
                }

                // Notify success and refresh UI
                MessageBox.Show("Car Successfully Returned");
                populate(); // Refresh data grid
               Deleteonreturn(); // Optional: Implement if needed
                AutoIdGeneration(); // Reset ID generation
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Ensure connection is closed even if an error occurs
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }
    }
}


