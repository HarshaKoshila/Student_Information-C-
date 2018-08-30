using KDUInfoApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsApp.Admin
{
    public partial class SearchFormAdmin : Form
    {
        public String SNO;
        DBAccess ob = new DBAccess();
        public SearchFormAdmin()
        {
            InitializeComponent();
            FillGrid();
            pnlProf.Visible = false;
        }

        public void FillGrid()
        {
            DataSet ds = ob.getAllCadetsForGridView();
            dgvCadets.DataSource = ds.Tables["cadetsOfKDU"].DefaultView;
        }

        public void FillGridFromSNO(String SNO)
        {
            DataSet ds = ob.getCadetsFromSNOForGridView(SNO);
            dgvCadets.DataSource = ds.Tables["SNO_cadetsOfKDU"].DefaultView;
        }

        public void FillGridFromName(String name)
        {
            DataSet ds = ob.getCadetsFromNameForGridView(name);
            dgvCadets.DataSource = ds.Tables["Name_cadetsOfKDU"].DefaultView;
        }

        public void FillGridFromIntake(String intake)
        {
            DataSet ds = ob.getCadetsFromIntakeForGridView(intake);
            dgvCadets.DataSource = ds.Tables["Intake_cadetsOfKDU"].DefaultView;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSearch.SelectedItem.Equals("SNO"))
                {
                    FillGridFromSNO(Regex.Replace(txtSearch.Text, @"\s", ""));
                }

                if (cmbSearch.SelectedItem.Equals("Name"))
                {
                    FillGridFromName(Regex.Replace(txtSearch.Text, @"\s", ""));
                }

                if (cmbSearch.SelectedItem.Equals("Intake"))
                {
                    FillGridFromIntake(Regex.Replace(txtSearch.Text, @"\s", ""));
                }
            }
            catch (NullReferenceException) { }
        }

        private void dgvCadets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlProf.Visible = false;
            try
            {
                SNO = dgvCadets.Rows[e.RowIndex].Cells["SNOColumn"].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(e);
            }

            if (e.ColumnIndex == dgvCadets.Columns["ColumnProf"].Index)
            {
                picBoxSearch.Image = ob.getProImg(SNO);
                lblID.Text = SNO;
                pnlProf.Visible = true;
            }
            if (e.ColumnIndex == dgvCadets.Columns["ColumnOL"].Index)
            {
                FormImgViewer form = new FormImgViewer(ob.getOLImg(SNO));
                form.ShowDialog();
            }
            if (e.ColumnIndex == dgvCadets.Columns["ColumnAL"].Index)
            {
                FormImgViewer form = new FormImgViewer(ob.getALImg(SNO));
                form.ShowDialog();
            }

            if (e.ColumnIndex == dgvCadets.Columns["ColumnDelete"].Index)//For Delete button
            {
                if (MessageBox.Show("Delete selected Job Post?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    SNO = dgvCadets.Rows[e.RowIndex].Cells["SNOColumn"].Value.ToString();
                    if (ob.deleteCadet(SNO))
                    {
                        MessageBox.Show("Successfully Deleted");
                        FillGrid();
                    }
                    else
                    {
                        MessageBox.Show("Error occured while deleting");
                    }
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminForm ob = new AdminForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnUsrAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUserFormAdmin ob = new AddUserFormAdmin();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnInstr_Click(object sender, EventArgs e)
        { 
        }
    }
}
