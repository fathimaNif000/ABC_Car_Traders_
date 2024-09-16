using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders_
{
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=YOOSUF-HARIS\\SQLEXPRESS;Initial Catalog=yoosuf;Integrated Security=True");
        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch = "select * from add_Car where car_ID ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            cmd.Parameters.AddWithValue("@car_ID", txt_search.Text);


            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_customer_car_ID.Text = dr["car_ID"].ToString();
                txt_customer_car_name.Text = dr["car_Name"].ToString();
                txt_customer_car_model.Text = dr["model"].ToString();
                txt_customer_car_year.Text = dr["year"].ToString();
                txt_customer_car_price.Text = dr["price"].ToString();
                txt_customer_car_desc.Text = dr["description"].ToString();


                if (dr["image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])dr["image"];
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                      
                    }
                }
                else
                {
                   
                }
            }
            else
            {
                MessageBox.Show("Invalid Car ID", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_customer_car_ID.Text = "";
            txt_customer_car_name.Text = "";
            txt_customer_car_model.Text = "";
            txt_customer_car_year.Text = "";
            txt_customer_car_price.Text = "";
            txt_customer_car_desc.Text = "";
            //txt_search.Text = "";

         

        }

        private void btn_order_Click(object sender, EventArgs e)
        {

        }
    }
}
