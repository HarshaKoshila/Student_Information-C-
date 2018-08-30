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
    public partial class FormImgViewer : Form
    {
         
        public FormImgViewer(Image img)
        {
            InitializeComponent(); 
            picBoxOL.Image = img;
        }

        private void FormOLimg_Load(object sender, EventArgs e)
        {

        }
    }
}
