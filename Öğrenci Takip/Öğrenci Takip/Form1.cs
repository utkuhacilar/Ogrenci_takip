﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace Öğrenci_Takip
{
    public partial class Form1 : Form
    {
        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            timer1.Start();
            portismi = comboBox1.Text;
            banthizi = comboBox2.Text;
            try
            {
                serialPort1.PortName = portismi;
                serialPort1.BaudRate = Convert.ToInt16(banthizi);

                serialPort1.Open();
                label1.Text = "Bağlantı sağlandı";
                label1.ForeColor = Color.Green;
            }
            catch
            {
                serialPort1.Close();
                serialPort1.Open();
                MessageBox.Show("Bağlantı zaten açık");
                
            }
           

            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label1.Text = "Bağlantı kesildi";
                label1.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                label2.Text = sonuc;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (portismi == null || banthizi == null)
            {
                MessageBox.Show("Bağlantını kontrol et");
            }
            else
            {
                timer1.Stop();
                serialPort1.Close();
                label1.Text = "bağalntı kapandı!";
                label1.ForeColor = Color.Red;

                kayit kyt = new kayit();
                kyt.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("115200");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;

        }
    }
}
