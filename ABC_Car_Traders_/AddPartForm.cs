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
    public partial class AddPartForm : Form
    {
        public AddPartForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void AddPartForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image file|*jpg;*jpeg", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic_carpart.Image = Image.FromFile(ofd.FileName);
                    txt_image.Text = ofd.FileName;

                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlinsert;
                sqlinsert = "insert into parts (part_name, part_desc, part_image, part_price, part_stock) values ('" + txt_name.Text + "','" + txt_description.Text + "','" + txt_image.Text + "','" + txt_price.Text + "','" + txt_stock.Text + "')";
                SqlCommand cmd = new SqlCommand(sqlinsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added sucsessfully!!");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            con.Close();
        }
    }
}
