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
    public partial class AttendSheetForm1 : Form
    {
        DBAccess ob = new DBAccess();
        byte[] imgHD;
        string imgHDLoc;
        String SNO;
        String ID;
        String name = " ";
        String intake = " ";
        int x = 0;//for pdf file name that user save
        public AttendSheetForm1()
        {
            InitializeComponent();
            btnAddAttend.Enabled = true;
            btnUpdateAtten.Enabled = false;
            FillGrid();
        }


        public void FillGrid()
        {
            DataSet ds = ob.getAttendance();
            dgvAttendance.DataSource = ds.Tables["AttendanceOfKDU"].DefaultView;
        }

        public void FillGridFromSNO(String SNO)
        {
            DataSet ds = ob.getAttendanceSNO(SNO);
            dgvAttendance.DataSource = ds.Tables["SNOAttendanceOfKDU"].DefaultView;
        }
        public void FillGridFromIntake(String intake)
        {
            DataSet ds = ob.getAttendanceFromIntake(intake);
            dgvAttendance.DataSource = ds.Tables["Intake_AttendanceOfKDU"].DefaultView;
        }

        public void Clear()
        {
            txtAcdAttend.Text = "";
            txtIntkAttend.Text = "";
            txtMilitryAttend.Text = "";
            txtNameAttend.Text = "";
            txtRnkAttend.Text = "";
            txtSNOAttend.Text = "";
            txtTrpAttend.Text = "";
        }

        public bool isEmptyAll()
        {
            if ((txtIntkAttend.Text).Equals("") || (txtAcdAttend.Text).Equals("") || (txtMilitryAttend.Text).Equals("") || (txtNameAttend.Text).Equals("") || (txtRnkAttend.Text).Equals("") || (txtSNOAttend.Text).Equals("") || (txtTrpAttend.Text).Equals(""))
                return true;
            else
                return false;
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

        private void btnAddAttend_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                if (ob.addAttend(txtSNOAttend.Text, txtRnkAttend.Text, txtTrpAttend.Text, txtMilitryAttend.Text, txtAcdAttend.Text, imgHD))
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

        private void btnUploadAtend_Click(object sender, EventArgs e)
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

        private void txtSNOAttend_Leave(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearAttend();
        }

        public void ClearAttend()
        {
            txtSNOAttend.Text = "";
            txtRnkAttend.Text = "";
            txtNameAttend.Text = "";
            txtIntkAttend.Text = "";
            txtAcdAttend.Text = "";
            txtMilitryAttend.Text = "";
            txtTrpAttend.Text = "";

            btnUpdateAtten.Enabled = false;
            btnAddAttend.Enabled = true;
        }

        private void dgvAttendance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = dgvAttendance.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(e);
            }
            if (e.ColumnIndex == dgvAttendance.Columns["ColumnView"].Index)
            {
                FormImgViewer form = new FormImgViewer(ob.getAttendImg(ID));
                form.ShowDialog();
            }

            if (e.ColumnIndex == dgvAttendance.Columns["ColumnDelete"].Index)
            {
                if (MessageBox.Show("Delete selected Job Post?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                { 
                    if (ob.deleteAttendance(ID))
                    {
                        MessageBox.Show("Successfully Deleted");
                        ClearAttend();
                        FillGrid();
                    }
                    else
                    {
                        MessageBox.Show("Error occured while deleting");
                    }
                }
            }
            if (e.ColumnIndex == dgvAttendance.Columns["ColumnSave"].Index)
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
                    SNO = dgvAttendance.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
                    CheckvalidSNO(SNO);

                    Document document = new Document();
                    try
                    {
                        PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + SNO + "_" + (++x) + ".pdf", FileMode.Create));
                        document.Open();
                        string header = @"Attendance";
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
                        for (int j = 4; j < dgvAttendance.Columns.Count; j++)
                        {
                            Paragraph p = new Paragraph(dgvAttendance.Columns[j].HeaderText + "     :" + dgvAttendance.Rows[e.RowIndex].Cells[j].Value);
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

        private void dgvAttendance_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = dgvAttendance.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            txtSNOAttend.Text = dgvAttendance.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
            if (CheckvalidSNO(txtSNOAttend.Text))
            {
                txtNameAttend.Text = name;
                txtIntkAttend.Text = intake;
            } 
            txtRnkAttend.Text = dgvAttendance.Rows[e.RowIndex].Cells["dgvRank"].Value.ToString();
            txtTrpAttend.Text = dgvAttendance.Rows[e.RowIndex].Cells["dgvTrp"].Value.ToString();
            txtMilitryAttend.Text = dgvAttendance.Rows[e.RowIndex].Cells["dgvMilitry"].Value.ToString();
            txtAcdAttend.Text = dgvAttendance.Rows[e.RowIndex].Cells["dgvAcd"].Value.ToString();

            btnUpdateAtten.Enabled = true;
            btnAddAttend.Enabled = false;
        }

        private void btnUpdateAtten_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                if (ob.updateAtendance(ID, txtRnkAttend.Text, txtTrpAttend.Text, txtMilitryAttend.Text, txtAcdAttend.Text,imgHD))
                {
                    MessageBox.Show("Successfully Updated");
                    ClearAttend();
                    FillGrid();
                }
                else
                {
                    MessageBox.Show("Error occured");
                } 
                btnUpdateAtten.Enabled = false;
                btnAddAttend.Enabled = true;
            }
        }

        private void txtNameAttend_MouseClick(object sender, MouseEventArgs e)
        {
            if (CheckvalidSNO(txtSNOAttend.Text))
            {
                txtNameAttend.Text = name;
                txtIntkAttend.Text = intake;
            }
            else
                MessageBox.Show("Invalid SNO ");
        }

        private void button5_Click(object sender, EventArgs e)
        { 

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
            SearchForm1 ob = new SearchForm1();
            ob.ShowDialog();
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
            AttendSheetForm1 ob = new AttendSheetForm1();
            ob.ShowDialog();
        }

        private void BtnIncDiary_Click(object sender, EventArgs e)
        {
            SickChrtForm1 ob = new SickChrtForm1();
            ob.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewCadet ob = new AddNewCadet();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgvAttendance.ColumnCount - 3); //avoiding unnessary column
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 95;
            pdfTable.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Topic on top of pdf
            string header = @"       Attendance Sheet";
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
            for (int j = 3; j < dgvAttendance.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dgvAttendance.Columns[j].HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                for (int j = 3; j < dgvAttendance.Columns.Count; j++)
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

        private void btnSearch_Click(object sender, EventArgs e)
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
    }//End Class
}
