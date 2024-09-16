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
    public partial class CarUpdateForm : Form
    {
        public CarUpdateForm()
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
                txt_make.Text = dr["Make"].ToString();
                txt_model.Text = dr["Model"].ToString();
                txt_year.Text = dr["Year"].ToString();
                txt_price.Text = dr["Price"].ToString();
                txt_desc.Text = dr["Description"].ToString();
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
                    sqldelete = " delete from cars where Model='" + txt_search.Text + "'";
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
            sqlUpdate = "update cars set Make= '" + txt_make.Text + "',Model='" + txt_model.Text + "', Year='" + txt_year.Text + "' , Price='" + txt_price.Text + "',Description='" + txt_desc.Text + "' where Make= '" + txt_make.Text + "' ";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Succesfully", "Update Car Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CarUpdateForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            sqlsearch = "select * from cars where Model ='" + txt_search.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count > 0)
            {
                dataGridView3.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Data Found!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
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
                                PdfPTable pTable = new PdfPTable(dataGridView3.Columns.Count);
                                pTable.DefaultCell.Padding = 2;
                                pTable.WidthPercentage = 100;
                                pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                                pTable.DefaultCell.BorderWidth = 1;

                                // Adding a heading for the DataGridView section
                                PdfPCell headerCell = new PdfPCell(new Phrase("Details", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD)));
                                headerCell.Colspan = dataGridView3.Columns.Count;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                headerCell.BackgroundColor = new BaseColor(0, 120, 215);
                                headerCell.Padding = 10;
                                pTable.AddCell(headerCell);

                                // Adding column headers
                                foreach (DataGridViewColumn column in dataGridView3.Columns)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                    cell.BackgroundColor = new BaseColor(240, 240, 240);
                                    pTable.AddCell(cell);
                                }

                                // Adding rows
                                foreach (DataGridViewRow row in dataGridView3.Rows)
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

