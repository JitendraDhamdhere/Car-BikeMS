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
using System.Security.Cryptography;
namespace carandbike1
{
    public partial class ManageUsersForm : Form
    {
        public ManageUsersForm()
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
                string query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM UserTbl";
                SqlCommand cmd = new SqlCommand(query, Con);
                nextPaymentId = Convert.ToInt32(cmd.ExecuteScalar());
                Uid.Text = nextPaymentId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating User ID: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void populate()
        {

            Con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];

            Con.Close();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {

                    Con.Open();
                    string query = "insert into UserTbl(Uname,Upass) values( ' " + Uname.Text + " ',' " + Upass.Text + " ')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added");
                    Con.Close();
                    populate();
                    AutoPaymentIdGeneration();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }

                {

                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Uid.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString(); Uname.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString(); Upass.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            populate();
            AutoPaymentIdGeneration();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query = "delete from UserTbl where Id=" + Uid.Text + ";";

                    SqlCommand cmd = new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User Deleted Successfully");

                    Con.Close();

                    populate();


                    AutoPaymentIdGeneration();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void Uid_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {

                    Con.Open();
                    string query = "update UserTbl set Uname= ' " + Uname.Text + " ', Upass=' " + Upass.Text + " ' where ID =" + Uid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Updated");
                    Con.Close();
                    populate();
                    AutoPaymentIdGeneration();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            CarDashboard main = new CarDashboard();


            main.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
    }
}
