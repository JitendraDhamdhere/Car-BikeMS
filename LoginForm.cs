using Microsoft.Data.SqlClient;
using System.Data;

namespace carandbike1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(ConnectionString.DBConn);

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) FROM UserTbl WHERE Uname = @username AND Upass = @password";
            string username = Uname.Text.Trim();
            string password = PassTb.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.DBConn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int userCount = (int)cmd.ExecuteScalar();

                        if (userCount == 1)
                        {
                            MessageBox.Show("Login Successful!");

                            DashboardSelect form = new DashboardSelect();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uname.Text = " ";
            PassTb.Text = " ";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }
    }
}
