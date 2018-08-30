using KDUInfoApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsApp
{
    public partial class SearchForm1 : Form
    {
        public String SNO;
        DBAccess ob = new DBAccess();
        public SearchForm1()
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

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblRank_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblNIC_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblServNo_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void picBoxSearch_Click(object sender, EventArgs e)
        {

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

        private void BtnIncDiary_Click(object sender, EventArgs e)
        {
            this.Hide();
            SickChrtForm1 ob = new SickChrtForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewCadet ob = new AddNewCadet();
            ob.ShowDialog();
            this.Close();
        }

        private void btnGood_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }
    }
}
