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
    public partial class InciDryForm1 : Form
    {
        DBAccess ob = new DBAccess();
        byte[] imgHD;
        string imgHDLoc;
        String SNO;
        String ID;
        String name = " ";
        String intake = " ";
        int x = 0;//for pdf file name that user save
        public InciDryForm1()
        {
            InitializeComponent();
            FillGrid();
            btnUpdate.Enabled = false;
            btnAddDiary.Enabled = true;
        }

        public void FillGrid()
        {
            DataSet ds = ob.getInciDry();
            dgvInciDry.DataSource = ds.Tables["IncidentDiaryOfKDU"].DefaultView;
        }

        public void FillGridFromSNO(String SNO)
        {
            DataSet ds = ob.getInciDrySNO(SNO);
            dgvInciDry.DataSource = ds.Tables["SNOIncidentDiaryOfKDU"].DefaultView;
        }
        public void FillGridFromIntake(String intake)
        {
            DataSet ds = ob.getInciDryFromIntake(intake);
            dgvInciDry.DataSource = ds.Tables["Intake_IncidentDiaryOfKDU"].DefaultView;
        }

        public void Clear()
        { 
            txtCritInDiary.Text = "";
            txtIntkeDiary.Text = "";
            txtNmeDiary.Text = "";
            txtRemrkDiary.Text = "";
            txtSerNoDiary.Text = "";
            txtSNODiary.Text = "";
            txtTrpDiary.Text = "";
            txtYrDiary.Text = "";
        }

        public bool isEmptyAll()
        {
            if ((txtCritInDiary.Text).Equals("") || (txtIntkeDiary.Text).Equals("") || (txtNmeDiary.Text).Equals("") || (txtRemrkDiary.Text).Equals("") || (txtSerNoDiary.Text).Equals("") || (txtSNODiary.Text).Equals("") || (txtTrpDiary.Text).Equals("") || (txtYrDiary.Text).Equals(""))
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

        private void btnAddDiary_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                String date1 = dtpDateDiray.Value.Date.ToShortDateString();
                if (ob.addIncidentDry(txtSNODiary.Text, txtTrpDiary.Text, txtYrDiary.Text, txtSerNoDiary.Text, date1, txtCritInDiary.Text, txtRemrkDiary.Text, imgHD))
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

        private void txtSNODiary_Leave(object sender, EventArgs e)
        {
           
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

        private void button1_Click(object sender, EventArgs e)
        {
            FillGridFromSNO(txtSearch.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Enabled = false;
            btnAddDiary.Enabled = true;
        }

        private void dgvInciDry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = dgvInciDry.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(e);
            }
            if (e.ColumnIndex == dgvInciDry.Columns["ColumnView"].Index)
            {
                FormImgViewer form = new FormImgViewer(ob.getInciDryImg(ID));
                form.ShowDialog();
            }

            if (e.ColumnIndex == dgvInciDry.Columns["ColumnDelete"].Index)
            {
                if (MessageBox.Show("Delete selected Job Post?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                { 
                    if (ob.deleteInciDry(ID))
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

            if (e.ColumnIndex == dgvInciDry.Columns["ColumnSave"].Index)
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
                    SNO = dgvInciDry.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
                    CheckvalidSNO(SNO);

                    Document document = new Document();
                    try
                    {
                        PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + SNO + "_" + (++x) + ".pdf", FileMode.Create));
                        document.Open();

                        string header = @"Incident Diary";
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
                        for (int j = 4; j < dgvInciDry.Columns.Count; j++)
                        {
                            Paragraph p = new Paragraph(dgvInciDry.Columns[j].HeaderText + "     :" + dgvInciDry.Rows[e.RowIndex].Cells[j].Value);
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

        private void dgvInciDry_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = dgvInciDry.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            txtSNODiary.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
            if (CheckvalidSNO(txtSNODiary.Text))
            {
                txtNmeDiary.Text = name;
                txtIntkeDiary.Text = intake;
            }
            txtTrpDiary.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvTrp"].Value.ToString();
            txtYrDiary.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvYear"].Value.ToString(); 
            txtSerNoDiary.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvSe"].Value.ToString();
            dtpDateDiray.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvDate"].Value.ToString();
            txtCritInDiary.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvInci"].Value.ToString();
            txtRemrkDiary.Text = dgvInciDry.Rows[e.RowIndex].Cells["dgvMrks"].Value.ToString(); 

            btnUpdate.Enabled = true;
            btnAddDiary.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                if (ob.updateInciDry(ID, txtTrpDiary.Text, txtYrDiary.Text, txtSerNoDiary.Text, dtpDateDiray.Value.ToShortDateString(), txtCritInDiary.Text, txtRemrkDiary.Text,imgHD))
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
                btnAddDiary.Enabled = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            this.Hide();
            GdCondctForm1 ob = new GdCondctForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            OfficerForm ob = new OfficerForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkAssForm1 ob = new WorkAssForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnChrgrSheet_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChrgSheetForm1 ob = new ChrgSheetForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnBlackbook_Click(object sender, EventArgs e)
        {
            this.Hide();
            BlackBkForm1 ob = new BlackBkForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            this.Hide();
            BackupForm ob = new BackupForm();
            ob.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }

        private void txtNmeDiary_MouseClick(object sender, MouseEventArgs e)
        {
            if (CheckvalidSNO(txtSNODiary.Text))
            {
                txtNmeDiary.Text = name;
                txtIntkeDiary.Text = intake;
            }
            else
                MessageBox.Show("Invalid SNO ");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgvInciDry.ColumnCount - 3); //avoiding unnessary column
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 95;
            pdfTable.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Topic on top of pdf
            string header = @"       Incident Diary";
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
            for (int j = 3; j < dgvInciDry.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dgvInciDry.Columns[j].HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dgvInciDry.Rows)
            {
                for (int j = 3; j < dgvInciDry.Columns.Count; j++)
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
    }
}
