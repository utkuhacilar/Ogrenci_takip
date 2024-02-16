using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Öğrenci_Takip
{
    public partial class kayit : Form
    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=veritabani.accdb");
        OleDbCommand kmt = new OleDbCommand();
        public kayit()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                label6.Text = sonuc;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void kayit_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = Form1.portismi;
            serialPort1.BaudRate = Convert.ToInt16(Form1.banthizi);

            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();
                    label7.Text = "Bağlantı Sağlandı";
                    label7.ForeColor = Color.Green;
                }
                catch 
                {
                    label7.Text = "Bağlantı Sağlanamadı";
                    
                }
               

            }
            else
            {
                
                label7.Text = "Bağlantı Sağlanamadı";
                label7.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "123123" || textBox1.Text=="" || comboBox1.Text=="seçiniz" || comboBox2.Text=="seçiniz"||textBox2.Text=="")
            {
                label8.Text = "Bilgilerinizi Eksiksiz Giriniz";
                label8.ForeColor = Color.Red;
            }
            else
            {
                try
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO kayit_tablo(kid,isim,sinif,sube,resimbilgisi) VALUES ('"+label6.Text+ "','"+textBox1.Text+"','"+textBox1.Text+"','"+comboBox1.Text+"','"+comboBox2.Text+"','"+textBox2.Text+"')";
                    kmt.ExecuteNonQuery();
                    label8.Text = "Kayıt Başarılı";
                    label8.ForeColor = Color.Green;



                    bag.Close();
                }
                catch 
                {
                    bag.Close();
                    MessageBox.Show("Bu kart zaten kayıtlı");
                   
                }
                
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label6.Text = "__________";
            textBox1.Text = "";
            comboBox1.Text = "seçiniz";
            comboBox2.Text = "seçiniz";
            textBox2.Text = "";
            label8.Text = "";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyaları (jpg) |*.jpg|Tüm Dosyalar |*.*";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\foto";
            dosya.RestoreDirectory = true;

            if (dosya.ShowDialog()==DialogResult.OK)
            {
                string di = dosya.SafeFileName;
                textBox2.Text = di;

            }
        }
    }
}
