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
    public partial class CustomerCarForm : Form
    {
        public CustomerCarForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=YOOSUF-HARIS\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

     

        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from cars where Model ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["Make"].ToString();
                textBox5.Text = dr["Model"].ToString();
                textBox3.Text = dr["Year"].ToString();
                textBox4.Text = dr["Price"].ToString();
                textBox2.Text = dr["Description"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid Car ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your order is confirmed! You can check your order status.", "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
