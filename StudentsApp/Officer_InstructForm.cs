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
    public partial class Officer_InstructForm : Form
    {
        public Officer_InstructForm()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            OfficerForm ob = new OfficerForm();
            ob.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchForm1 ob = new SearchForm1();
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            PTTestForm1 ob = new PTTestForm1();
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

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            AttendSheetForm1 ob = new AttendSheetForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            SickChrtForm1 ob = new SickChrtForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewCadet ob = new AddNewCadet();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
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
    }
}
