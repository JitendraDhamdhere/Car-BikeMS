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
    public partial class DashboardSelect : Form
    {
        public DashboardSelect()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            CarDashboard form3 = new CarDashboard();
            form3.Show();   // Show the next form (Form2)
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            BikeDashboard form4 = new BikeDashboard();
            form4.Show();   // Show the next form (Form2)
            this.Hide();
        }
    }
}

