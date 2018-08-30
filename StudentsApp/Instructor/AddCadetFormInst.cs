using KDUInfoApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StudentsApp.Instructor
{
    public partial class AddCadetFormInst : Form
    {
        DBAccess ob = new DBAccess();
        byte[] imgProf;
        byte[] imgOL;
        byte[] imgAL;
        string imgProfLoc;
        string imgOLLoc;
        string imgALLoc;

        public AddCadetFormInst()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImgOl_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Only. |*.jpg; *.jpeg; *.png; *.gif; ";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                imgOLLoc = openFD.FileName.ToString();
                picBoxOL.ImageLocation = imgOLLoc;
                lblOL.Text = "";
            }
            else
                lblOL.Text = "No images";
        }

        private void btnImgAL_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Only. |*.jpg; *.jpeg; *.png; *.gif; ";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                imgALLoc = openFD.FileName.ToString();
                picBoxAL.ImageLocation = imgALLoc;
                lblAL.Text = "";
            }
            else
                lblAL.Text = "No images";
        }

        private void btnImgProf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Only. |*.jpg; *.jpeg; *.png; *.gif; ";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                imgProfLoc = openFD.FileName.ToString();
                picBoxProf.ImageLocation = imgProfLoc;
                lblProf.Text = "";
            }
            else
                lblProf.Text = "No images";
        }

        private void btnAddCadet_Click(object sender, EventArgs e)
        {
            String date1 = dateTimePicker1.Value.ToShortDateString();
            String birth = dateTimePicker2.Value.Date.ToShortDateString();
            String gender = "";
            imgProf = null;
            imgOL = null;
            imgAL = null;
            if (rbMaleCadet.Checked)
            {
                gender = "Male";

            }
            else if (rbFemaleCadet.Checked)
            {
                gender = "Female";

            }

            //check whether all text fields is not empty
            if (txtSNO.Text.Equals("") || txtName.Text.Equals("") || txtNIC.Text.Equals("") || txtIntake.Text.Equals("") || txtRank.Text.Equals("") || txtRace.Text.Equals("") || gender.Equals("") || txtAddrss.Text.Equals("") || txtNxtKin.Text.Equals("") || txtRelat.Text.Equals("") || txtOccu.Text.Equals("") || txtFamAddres.Text.Equals("") || txtHeigh.Text.Equals("") || txtWeight.Text.Equals("") || txtChest.Text.Equals("") || txtNeck.Text.Equals("") || txtHat.Text.Equals("") || txtBoot.Text.Equals("") || txtBlood.Text.Equals("") || txtWaist.Text.Equals("") || txtBank.Text.Equals("") || txtPolis.Text.Equals("") || txtEmp.Text.Equals("") || txtSchool.Text.Equals("") || txtYrOfSchool.Text.Equals("") || txtSport.Text.Equals("") || txtOLyr.Text.Equals("") || txtOLsheet.Text.Equals("") || txtALyr.Text.Equals("") || txtALsheet.Text.Equals(""))
            {
                MessageBox.Show("Please fill all input field before submit");
            }
            else
            {
                //After validating then check whether all image is exist
                if ((lblAL.Text.Equals("")) && (lblOL.Text.Equals("")) && (lblProf.Text.Equals(""))) //Check whether all images are set
                {
                    try
                    {   //---------Convert Image to byte format
                        //---------For imgOL
                        FileStream stream1 = new FileStream(imgOLLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader brs1 = new BinaryReader(stream1);
                        imgOL = brs1.ReadBytes((int)stream1.Length);
                        //------------For imgAL
                        FileStream stream2 = new FileStream(imgALLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader brs2 = new BinaryReader(stream2);
                        imgAL = brs2.ReadBytes((int)stream2.Length);
                        //-----------For imgProf
                        FileStream stream3 = new FileStream(imgProfLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader brs3 = new BinaryReader(stream3);
                        imgProf = brs3.ReadBytes((int)stream3.Length);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("No images " + e);
                        MessageBox.Show("Images aren't set correctly ");
                    }


                    if (ob.addCadet(txtSNO.Text, txtName.Text, txtNIC.Text, date1, gender, birth, txtIntake.Text, txtRank.Text, txtRace.Text, txtAddrss.Text, txtNxtKin.Text, txtRelat.Text, txtOccu.Text, txtFamAddres.Text, txtHeigh.Text, txtWeight.Text, txtChest.Text, txtNeck.Text, txtHat.Text, txtBoot.Text, txtBlood.Text, txtWaist.Text, txtBank.Text, txtPolis.Text, txtEmp.Text, txtSchool.Text, txtYrOfSchool.Text, txtSport.Text, txtOLyr.Text, txtOLsheet.Text, txtALyr.Text, txtALsheet.Text, imgOL, imgAL, imgProf))
                    {
                        MessageBox.Show("Successful added to db ");
                    }
                    else
                    {
                        MessageBox.Show("Fail added");
                    }

                }
                else
                    MessageBox.Show("Set all images");
            }
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            String str = txtNIC.Text; //NIC validate - 9 digit and last letter X or V
            if ((str.Count(char.IsDigit) == 9) && (str.EndsWith("X", StringComparison.OrdinalIgnoreCase) || str.EndsWith("V", StringComparison.OrdinalIgnoreCase)))
            {
                lblNICmsg.Text = "";
            }
            else
            {
                lblNICmsg.Text = "Invalid NIC";
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            InstructorForm ob = new InstructorForm();
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

        private void btnBlackbook_Click(object sender, EventArgs e)
        {
            this.Hide();
            AttendFormInstruct ob = new AttendFormInstruct();
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

        private void button4_Click(object sender, EventArgs e)
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchFormIntruct ob = new SearchFormIntruct();
            ob.ShowDialog();
            this.Close();
        }

        private void txtSNO_Leave(object sender, EventArgs e)
        {
            if (!txtSNO.Text.Equals(""))
            {
                if (!(txtSNO.Text).All(char.IsDigit))
                {
                    lblSNO.Text = "*Only numbers";
                }
            }
        }

        private void txtSNO_TextChanged(object sender, EventArgs e)
        {
            lblSNO.Text = "";
        }
    }
}
