using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders_
{
    public partial class AdminDashForm : Form
    {
        public AdminDashForm()
        {
            InitializeComponent();
        }

        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

    

        private void btn_dash_Click(object sender, EventArgs e)
        {
            loadform(new DashboardForm());
        }
        private void btn_partmenu_Click(object sender, EventArgs e)
        {
            loadform(new AddCarForm());
        }

        private void btn_customers_Click(object sender, EventArgs e)
        {
            loadform(new CarUpdateForm());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            this.Hide();
            lf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new AddPartForm());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadform(new UpdatePartForm());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadform(new CustomerUpdateForm());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadform(new AdminOrderForm());
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
