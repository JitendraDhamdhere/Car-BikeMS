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

namespace carandbike1
{
    public partial class BikeRental : Form
    {
        private readonly string _connectionString = ConnectionString.DBConn;

        public BikeRental()
        {
            InitializeComponent();

        }
        private SqlConnection Con = new SqlConnection(ConnectionString.DBConn);
        private void ExecuteQuery(Action<SqlCommand> queryAction, string sql)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        con.Open();
                        queryAction(cmd);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        private DataTable ExecuteQueryWithResult(string sql, Action<SqlCommand> parameterAction = null)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    if (parameterAction != null) parameterAction(cmd);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        try
                        {
                            con.Open();
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                        return dt;
                    }
                }
            }
        }

        private void fillcombo()
        {
            var dt = ExecuteQueryWithResult("SELECT RegNum FROM BikeTbl");
            if (dt.Rows.Count > 0)
            {
                BikeRegCb.ValueMember = "RegNum";
                BikeRegCb.DataSource = dt;
            }
        }

        private void fillCustomer()
        {
            var dt = ExecuteQueryWithResult("SELECT CustId FROM BikeCustomerTbl");
            if (dt.Rows.Count > 0)
            {
                BCustCb.ValueMember = "CustId";
                BCustCb.DataSource = dt;
            }
        }

        private void populate()
        {
            var dt = ExecuteQueryWithResult("SELECT * FROM BikeRentalTbl");
            BRentDGV.DataSource = dt;
        }

        private void fetchCustName()
        {
            var dt = ExecuteQueryWithResult(
                "SELECT CustName FROM BikeCustomerTbl WHERE CustId = @CustId",
                cmd => cmd.Parameters.AddWithValue("@CustId", BCustCb.SelectedValue)
            );

            if (dt.Rows.Count > 0)
            {
                BCustNameTb.Text = dt.Rows[0]["CustName"].ToString();
            }
        }

        private void UpdateonRent(bool isAvailable)
        {
            var availability = isAvailable ? "Yes" : "No";
            ExecuteQuery(
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@RegNum", BikeRegCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@Available", availability);
                    cmd.ExecuteNonQuery();
                },
                "UPDATE BikeTbl SET Available = @Available WHERE RegNum = @RegNum"
            );
        }

        private void AutoIdGeneration()
        {
            var dt = ExecuteQueryWithResult("SELECT ISNULL(MAX(RentId), 0) + 1 AS NewId FROM BikeRentalTbl");
            if (dt.Rows.Count > 0)
            {
                BIdTb.Text = dt.Rows[0]["NewId"].ToString();
            }
        }
        private void BikeRental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            populate();
            AutoIdGeneration();
        }

        private void BikeRegCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BIdTb.Text) || string.IsNullOrWhiteSpace(BCustCb.Text) || string.IsNullOrWhiteSpace(BFeesTb.Text))
            {
                MessageBox.Show("Missing information");
                return;
            }

            ExecuteQuery(
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@RegNum", BikeRegCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CustName", BCustNameTb.Text);
                    cmd.Parameters.AddWithValue("@RentDate", BRentDate.Value);
                    cmd.Parameters.AddWithValue("@ReturnDate", BReturnDate.Value);
                    cmd.Parameters.AddWithValue("@Fees", decimal.Parse(BFeesTb.Text));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Car Successfully Rented");
                    UpdateonRent(false); // Mark as rented
                    populate();
                    AutoIdGeneration();
                },
                "INSERT INTO BikeRentalTbl (CarReg, CustName, RentDate, ReturnDate, RentFee) VALUES (@RegNum, @CustName, @RentDate, @ReturnDate, @Fees)"
            );
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BIdTb.Text) || string.IsNullOrWhiteSpace(BCustNameTb.Text) || string.IsNullOrWhiteSpace(BFeesTb.Text))
            {
                MessageBox.Show("Missing information");
                return;
            }

            ExecuteQuery(
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@RentId", int.Parse(BIdTb.Text));
                    cmd.Parameters.AddWithValue("@RegNum", BikeRegCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CustName", BCustNameTb.Text);
                    cmd.Parameters.AddWithValue("@RentDate", BRentDate.Value);
                    cmd.Parameters.AddWithValue("@ReturnDate", BReturnDate.Value);
                    cmd.Parameters.AddWithValue("@Fees", decimal.Parse(BFeesTb.Text));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Rental record updated successfully.");
                    populate();
                },
                "UPDATE BikeRentalTbl SET CarReg = @RegNum, CustName = @CustName, RentDate = @RentDate, ReturnDate = @ReturnDate, RentFee = @Fees WHERE RentId = @RentId"
            );
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BIdTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            ExecuteQuery(
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@RentId", int.Parse(BIdTb.Text));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Rental Deleted Successfully");
                    UpdateonRent(true); // Mark as available
                    populate();
                },
                "DELETE FROM BikeRentalTbl WHERE RentId = @RentId"
            );
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            BikeDashboard form = new BikeDashboard();
            form.Show();
        }

        private void BCustCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchCustName();
        }
    }
}
        
            