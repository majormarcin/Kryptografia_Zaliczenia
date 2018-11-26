using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public StringBuilder sb = new StringBuilder();
        public string napis;
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                label1.Text = openFileDialog1.FileName;
                byte[] fileBytes = File.ReadAllBytes(label1.Text);
                string napis = Encoding.ASCII.GetString(fileBytes);
                foreach (byte b in fileBytes)
                textBox1.Text = napis.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
        }
        public int klucz;
        String[] nowy;
        public void button3_Click(object sender, EventArgs e)
        {
            klucz = System.Convert.ToInt32(textBox2.Text);
            label2.Text = "Obecnie użyty klucz:"+textBox2.Text;
            textBox3.Text = koduj(textBox1.Text);
        }
        protected string koduj(string napis_dokodowania)
        {
            string output = string.Empty;

            foreach (char ch in napis_dokodowania)
                output += znak(ch);

            return output;
        }
        protected char znak(char znak) {
            if (!char.IsLetter(znak))
            {
            return znak;
            }
            char d = char.IsUpper(znak) ? 'A' : 'a';
            return (char)((((znak + klucz) - d) % 26) + d);
        }

        protected string decode(string napis_dokodowania)
        {
            klucz = 26-klucz;
            return koduj(napis_dokodowania);
        }
        protected void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = decode(textBox1.Text);
        }
    }
}
