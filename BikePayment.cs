using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carandbike1
{
    public partial class BikePayment : Form
    {
        public BikePayment()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(ConnectionString.DBConn);
        private void AutoPaymentIdGeneration()
        {
            int nextPaymentId = 0;
            try
            {
                Con.Open();
                string query = "SELECT ISNULL(MAX(PaymentId), 0) + 1 FROM BikePaymentTbl";
                SqlCommand cmd = new SqlCommand(query, Con);
                nextPaymentId = Convert.ToInt32(cmd.ExecuteScalar());
                PaymentIdTb.Text = nextPaymentId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Payment ID: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void fillRentalId()
        {
            Con.Open();
            string query = "SELECT RentId FROM BikeRentalTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RentId", typeof(int));
            dt.Load(rdr);
            RentalIdCb.ValueMember = "RentId";
            RentalIdCb.DataSource = dt;
            Con.Close();
        }

        private void fetchRentalDetails()
        {
            try
            {
                if (Con.State == ConnectionState.Closed) // Check if the connection is already open
                {
                    Con.Open();
                }

                string query = $"SELECT BikeRentalTbl.CustName, BikeTbl.RegNum, BikeTbl.Price FROM BikeRentalTbl " +
                               $"INNER JOIN BikeTbl ON BikeRentalTbl.CarReg = BikeTbl.RegNum WHERE BikeRentalTbl.RentId = {RentalIdCb.SelectedValue}";

                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    CustNameTb.Text = rdr["CustName"].ToString();
                    CarRegTb.Text = rdr["RegNum"].ToString();
                    AmountTb.Text = rdr["Price"].ToString();
                }
                else
                {
                    MessageBox.Show("No rental details found.");
                }

                rdr.Close(); // Always close the reader
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close(); // Ensure the connection is closed
                }
            }
        }


        private void populatePayments()
        {
            Con.Open();
            string query = "SELECT * FROM BikePaymentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PaymentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void BikePayment_Load(object sender, EventArgs e)
        {
            fillRentalId();
            populatePayments();
            PaymentMethodCb.Items.Add("Cash");
            PaymentMethodCb.Items.Add("Card");
            PaymentMethodCb.Items.Add("Mobile Payment");
            PaymentMethodCb.Items.Add("Bank Transfer");
            PaymentMethodCb.SelectedIndex = 0; // Set default selection
            PopulatePayments();
            AutoPaymentIdGeneration();
        }

        private void GenerateReceiptBtn_Click(object sender, EventArgs e)
        {


            if (RentalIdCb.SelectedValue == null || CustNameTb.Text == "" || AmountTb.Text == "" || PaymentMethodCb.SelectedItem == null)
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                string query = $"INSERT INTO BikePaymentTbl (RentalId, CustId, Amount, PaymentDate, PaymentMethod) " +
                               $"VALUES ({RentalIdCb.SelectedValue}, (SELECT CustId FROM BikeCustomerTbl WHERE CustName = '{CustNameTb.Text}'), " +
                               $"{AmountTb.Text}, '{PaymentDateDtp.Value.ToString("yyyy-MM-dd")}', '{PaymentMethodCb.SelectedItem.ToString()}')";

                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Payment Successfully Recorded");

                Con.Close();

                populatePayments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DeletePaymentBtn_Click(object sender, EventArgs e)
        {


            {
                if (PaymentIdTb.Text == "")
                {
                    MessageBox.Show("Please select a payment to delete");
                    return;
                }

                try
                {
                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }

                    // Query to delete payment from the PaymentTbl
                    string query = "DELETE FROM BikePaymentTbl WHERE PaymentId = @PaymentId";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@PaymentId", PaymentIdTb.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Payment Deleted Successfully");

                    // Refresh the payment data grid (if any) after deletion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }


        }

        private void generateRecipt_Click(object sender, EventArgs e)
        {
            {
                string receipt = $"--- Receipt ---\n" +
                                 $"Customer Name: {CustNameTb.Text}\n" +
                                 $"Bike Reg: {CarRegTb.Text}\n" +
                                 $"Amount Paid: {AmountTb.Text}\n" +
                                 $"Payment Method: {PaymentMethodCb.SelectedItem.ToString()}\n" +
                                 $"Payment Date: {PaymentDateDtp.Value.ToString("yyyy-MM-dd")}\n" +
                                 $"-------------------";

                // Show the receipt in a message box for now
                MessageBox.Show(receipt, "Payment Receipt");

                // Trigger the print event
                PrintReceipt(receipt);
            }
        }
        private void PrintReceipt(string receiptContent)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(receiptContent, new Font("Arial", 12), Brushes.Black, new PointF(100, 100));
            };

            // Create a PrintDialog for user to select printer or save as PDF
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();

            BikeDashboard Form = new BikeDashboard();

            Form.Show();
        }
        private void PopulatePayments()
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                string query = "SELECT * FROM BikePaymentTbl";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                PaymentDGV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }
        private void PaymentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            {
                PaymentIdTb.Text = PaymentDGV.SelectedRows[0].Cells[0].Value.ToString();
                CustNameTb.Text = PaymentDGV.SelectedRows[0].Cells[1].Value.ToString();
                AmountTb.Text = PaymentDGV.SelectedRows[0].Cells[2].Value.ToString();
                PaymentMethodCb.SelectedItem = PaymentDGV.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void RentalIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchRentalDetails();

        }
    }
}
