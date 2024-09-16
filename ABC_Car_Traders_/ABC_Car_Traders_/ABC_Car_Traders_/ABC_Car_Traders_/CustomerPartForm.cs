using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABC_Car_Traders_
{
    public partial class CustomerPartForm : Form
    {
        public CustomerPartForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=YOOSUF-HARIS\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

     

        private void btn_order_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your order is confirmed! You can check your order status.", "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from car_parts where PartName ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id.Text = dr["CarId"].ToString();
                lbl_name.Text = dr["PartName"].ToString();
                lbl_price.Text = dr["Price"].ToString();
                lbl_desc.Text = dr["Description"].ToString();
         
            }
            else
            {
                MessageBox.Show("Invalid Car Part ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }
    }
}
