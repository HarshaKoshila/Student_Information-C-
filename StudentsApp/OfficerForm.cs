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
    public partial class OfficerForm : Form
    {
        public OfficerForm()
        {
            InitializeComponent();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            
                this.btnHome.BackColor = Color.FromArgb(0, 102, 255);
             
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            this.btnHome.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        { 

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void btnGood_MouseEnter(object sender, EventArgs e)
        {
            this.btnGood.BackColor = Color.FromArgb(0, 102, 255);
        }

        private void pnlCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.FlatStyle = FlatStyle.Standard;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatStyle = FlatStyle.Flat;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            GdCondctForm1 ob = new GdCondctForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkAssForm1 ob = new WorkAssForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        { 
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            this.Hide();
            GdCondctForm1 ob = new GdCondctForm1();
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

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChrgSheetForm1 ob = new ChrgSheetForm1();
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

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            BlackBkForm1 ob = new BlackBkForm1();
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

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            InciDryForm1 ob = new InciDryForm1();
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

        private void button16_Click(object sender, EventArgs e)
        {
            this.Hide();
            BackupForm ob = new BackupForm();
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

        private void button15_Click(object sender, EventArgs e)
        { 
        }

        private void btnInstr_Click(object sender, EventArgs e)
        { 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Officer_InstructForm ob = new Officer_InstructForm();
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
