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
    public partial class CustomerUpdateForm : Form
    {
        public CustomerUpdateForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void CustomerUpdateForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from customer where customer_id ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_id.Text = dr["customer_id"].ToString();
                txt_name.Text = dr["name"].ToString();
                txt_no.Text = dr["telephone"].ToString();
                date_dob.Text = dr["dob"].ToString();
                txt_add.Text = dr["address"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid Customer ID", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                    sqldelete = " delete from customer where customer_id='" + txt_search.Text + "'";
                    SqlCommand cmd = new SqlCommand(sqldelete, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Details Deleted sucsessfully!!");
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
            sqlUpdate = "update customer set name='" + txt_name.Text + "', telephone='" + txt_no.Text + "' , address='" + txt_add.Text + "', dob='"+date_dob.Value+"' where customer_id '" + txt_id.Text + "')";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Succesfully", "Update Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void datagridcustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string query = "SELECT * FROM customer";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridcustomers.DataSource = dataTable;
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
