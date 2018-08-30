using StudentsApp.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsApp
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGood_Click(object sender, EventArgs e)
        { 
        }

        private void button10_Click(object sender, EventArgs e)
        { 
        }

        private void btnWork_Click(object sender, EventArgs e)
        { 
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
             

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnAddGCndct_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUserFormAdmin ob = new AddUserFormAdmin();
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

        private void button15_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchFormAdmin ob = new SearchFormAdmin();
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
            this.Hide();
            SearchFormAdmin ob = new SearchFormAdmin();
            ob.ShowDialog();
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.Hide();
            BackupFormAdmin ob = new BackupFormAdmin();
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
    }
}
