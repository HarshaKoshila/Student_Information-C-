using KDUInfoApp;
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
    public partial class LoginForm : Form
    {
        DBAccess ob = new DBAccess();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            List<User> list = ob.getUser((txtUsrName.Text).Replace(" ", String.Empty));
            bool isEmpty = !list.Any();
            if (isEmpty)
            {
                lblError.Text = "*Wrong user name";
            }
            else
            {
                foreach (var value in list)
                {
                    if (value.password.Replace(" ", String.Empty).Equals((txtPwd.Text).Replace(" ", String.Empty)))
                    {
                        if (value.userType.Replace(" ", String.Empty).Equals("Admin"))
                        {
                            this.Hide();
                            AdminForm ob = new AdminForm();
                            ob.ShowDialog();
                            this.Close();
                        }
                        if (value.userType.Replace(" ", String.Empty).Equals("Officer"))
                        {
                            this.Hide();
                            OfficerForm ob = new OfficerForm();
                            ob.ShowDialog();
                            this.Close();
                        }
                        if (value.userType.Replace(" ", String.Empty).Equals("Instructor"))
                        {
                            this.Hide();
                            InstructorForm ob = new InstructorForm();
                            ob.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                        lblError.Text = "*Wrong password";
                }
            }
           
            
        }

        
    }
}
