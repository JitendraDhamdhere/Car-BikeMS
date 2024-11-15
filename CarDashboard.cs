using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carandbike1
{
    public partial class CarDashboard : Form
    {
        public CarDashboard()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Car car = new Car();
            car.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customers customers = new Customers();
            customers.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rental rental = new Rental();
            rental.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Returncs ret = new Returncs();
            ret.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageUsersForm Form = new ManageUsersForm();
            Form.Show();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard board = new Dashboard();
            board.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardSelect Form = new DashboardSelect();
            Form.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Payment Form = new Payment();
            Form.Show();
        }
    }
}
