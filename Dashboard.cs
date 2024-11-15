using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace carandbike1
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(ConnectionString.DBConn);
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string querycar = "select Count(*) from CarTbl";

            SqlDataAdapter sda = new SqlDataAdapter(querycar, Con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            CarLbl.Text = dt.Rows[0][0].ToString();

            string querycust = "select Count(*) from CustomerTbl";

            SqlDataAdapter sda1 = new SqlDataAdapter(querycust, Con);

            DataTable dt1 = new DataTable();

            sda1.Fill(dt1);

            CustLbl.Text = dt1.Rows[0][0].ToString();

            string queryuser = "select Count(*) from UserTbl";

            SqlDataAdapter sda2 = new SqlDataAdapter(queryuser, Con);

            DataTable dt2 = new DataTable();

            sda2.Fill(dt2);

            UserLbl.Text = dt2.Rows[0][0].ToString();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();

            CarDashboard Form = new CarDashboard();

            Form.Show();
        }
    }
}

