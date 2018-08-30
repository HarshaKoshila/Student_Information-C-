using KDUInfoApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text.RegularExpressions;

namespace StudentsApp
{
    public partial class SickChrtForm1 : Form
    {
        DBAccess ob = new DBAccess();
        byte[] imgHD;
        string imgHDLoc;
        String SNO;
        String ID;
        String name = " ";
        String intake = " ";
        int x = 0;//for pdf file name that user save
        public SickChrtForm1()
        {
            InitializeComponent();
            FillGrid();
            btnUpdate.Enabled = false;
            btnAddSick.Enabled = true;
        }

        public void FillGrid()
        {
            DataSet ds = ob.getSickChrt();
            dgvSickChrt.DataSource = ds.Tables["SickChartOfKDU"].DefaultView;
        }

        public void FillGridFromSNO(String SNO)
        {
            DataSet ds = ob.getSickChrtSNO(SNO);
            dgvSickChrt.DataSource = ds.Tables["SNOSickChartOfKDU"].DefaultView;
        }

        public void FillGridFromIntake(String intake)
        {
            DataSet ds = ob.getSickChrtFromIntake(intake);
            dgvSickChrt.DataSource = ds.Tables["Intake_SickChartOfKDU"].DefaultView;
        }

        public bool isEmptyAll()
        {
            if ((txtCategSick.Text).Equals("") || (txtIntkSick.Text).Equals("") || (txtNameSick.Text).Equals("") || (txtRnkSick.Text).Equals("") || (txtSicknssSick.Text).Equals("") || (txtSNOsick.Text).Equals("") || (txtTroopSick.Text).Equals("") || (txtYrSick.Text).Equals(""))
                return true;
            else
                return false;
        }

        public void Clear()
        {
            txtCategSick.Text = "";
            txtIntkSick.Text = "";
            txtNameSick.Text = "";
            txtRnkSick.Text = "";
            txtSicknssSick.Text = "";
            txtSNOsick.Text = "";
            txtTroopSick.Text = "";
            txtYrSick.Text = "";
        }

        private void btnAddSick_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                String date1 = dtpDateSick.Value.ToShortDateString();
                if (ob.addSickChrt(txtSNOsick.Text, txtRnkSick.Text, txtTroopSick.Text, txtYrSick.Text, txtSicknssSick.Text, txtCategSick.Text, date1, imgHD))
                {
                    MessageBox.Show("Successful added to db ");
                    FillGrid();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Fail added");
                }
            }
        }

        private void btnUploadSick_Click(object sender, EventArgs e)
        {
            imgHD = null;
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Only. |*.jpg; *.jpeg; *.png; *.gif; ";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                imgHDLoc = openFD.FileName.ToString();
                picBoxHdImgAtend.ImageLocation = imgHDLoc;
                lblhd.Text = "";
                try
                {
                    //-----------For img
                    FileStream stream3 = new FileStream(imgHDLoc, FileMode.Open, FileAccess.Read);
                    BinaryReader brs3 = new BinaryReader(stream3);
                    imgHD = brs3.ReadBytes((int)stream3.Length);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Images aren't set correctly ");
                }
            }
            else
                lblhd.Text = "No images";
        }

        public bool CheckvalidSNO(String inputTxtBox)  //Check whether SNO exist in system
        {
            bool status = false;
            List<Cadet> list = ob.getAllCadet();
            foreach (var value in list)
            {
                if (value.SNO.Replace(" ", String.Empty).Equals(inputTxtBox.Replace(" ", String.Empty)))
                {
                    name = value.name;  //Used Relace method for Removing white spaces 
                    intake = value.intake;
                    status = true;
                }
            }

            return status;
        }

        private void txtSNOsick_Leave(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvSickChrt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Enabled = false;
            btnAddSick.Enabled = true;
        }

        private void dgvSickChrt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = dgvSickChrt.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(e);
            }
            if (e.ColumnIndex == dgvSickChrt.Columns["ColumnDelete"].Index)
            {
                if (MessageBox.Show("Delete selected Job Post?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                { 
                    if (ob.deleteSickChrt(ID))
                    {
                        MessageBox.Show("Successfully Deleted");
                        Clear();
                        FillGrid();
                    }
                    else
                    {
                        MessageBox.Show("Error occured while deleting");
                    }
                }
            }
            if (e.ColumnIndex == dgvSickChrt.Columns["ColumnView"].Index)
            {
                FormImgViewer form = new FormImgViewer(ob.getSickImg(ID));
                form.ShowDialog();
            }

            if (e.ColumnIndex == dgvSickChrt.Columns["ColumnSave"].Index)
            {
                string folderPath = "";
                FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                if (directchoosedlg.ShowDialog() == DialogResult.OK)
                {
                    folderPath = directchoosedlg.SelectedPath;
                }
                //If User select the path....
                if (!(folderPath.Equals("")))
                {
                    SNO = dgvSickChrt.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
                    CheckvalidSNO(SNO);

                    Document document = new Document();
                    try
                    {
                        PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + SNO + "_" + (++x) + ".pdf", FileMode.Create));
                        document.Open();

                        string header = @"Sick Chart";
                        Paragraph p0 = new Paragraph();
                        p0.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLUE);
                        p0.Add(header);
                        document.Add(p0);

                        string header1 = @"Issue Date  :" + DateTime.Now.ToString();
                        Paragraph p1 = new Paragraph();
                        p1.Font = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLUE);
                        p1.Add(header1);
                        document.Add(p1);

                        Paragraph p2 = new Paragraph("SNO        :" + SNO);
                        p2.Font = FontFactory.GetFont(FontFactory.HELVETICA, 15f, BaseColor.BLACK);
                        document.Add(p2);

                        Paragraph p3 = new Paragraph("Name      :" + name);
                        p3.Font = FontFactory.GetFont(FontFactory.HELVETICA, 15f, BaseColor.BLACK);
                        document.Add(p3);

                        Paragraph p4 = new Paragraph("Intake     :" + intake);
                        p4.Font = FontFactory.GetFont(FontFactory.HELVETICA, 15f, BaseColor.BLACK);
                        document.Add(p4);

                        Paragraph p5 = new Paragraph(" ");
                        p5.Font = FontFactory.GetFont(FontFactory.HELVETICA, 15f, BaseColor.BLACK);
                        document.Add(p5);

                        //Read all data of DataGridView
                        for (int j = 4; j < dgvSickChrt.Columns.Count; j++)
                        {
                            Paragraph p = new Paragraph(dgvSickChrt.Columns[j].HeaderText + "     :" + dgvSickChrt.Rows[e.RowIndex].Cells[j].Value);
                            p.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.BLACK);
                            document.Add(p);
                        }

                        document.Close();
                        MessageBox.Show("Successfully saved ");
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Pdf file already exist with " + SNO + " SNO  (Please Delete it or Select another path)");
                    }

                }
            }
        }

        private void dgvSickChrt_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = dgvSickChrt.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            txtSNOsick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
            if (CheckvalidSNO(txtSNOsick.Text))
            {
                txtNameSick.Text = name;
                txtIntkSick.Text = intake;
            }
            txtRnkSick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvRank"].Value.ToString();
            txtTroopSick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvTrp"].Value.ToString();
            dtpDateSick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvDate"].Value.ToString();
            txtYrSick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvYear"].Value.ToString();
            txtSicknssSick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvSick"].Value.ToString();
            txtCategSick.Text = dgvSickChrt.Rows[e.RowIndex].Cells["dgvCat"].Value.ToString(); 

            btnUpdate.Enabled = true;
            btnAddSick.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                if (ob.updateSickChrt(ID, txtRnkSick.Text, txtTroopSick.Text, txtYrSick.Text, txtSicknssSick.Text, txtCategSick.Text, dtpDateSick.Value.ToShortDateString(),imgHD))
                {
                    MessageBox.Show("Successfully Updated");
                    Clear();
                    FillGrid();
                }
                else
                {
                    MessageBox.Show("Error occured");
                }
                
                
                btnUpdate.Enabled = false;
                btnAddSick.Enabled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillGridFromSNO(txtSearch.Text);
        }

        private void txtNameSick_MouseClick(object sender, MouseEventArgs e)
        {
            if (CheckvalidSNO(txtSNOsick.Text))
            {
                txtNameSick.Text = name;
                txtIntkSick.Text = intake;
            }
            else
                MessageBox.Show("Invalid SNO ");
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            OfficerForm ob = new OfficerForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchForm1 ob = new SearchForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            this.Hide();
            PTTestForm1 ob = new PTTestForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnBlackbook_Click(object sender, EventArgs e)
        {
            this.Hide();
            AttendSheetForm1 ob = new AttendSheetForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }

        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewCadet ob = new AddNewCadet();
            ob.ShowDialog();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgvSickChrt.ColumnCount - 3); //avoiding unnessary column
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 95;
            pdfTable.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Topic on top of pdf
            string header = @"       Sick Chart";
            Paragraph p0 = new Paragraph();
            p0.Font = FontFactory.GetFont(FontFactory.HELVETICA, 25f, BaseColor.BLUE);
            p0.Add(header);

            string header1 = @"                          Issue Date  :" + DateTime.Now.ToString();
            Paragraph p1 = new Paragraph();
            p1.Font = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLUE);
            p1.Add(header1);

            Paragraph p5 = new Paragraph(" ");
            p5.Font = FontFactory.GetFont(FontFactory.HELVETICA, 15f, BaseColor.BLACK);

            //Add Header row by avoiding unnessary column
            for (int j = 3; j < dgvSickChrt.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dgvSickChrt.Columns[j].HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dgvSickChrt.Rows)
            {
                for (int j = 3; j < dgvSickChrt.Columns.Count; j++)
                {
                    pdfTable.AddCell(row.Cells[j].Value.ToString());
                }
            }

            //Exporting to PDF
            string folderPath = "";
            FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
            if (directchoosedlg.ShowDialog() == DialogResult.OK)
            {
                folderPath = directchoosedlg.SelectedPath;
            }
            ///////////  
            if (!(folderPath.Equals("")))
            {
                try
                {   //Add what we created things into document is defined pdfDoc
                    String date = DateTime.Now.ToString().Replace(" ", String.Empty).Replace(":", "_").Replace("/", "_"); //Covert to date string as a valid pdf name
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(folderPath + "/" + "PDF_" + date + ".pdf", FileMode.Create));
                    pdfDoc.Open();
                    pdfDoc.Add(p0);
                    pdfDoc.Add(p1);
                    pdfDoc.Add(p5);
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();

                    MessageBox.Show("Successfully saved ");
                }
                catch (IOException)
                {
                    MessageBox.Show("Pdf file already exist same name(Please Delete it or Select another path)");
                }
            }


        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbSearch.SelectedIndex.Equals(0))
                {
                    FillGridFromSNO(Regex.Replace(txtSearch.Text, @"\s", ""));
                }
                if (cmbSearch.SelectedIndex.Equals(1))
                {
                    FillGridFromIntake(Regex.Replace(txtSearch.Text, @"\s", ""));
                }
            }
            catch (NullReferenceException) { MessageBox.Show("First Select search criteria"); }
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
