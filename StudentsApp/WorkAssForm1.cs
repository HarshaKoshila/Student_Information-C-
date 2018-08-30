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
    public partial class WorkAssForm1 : Form
    {
        DBAccess ob = new DBAccess();
        byte[] imgHD;
        string imgHDLoc;
        String SNO;
        String ID; 
        String name = " ";
        String intake = " ";
        int x = 0;//for pdf file name that user save

        public WorkAssForm1()
        {
            InitializeComponent();
            lblTotal1.Visible = false;
            lblTotal2.Visible = false;
            FillGrid();
            btnUpdate.Enabled = false;
            btnAddWork.Enabled = true;
        }

        public void FillGrid()
        {
            DataSet ds = ob.getWorkAss();
            dgvWorkAss.DataSource = ds.Tables["WorkAssOfKDU"].DefaultView;
        }

        public void FillGridFromSNO(String SNO)
        {
            DataSet ds = ob.getWorkAssSNO(SNO);
            dgvWorkAss.DataSource = ds.Tables["SNOWorkAssOfKDU"].DefaultView;
        }

        public void FillGridFromIntake(String intake)
        {
            DataSet ds = ob.getWorkAssFromIntake(intake);
            dgvWorkAss.DataSource = ds.Tables["Intake_WorkAssOfKDU"].DefaultView;
        }

        public void Clear()
        {
            txtDrill.Text = "";
            txtFile.Text = "";
            txtGConduct.Text = "";
            txtIntkWork.Text = "";
            txtNameWork.Text = "";
            txtPT.Text = "";
            txtSNOWork.Text = "";
            txtTrpWork.Text = "";
            txtWTrn.Text = "";
            lblTotal1.Visible = false;
            lblTotal2.Visible = false;

        }

        public bool isEmptyAll()
        {
            if ( (txtDrill.Text).Equals("") || (txtFile.Text).Equals("") || (txtGConduct.Text).Equals("")   || (txtPT.Text).Equals("") || (txtTrpWork.Text).Equals("")|| (txtWTrn.Text).Equals("") || (txtSNOWork.Text).Equals("") || (txtNameWork.Text).Equals("") || (txtIntkWork.Text).Equals(""))
                return true;
            else
                return false;
        }


        private void btnCal_Click(object sender, EventArgs e)
        {
            float total;
            try
            {
                total = (float)(Convert.ToDouble(txtDrill.Text) + Convert.ToDouble(txtPT.Text) + Convert.ToDouble(txtWTrn.Text) + Convert.ToDouble(txtGConduct.Text) + Convert.ToDouble(txtFile.Text));

                lblTotal2.Text = total.ToString();
                lblTotal1.Visible = true;
                lblTotal2.Visible = true;
            }
            catch (FormatException ) { }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                String date1 = dtpWork.Value.ToShortDateString();
                if (ob.addWorkAss(txtSNOWork.Text, txtTrpWork.Text, date1, txtDrill.Text, txtPT.Text, txtWTrn.Text, txtGConduct.Text, txtFile.Text, lblTotal2.Text, imgHD))
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
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
                    name = value.name ;  //Used Relace method for Removing white spaces 
                    intake = value.intake;
                    status = true;
                }
            }

            return status;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnAddWork.Enabled = true;
            Clear();
        }


        

        private void txtSNO_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSNO_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void txtSNO_Leave(object sender, EventArgs e)
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvWorkAss_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvWorkAss_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = dgvWorkAss.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(e);
            }
            if (e.ColumnIndex == dgvWorkAss.Columns["ColumnView"].Index)
            {
                FormImgViewer form = new FormImgViewer(ob.getWorkAssImg(ID));
                form.ShowDialog();
            }

            ///////////////////

            if (e.ColumnIndex == dgvWorkAss.Columns["ColumnDelete"].Index)
            {
                if (MessageBox.Show("Delete selected Job Post?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                { 
                    if (ob.deleteWorkAss(ID))
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
            

            if (e.ColumnIndex == dgvWorkAss.Columns["ColumnSave"].Index)
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
                    SNO = dgvWorkAss.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
                    CheckvalidSNO(SNO);

                    Document document = new Document();
                    try
                    {
                        PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + SNO + "_" + (++x) + ".pdf", FileMode.Create));
                        document.Open();

                        string header = @"Work Assessment";
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
                        for (int j = 4; j < dgvWorkAss.Columns.Count; j++)
                        {
                            Paragraph p = new Paragraph(dgvWorkAss.Columns[j].HeaderText + "     :" + dgvWorkAss.Rows[e.RowIndex].Cells[j].Value);
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

        private void dgvWorkAss_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = dgvWorkAss.Rows[e.RowIndex].Cells["ColumnID"].Value.ToString();
            txtSNOWork.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvSNO"].Value.ToString();
            if (CheckvalidSNO(txtSNOWork.Text))
            {
                txtNameWork.Text = name;
                txtIntkWork.Text = intake;
            } 
            txtTrpWork.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvTrp"].Value.ToString();
            dtpWork.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvDate"].Value.ToString();
            txtDrill.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvDrill"].Value.ToString();
            txtPT.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvPT"].Value.ToString();
            txtWTrn.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvWTrn"].Value.ToString();
            txtGConduct.Text = dgvWorkAss.Rows[e.RowIndex].Cells["gdvCndct"].Value.ToString();
            txtFile.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvFile"].Value.ToString();
            lblTotal2.Text = dgvWorkAss.Rows[e.RowIndex].Cells["dgvTotal"].Value.ToString();

            btnUpdate.Enabled = true;
            btnAddWork.Enabled = false;
            lblTotal1.Visible = true;
            lblTotal2.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                if (ob.updateWorkAss(ID, txtTrpWork.Text, dtpWork.Value.ToShortDateString(), txtDrill.Text, txtPT.Text, txtWTrn.Text, txtGConduct.Text, txtFile.Text, lblTotal2.Text,imgHD))
                {
                    MessageBox.Show("Successfully Updated");

                }
                else
                {
                    MessageBox.Show("Error occured");
                }
                Clear();
                FillGrid();
                btnUpdate.Enabled = false;
                btnAddWork.Enabled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillGridFromSNO(txtSearch.Text);
        }

        private void txtNameWork_MouseClick(object sender, MouseEventArgs e)
        {
            if (CheckvalidSNO(txtSNOWork.Text))
            {
                txtNameWork.Text = name;
                txtIntkWork.Text = intake;
            }
            else
                MessageBox.Show("Invalid SNO ");
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

        private void BtnIncDiary_Click(object sender, EventArgs e)
        {
            this.Hide();
            InciDryForm1 ob = new InciDryForm1();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgvWorkAss.ColumnCount-3); //avoiding unnessary column
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 95;
            pdfTable.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Topic on top of pdf
            string header = @"       Work Assessment";
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
            for (int j = 3; j < dgvWorkAss.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dgvWorkAss.Columns[j].HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dgvWorkAss.Rows)
            {
                for (int j = 3; j < dgvWorkAss.Columns.Count; j++)
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
                    PdfWriter.GetInstance(pdfDoc, new FileStream(folderPath + "/" +"PDF_" +date+ ".pdf", FileMode.Create));
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

        private void label15_Click(object sender, EventArgs e)
        {

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
    }
}
