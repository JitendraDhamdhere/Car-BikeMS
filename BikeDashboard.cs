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
    public partial class BikeDashboard : Form
    {
        public BikeDashboard()
        {
            InitializeComponent();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

            this.Hide();

            ManageUsersForm users = new ManageUsersForm();

            users.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Bike bike = new Bike();

            bike.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Bikecustomer bike = new Bikecustomer();

            bike.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            this.Hide();

            BikeRental bike = new BikeRental();

            bike.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            BikeReturn bike = new BikeReturn();

            bike.Show();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            BikePayment bike = new BikePayment();

            bike.Show();
        }
    }
}
