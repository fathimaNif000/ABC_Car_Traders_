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
    public partial class AddCarForm : Form
    {
        public AddCarForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=MUHAMMEDH\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

        private void btn_image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image file|*jpg;*jpeg", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic_car.Image = Image.FromFile(ofd.FileName);
                    txt_image.Text = ofd.FileName;

                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlinsert;
                sqlinsert = "insert into car (car_make, car_model, car_year, car_price, car_stock, car_desc, car_image ) values ('" + txt_make.Text + "','" + txt_model.Text + "','" + txt_year.Text + "','" + txt_price.Text + "','" + txt_stock.Text + "','" + txt_desc.Text + "','" + txt_image.Text + "')";
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
