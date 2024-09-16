using iTextSharp.text.pdf;
using iTextSharp.text;
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
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=YOOSUF-HARIS\\SQLEXPRESS;Initial Catalog=ABC_Car_Traders;Integrated Security=True");
        private void btn_cars_Click(object sender, EventArgs e)
        {

        }


        private void btn_sales_Click(object sender, EventArgs e)
        {

        }

        private void chart_rev2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_cars_Click_1(object sender, EventArgs e)
        {
            string sqlsearch;

            // If textBox1 is empty, fetch all customer details
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                sqlsearch = "SELECT * FROM customer";
            }
            else
            {
                // If textBox1 is not empty, search by CustomerID
                sqlsearch = "SELECT * FROM customer WHERE CustomerID ='" + textBox1.Text + "'";
            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                sqlsearch = "SELECT * FROM cars";
            }
            else
            {
                sqlsearch = "SELECT * FROM cars WHERE CarID ='" + textBox3.Text + "'";
            }

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

        private void btn_parts_Click(object sender, EventArgs e)
        {
            string sqlsearch;
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                sqlsearch = "SELECT * FROM car_parts";
            }
            else
            {
                sqlsearch = "SELECT * FROM car_parts WHERE PartID ='" + textBox2.Text + "'";
            }

            SqlCommand cmd = new SqlCommand(sqlsearch, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Data Found!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 || dataGridView2.Rows.Count > 0 || dataGridView3.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (.pdf)|*.pdf";
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

                                // Function to add a DataGridView to the PDF
                                void AddDataGridToPDF(DataGridView dataGridView, string title)
                                {
                                    if (dataGridView.Rows.Count > 0)
                                    {
                                        PdfPTable pTable = new PdfPTable(dataGridView.Columns.Count);
                                        pTable.DefaultCell.Padding = 2;
                                        pTable.WidthPercentage = 100;
                                        pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                                        pTable.DefaultCell.BorderWidth = 1;

                                        // Adding a heading for each DataGridView section
                                        PdfPCell headerCell = new PdfPCell(new Phrase(title, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD)));
                                        headerCell.Colspan = dataGridView.Columns.Count;
                                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        headerCell.BackgroundColor = new BaseColor(0, 120, 215);
                                        headerCell.Padding = 10;
                                        pTable.AddCell(headerCell);

                                        // Adding column headers
                                        foreach (DataGridViewColumn column in dataGridView.Columns)
                                        {
                                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                            cell.BackgroundColor = new BaseColor(240, 240, 240);
                                            pTable.AddCell(cell);
                                        }

                                        // Adding rows
                                        foreach (DataGridViewRow row in dataGridView.Rows)
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
                                        pdfDoc.Add(new Paragraph("\n")); // Add space between tables
                                    }
                                }

                                // Add all three DataGridViews to the PDF
                                AddDataGridToPDF(dataGridView1, "Grid 1 Details");
                                AddDataGridToPDF(dataGridView2, "Grid 2 Details");
                                AddDataGridToPDF(dataGridView3, "Grid 3 Details");

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



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
