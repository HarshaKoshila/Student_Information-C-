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
    public partial class StartUpForm : Form
    {
        public StartUpForm()
        {
            InitializeComponent();
        }

        private void StartUpForm_Load(object sender, EventArgs e)
        { 
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 700;
            progressBar.Maximum = 10; 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value != 10)
            {
                progressBar.Value++;
            }
            else
            {  
                timer1.Stop();
                this.Hide();
                LoginForm ob = new LoginForm();
                ob.ShowDialog();
                this.Close();
            }
        }
    }
}
