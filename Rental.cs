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
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Drawing.Text;


namespace carandbike1
{
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(ConnectionString.DBConn);
        private void fillcombo()
        {
            Con.Open();
            string query = "select RegNum from CarTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(rdr);
            CarRegCb.ValueMember = "RegNum";
            CarRegCb.DataSource = dt;
            Con.Close();
        }

        // Method to fill customer combo box
        private void fillCustomer()
        {
            Con.Open();
            string query = "select CustId from CustomerTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustCb.ValueMember = "CustId";
            CustCb.DataSource = dt;
            Con.Close();
        }

        // Method to populate the rental data grid view
        private void populate()
        {
            Con.Open();
            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        // Method to fetch customer name based on customer ID
        private void fetchCustName()
        {
            Con.Open();
            string query = "select * from CustomerTbl where CustId=" + CustCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }

        // Method to update car availability after renting
        private void UpdateonRent()
        {
            Con.Open();
            string query = "update CarTbl set Available='No' where RegNum='" + CarRegCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        // Method to update car availability after rental deletion
        private void UpdateonRentDelete()
        {
            Con.Open();
            string query = "update CarTbl set Available='Yes' where RegNum='" + CarRegCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        // Method to generate the next rental ID
        private void AutoIdGeneration()
        {
            int num = 0;
            try
            {
                Con.Open();
                string query = "SELECT MAX(RentId) + 1 FROM RentalTbl";
                SqlCommand cmd = new SqlCommand(query, Con);
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
        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            populate();
            AutoIdGeneration();
        }

        private void CarRegCb_SelectionChangeCommitted(object sender, EventArgs e)

        {

        }
        private void CustCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           if (IdTb.Text == "" || CustNameTb.Text == "" || FeesTb.Text == "")
    {
        MessageBox.Show("Missing information");
    }
    else
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.DBConn))
            {
                con.Open();
                string query = "INSERT INTO RentalTbl (CarReg, CustName, RentDate, ReturnDate, RentFee) VALUES (@RegNum, @CustName, @RentDate, @ReturnDate, @Fees)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RegNum", CarRegCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CustName", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@RentDate", RentDate.Text);
                cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Text);
                cmd.Parameters.AddWithValue("@Fees", FeesTb.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Car Successfully Rented");
            }
            UpdateonRent();
            populate();
            AutoIdGeneration(); // Generate next ID after adding rental
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardSelect form = new DashboardSelect();
            form.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "DELETE FROM RentalTbl WHERE RentId=@RentId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@RentId", IdTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental Deleted Successfully");
                    Con.Close();
                    populate();
                    UpdateonRentDelete(); // Restore car availability after deleting rental
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && RentDGV.SelectedRows.Count > 0)
            {
                IdTb.Text = RentDGV.SelectedRows[0].Cells[0].Value.ToString();
                CarRegCb.SelectedValue = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
                FeesTb.Text = RentDGV.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void CarRegCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IdTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IdTb.Text) || string.IsNullOrWhiteSpace(CustNameTb.Text) || string.IsNullOrWhiteSpace(FeesTb.Text))
            {
                MessageBox.Show("Missing information");
                return;
            }

            try
            {
                Con.Open();
                string query = "UPDATE RentalTbl SET CarReg = @RegNum, CustName = @CustName, RentDate = @RentDate, ReturnDate = @ReturnDate, RentFee = @Fees WHERE RentId = @RentId";
                SqlCommand cmd = new SqlCommand(query, Con);

                cmd.Parameters.AddWithValue("@RentId", int.Parse(IdTb.Text));
                cmd.Parameters.AddWithValue("@RegNum", CarRegCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CustName", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@RentDate", RentDate.Value); // Use DateTime value instead of text
                cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value); // Use DateTime value instead of text
                cmd.Parameters.AddWithValue("@Fees", decimal.Parse(FeesTb.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Rental record updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                Con.Close();
                populate(); // Refresh the data grid view
            }
        }
    }
}