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
    public partial class UpdatePartForm : Form
    {
        public UpdatePartForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void UpdatePartForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from parts where part_name ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_name.Text = dr["part_name"].ToString();
                txt_price.Text = dr["part_price"].ToString();
                txt_stock.Text = dr["part_stock"].ToString();
                txt_desc.Text = dr["part_desc"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid Part Name", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                    sqldelete = " delete from parts where part_name='" + txt_search.Text + "'";
                    SqlCommand cmd = new SqlCommand(sqldelete, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Parts Details Deleted sucsessfully!!");
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
            sqlUpdate = "update parts set part_name '" + txt_name.Text + "',part_price='" + txt_price.Text + "', part_stock='" + txt_stock.Text + "' , part_desc='" + txt_desc.Text + "' where part_name '" + txt_name.Text + "' )";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Succesfully", "Update Parts Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void datagridcarparts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string query = "SELECT * FROM parts";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridcarparts.DataSource = dataTable;
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
