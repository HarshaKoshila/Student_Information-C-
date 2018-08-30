using KDUInfoApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsApp.Admin
{
    public partial class AddUserFormAdmin : Form
    {
        DBAccess ob = new DBAccess();
        byte[] imgProf;
        string imgProfLoc;
        String corectPdw = "";
        String userID;
        String userType;
        public AddUserFormAdmin()
        {
            InitializeComponent();
            FillGrid();
            btnUpdateUser.Enabled = false;
            btnAddUser.Enabled = true;
            pnlProf.Visible = false;
        }

        public void FillGrid()
        {
            DataSet ds = ob.getAllUser();
            dgvUser.DataSource = ds.Tables["UserOfKDU"].DefaultView;
        }

        public void Clear()
        {
            txtNameUser.Text = "";
            txtEmailUser.Text = "";
            txtPhoneUser.Text = "";
            txtPwdUser.Text = "";
            txtRePwdUser.Text = "";
            txtUName.Text = "";
            txtRank.Text = "";
            rbInstruct.Checked = false;
            rbOfficer.Checked = false;
            btnUpdateUser.Enabled = false;
            btnAddUser.Enabled = true;
        }

        public bool IsEmptyAll()
        {
            if ((txtEmailUser.Text).Equals("") || (txtNameUser.Text).Equals("") || (txtRank.Text).Equals("") || (txtPhoneUser.Text).Equals("") || (txtPwdUser.Text).Equals("") || ((rbInstruct.Checked).Equals(false) && (rbOfficer.Checked).Equals(false) && (rbAdmin.Checked).Equals(false)))
            {
                return true;
            }
            return false;
        }

        public bool IsValidEmail(string emailaddress) //Email Validator
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        { 
            if (IsEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            { 
                if (rbOfficer.Checked)
                {
                    userType = "Officer";

                }
                else if (rbInstruct.Checked)
                {
                    userType = "Instructor";

                }
                else if (rbAdmin.Checked)
                {
                    userType = "Admin";

                }
                if ((txtPwdUser.Text).Equals(txtRePwdUser.Text))
                {
                    corectPdw = txtPwdUser.Text;
                    
                    if (ob.addUser(txtNameUser.Text, userType, txtRank.Text,txtPhoneUser.Text, txtEmailUser.Text,txtUName.Text, corectPdw,imgProf))
                    {
                        MessageBox.Show("Successful added to db ");
                        FillGrid();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Set image Correctly ");
                    }
                }
                else
                    lblPwdMsg.Text = "*No match passord";

            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (IsEmptyAll())
                MessageBox.Show("please fill in all required fields");
            else
            {
                if (rbOfficer.Checked)
                {
                    userType = "Officer";

                }
                else if (rbInstruct.Checked)
                {
                    userType = "Instructor";

                }
                else if (rbAdmin.Checked)
                {
                    userType = "Admin";

                }
                if ((txtPwdUser.Text).Equals(txtRePwdUser.Text))
                {
                    corectPdw = txtPwdUser.Text;
                    if (ob.updateUser(userID, txtNameUser.Text, userType,txtRank.Text, txtPhoneUser.Text, txtEmailUser.Text,txtUName.Text, corectPdw, imgProf))
                    {
                        MessageBox.Show("Successfully Updated");
                        FillGrid();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Set image Correctly");
                    }
                }
                else
                    lblPwdMsg.Text = "*No match passord"; 
            }
        }

        private void txtRePwdUser_TextChanged(object sender, EventArgs e)
        {
            lblPwdMsg.Text = " ";
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlProf.Visible = false;
            try
            {
                userID = dgvUser.Rows[e.RowIndex].Cells["dgvID"].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(e);
            }

            if (e.ColumnIndex == dgvUser.Columns["ColumnView"].Index)
            {
                picBoxSearch.Image = ob.getUserProImg(userID); 
                pnlProf.Visible = true;
            }
            if (e.ColumnIndex == dgvUser.Columns["ColumnDelete"].Index)
            {
                if (MessageBox.Show("Delete selected Job Post?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                { 
                    if (ob.deleteUser(userID))
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
        }

        private void dgvUser_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            userID = dgvUser.Rows[e.RowIndex].Cells["dgvID"].Value.ToString();
            txtNameUser.Text = dgvUser.Rows[e.RowIndex].Cells["dgvName"].Value.ToString();
            userType = dgvUser.Rows[e.RowIndex].Cells["dgvUserType"].Value.ToString();
            txtPhoneUser.Text = dgvUser.Rows[e.RowIndex].Cells["dgvPhone"].Value.ToString();
            txtEmailUser.Text = dgvUser.Rows[e.RowIndex].Cells["dgvEmail"].Value.ToString();
            txtUName.Text = dgvUser.Rows[e.RowIndex].Cells["dgvUserName"].Value.ToString();
            if (userType.Equals("Officer"))
            {
                rbOfficer.Select();

            }
            else if (userType.Equals("Instructor"))
            {
                rbInstruct.Select();

            }
            else if (userType.Equals("Admin"))
            {
                rbAdmin.Select();

            }

            btnUpdateUser.Enabled = true;
            btnAddUser.Enabled = false;
        }

        private void btnInstr_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchFormAdmin ob = new SearchFormAdmin();
            ob.ShowDialog();
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            this.Hide();
            BackupFormAdmin ob = new BackupFormAdmin();
            ob.ShowDialog();
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminForm ob = new AdminForm();
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

        private void btnUsrAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnUploadBbook_Click(object sender, EventArgs e)
        {
            imgProf = null;
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Only. |*.jpg; *.jpeg; *.png; *.gif; ";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                imgProfLoc = openFD.FileName.ToString();
                picBoxImg.ImageLocation = imgProfLoc;
                lblhd.Text = "";
                try
                {
                    //-----------For imgProf
                    FileStream stream3 = new FileStream(imgProfLoc, FileMode.Open, FileAccess.Read);
                    BinaryReader brs3 = new BinaryReader(stream3);
                    imgProf = brs3.ReadBytes((int)stream3.Length);
                }
                catch (ArgumentNullException)
                { 
                    MessageBox.Show("Images aren't set correctly ");
                }
            }
            else
                lblhd.Text = "No images";
        }

        private void txtEmailUser_Leave(object sender, EventArgs e)
        {
            if (!txtEmailUser.Text.Equals(""))
            {
                if (!IsValidEmail(txtEmailUser.Text))
                {
                    lblEmail.Text = "Not a valid email";
                }
                else
                    lblEmail.Text = "";
            }
        }

        private void txtEmailUser_TextChanged(object sender, EventArgs e)
        {
            lblEmail.Text = "";
        }
    }
}
