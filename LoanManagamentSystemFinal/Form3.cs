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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "Yg9HCpve56nS6OdKSQ0Afag6hPwYtVAJh8KghnKo",
            BasePath = "https://loanmanagement-8faf0-default-rtdb.asia-southeast1.firebasedatabase.app/"

        };

        IFirebaseClient client;
        private void button1_Click(object sender, EventArgs e)
        {
            var result = client.Get(@"ClientList/" + textBox1.Text);
            ClientInfo cl = result.ResultAs<ClientInfo>();
            if(cl.id == "")
            {
                MessageBox.Show("data tidak ditemukan");
            }
            if(cl.pinjaman == "lunas")
            {
                MessageBox.Show("Kredit telah Lunas");
                label8.Text = cl.nama;
                label9.Text = cl.alamat;
                label10.Text = cl.pinjaman;
            }
            else 
            {
                label8.Text = cl.nama;
                label9.Text = cl.alamat;
                label10.Text = cl.pinjaman;
                button2.Enabled = true;
                textBox2.Enabled = true;
            }
            
        }

        private void Form3_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            double bayard = double.Parse(textBox2.Text);
            double pinjamand = double.Parse(label10.Text);
            if (bayard == 0)
            {
                MessageBox.Show("Mohon isi jumlah yang dibayarkan");
            }
            if(bayard > pinjamand)
            {
                MessageBox.Show("Jumlah pembayaran melebihi kredit");
            }
            else
            {
                double sisad = (pinjamand - bayard);
                string sisa;
                if(sisad == 0)
                {
                    sisa = "lunas";    
                }
                else
                {
                    sisa = sisad.ToString();
                }
                ClientInfo cl = new ClientInfo()
                {
                    id = textBox1.Text,
                    nama = label8.Text,
                    alamat = label9.Text,
                    pinjaman =  sisa
                };
                var setter = client.Update("ClientList/" + textBox1.Text, cl);
                MessageBox.Show("Pembayaran dilakukan");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new LoanManagement().Show();
            this.Hide();
        }
    }
}
