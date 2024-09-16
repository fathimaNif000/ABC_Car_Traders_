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

namespace ABC_Car_Traders_
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=FATHI\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void btn_login_Click(object sender, EventArgs e)
        {
            string email, password;

            email = txt_email.Text;
            password = txt_pw.Text;

            string userType = "";

            if (chkbox_admin.Checked)
            {
                userType = "users";
            }
            else if (chkbox_customer.Checked)
            {
                userType = "customer";
            }

            string query = "";
            if (userType == "users")
            {
                query = "SELECT * FROM users WHERE email='" + txt_email.Text + "' AND password='" + txt_pw.Text + "'";
            }
            else if (userType == "customer")
            {
                query = "SELECT * FROM customer WHERE email='" + txt_email.Text + "' AND password='" + txt_pw.Text + "'";
            }

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                email = txt_email.Text;
                password = txt_pw.Text;
                MessageBox.Show("Login Successfully");

                if (userType == "users")
                {
                    AdminDashForm adminDashboard = new AdminDashForm();
                    this.Hide();
                    adminDashboard.Show();
                }
                else if (userType == "customer")
                {
                    CustomerDashForm cd = new CustomerDashForm();
                    this.Hide();
                    cd.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid Email or Password");
            }
            con.Close();
        }

        private void linklbl_signup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            this.Hide();
            reg.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
