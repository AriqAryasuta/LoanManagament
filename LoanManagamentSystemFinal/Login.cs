using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagamentSystemFinal
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "Admin" && txtPassword.Text == "1234")
            {
                new LoanManagement().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Password atau Username salah");
            }
        }
    }
}
