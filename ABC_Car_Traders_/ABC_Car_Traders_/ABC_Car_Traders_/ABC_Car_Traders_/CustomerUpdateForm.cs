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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace ABC_Car_Traders_
{
    public partial class CustomerUpdateForm : Form
    {
        public CustomerUpdateForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=YOOSUF-HARIS\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");

       
        private void btn_search_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from customer where CustomerID='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_id.Text = dr["CustomerID"].ToString();
                txt_name.Text = dr["Name"].ToString();
                textno.Text = dr["Telp"].ToString();
                Nictex.Text = dr["Nic"].ToString();
                Emailt.Text = dr["Email"].ToString();
                txt_add.Text = dr["Address"].ToString();
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
                    sqldelete = " delete from customer where CustomerID='" + txt_search.Text + "'";
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
            sqlUpdate = "update customer set Name='" + txt_name.Text + "', Telp='" + textno.Text + "' , Address='" + txt_add.Text + "', Email='" + Emailt.Text + "', Nic='" + Nictex.Text + "' where Name= '" + txt_name.Text + "' ";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Succesfully", "Update Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void CustomerUpdateForm_Load(object sender, EventArgs e)
        {

        }


        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void date_dob_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_add_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_cars_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from customer where CustomerID ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Data Found!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (.pdf)|.pdf";
                save.FileName = "Report.pdf";
                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Unable to write data to disk: " + ex.Message);
                        }
                    }

                    if (!ErrorMessage)
                    {
                        try
                        {
                            using (FileStream stream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();

                                // Function to add the DataGridView to the PDF
                                PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                                pTable.DefaultCell.Padding = 2;
                                pTable.WidthPercentage = 100;
                                pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                                pTable.DefaultCell.BorderWidth = 1;

                                // Adding a heading for the DataGridView section
                                PdfPCell headerCell = new PdfPCell(new Phrase("Details", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD)));
                                headerCell.Colspan = dataGridView1.Columns.Count;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                headerCell.BackgroundColor = new BaseColor(0, 120, 215);
                                headerCell.Padding = 10;
                                pTable.AddCell(headerCell);

                                // Adding column headers
                                foreach (DataGridViewColumn column in dataGridView1.Columns)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                    cell.BackgroundColor = new BaseColor(240, 240, 240);
                                    pTable.AddCell(cell);
                                }

                                // Adding rows
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.Value != null)
                                        {
                                            pTable.AddCell(cell.Value.ToString());
                                        }
                                    }
                                }

                                pdfDoc.Add(pTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data exported successfully.", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export!", "Info");
            }
        }
    }
}
