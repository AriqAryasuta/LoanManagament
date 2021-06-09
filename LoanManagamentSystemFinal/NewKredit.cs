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
using FireSharp.Interfaces;
using FireSharp.Response;

namespace LoanManagamentSystemFinal
{
    public partial class NewKredit : Form
    {
        public NewKredit()
        {
            InitializeComponent();
        }

        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "Yg9HCpve56nS6OdKSQ0Afag6hPwYtVAJh8KghnKo",
            BasePath = "https://loanmanagement-8faf0-default-rtdb.asia-southeast1.firebasedatabase.app/"

        };

        IFirebaseClient client;

        private void NewKredit_Load(object sender, EventArgs e)
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
            ClientInfo cl = new ClientInfo()
            {
                nama = textBox1.Text,
                alamat = textBox2.Text,
                id = textBox4.Text,
                pinjaman = label12.Text
            };
            var setter = client.Set("ClientList/" + textBox4.Text,cl);

            FirebaseResponse res = client.Get(@"Counter");
            int Counter = int.Parse(res.ResultAs<string>());

            idInfo idlist = new idInfo()
            {
                id = textBox4.Text
            };

            var set2 = client.Set(@"Counter", ++Counter);
            var set3 = client.Set(@"data/" + Counter, idlist);
            MessageBox.Show("Data ditambahkan");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double maxpinjaman;
            double penghasilan;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                penghasilan = double.Parse(textBox3.Text);
                button4.Enabled = true;
                textBox5.Enabled = true;
                if (radioButton1.Checked)
                {
                    maxpinjaman = penghasilan*0.6 * 6;
                    label6.Text = maxpinjaman.ToString();
                }
                if (radioButton2.Checked)
                {
                    maxpinjaman = penghasilan * 0.6 * 12;
                    label6.Text = maxpinjaman.ToString();
                }
                if (radioButton3.Checked)
                {
                    maxpinjaman = penghasilan * 0.6 * 18;
                    label6.Text = maxpinjaman.ToString();
                }
            }
            else
            {
                MessageBox.Show("Mohon isi semua box");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double maxpinjaman = double.Parse(label6.Text);
            double pinjaman = double.Parse(textBox5.Text);
            double pinjamanBunga;
            if(maxpinjaman < pinjaman)
            {
                MessageBox.Show("Jumlah pinjaman melebih batas maksimum");
            }
            else
            {
                if (radioButton1.Checked)
                {
                    pinjamanBunga = pinjaman * 1.02;
                    label12.Text = pinjamanBunga.ToString();
                    
                }
                if (radioButton2.Checked)
                {
                    pinjamanBunga = pinjaman * 1.04;
                    label12.Text = pinjamanBunga.ToString();
                    
                }
                if (radioButton3.Checked)
                {
                    pinjamanBunga = pinjaman * 1.06;
                    label12.Text = pinjamanBunga.ToString();
                    
                }
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new LoanManagement().Show();
            this.Hide();
            
        }
    }
}
