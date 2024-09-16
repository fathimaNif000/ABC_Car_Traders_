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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linklbl_signin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm lg = new LoginForm();
            this.Hide();
            lg.Show();
        }

        private void btn_reg_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlinsert;
                sqlinsert = "insert into customer (name, email, nic, dob , address, password, conpassword ) values ('" + txt_name.Text + "','" + txt_em.Text + "','" + txt_nic.Text + "','" + date_dob.Value + "','" + txt_add.Text + "','" + txt_pw.Text + "','" + txt_pwconfirm.Text + "')";
                SqlCommand cmd = new SqlCommand(sqlinsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registered sucsessfully!!");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            con.Close();
        }
    }
}
