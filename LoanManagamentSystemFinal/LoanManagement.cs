using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace LoanManagamentSystemFinal
{
    public partial class LoanManagement : Form
    {
        public LoanManagement()
        {
            InitializeComponent();
        }
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "Yg9HCpve56nS6OdKSQ0Afag6hPwYtVAJh8KghnKo",
            BasePath = "https://loanmanagement-8faf0-default-rtdb.asia-southeast1.firebasedatabase.app/"
            
        };

        IFirebaseClient client;

        private void LoanManagement_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                MessageBox.Show("Terdapat permasalahan jaringan");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new NewKredit().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            FirebaseResponse res = client.Get(@"Counter");
            int Counter = int.Parse(res.ResultAs<string>());
            for(int i = 1; i<= Counter; i++)
            {
                FirebaseResponse res2 = client.Get(@"data/" + i + "/id");
                string id = res2.ResultAs<string>();

                FirebaseResponse res3 = client.Get(@"ClientList/" + id);
                ClientInfo cl = res3.ResultAs<ClientInfo>();

                if(cl.alamat != "")
                {
                    dataGridView1.Rows.Add(cl.alamat, cl.id, cl.nama, cl.pinjaman);
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
            
        }
    }
}
