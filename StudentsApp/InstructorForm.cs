using StudentsApp.Instructor;
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
    public partial class InstructorForm : Form
    {
        public InstructorForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        { 
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchFormIntruct ob = new SearchFormIntruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        { 
        }

        private void btnBlackbook_Click(object sender, EventArgs e)
        {
            this.Hide();
            AttendFormInstruct ob = new AttendFormInstruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            PTTestFormInstruc ob = new PTTestFormInstruc();
            ob.ShowDialog();
            this.Close();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            this.Hide();
            PTTestFormInstruc ob = new PTTestFormInstruc();
            ob.ShowDialog();
            this.Close();
        }

        private void BtnIncDiary_Click(object sender, EventArgs e)
        {
            this.Hide();
            SickChrtFormInstruct ob = new SickChrtFormInstruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            SickChrtFormInstruct ob = new SickChrtFormInstruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SearchFormIntruct ob = new SearchFormIntruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            PTTestFormInstruc ob = new PTTestFormInstruc();
            ob.ShowDialog();
            this.Close();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AttendFormInstruct ob = new AttendFormInstruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SickChrtFormInstruct ob = new SickChrtFormInstruct();
            ob.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCadetFormInst ob = new AddCadetFormInst();
            ob.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCadetFormInst ob = new AddCadetFormInst();
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
    }
}
