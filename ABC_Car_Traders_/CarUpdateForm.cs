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
    public partial class CarUpdateForm : Form
    {
        public CarUpdateForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from car where car_model ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_make.Text = dr["car_make"].ToString();
                txt_model.Text = dr["car_model"].ToString();
                txt_year.Text = dr["car_year"].ToString();
                txt_price.Text = dr["car_price"].ToString();
                txt_stock.Text = dr["car_stock"].ToString();
                txt_desc.Text = dr["car_desc"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid Car ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string sqldelete;
                    sqldelete = " delete from car where car_model='" + txt_search.Text + "'";
                    SqlCommand cmd = new SqlCommand(sqldelete, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Details Deleted sucsessfully!!");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            con.Close();

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string sqlUpdate;
            sqlUpdate = "update car set car_make= '" + txt_make.Text + "',car_model='" + txt_model.Text + "', car_year='" + txt_year.Text + "' , car_price='" + txt_price.Text + "',car_stock='" + txt_stock.Text + "',car_desc='" + txt_desc.Text + "' where car_make= '" + txt_make.Text + "' ";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Succesfully", "Update Car Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void datagridcars_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                    datagridcars.DataSource = dataTable;
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
    }
}
