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
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void CustomerCarForm_Load(object sender, EventArgs e)
        {

        }

        private void datagridcar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string query = "SELECT * FROM car";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridcar.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from car where car_model ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["car_make"].ToString();
                textBox5.Text = dr["car_model"].ToString();
                textBox3.Text = dr["car_year"].ToString();
                textBox4.Text = dr["car_price"].ToString();
                textBox2.Text = dr["car_desc"].ToString();
                pic_car.Text = dr["car_image"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid Car ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            CustomerOrderForm co = new CustomerOrderForm();
            co.Show();
            this.Hide();
        }
    }
}
