using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//Encryption
//The the plaintext(P) and key(K) are added modulo 26.
//Ei = (Pi + Ki) mod 26
//Decryption
//Di = (Ei - Ki + 26) mod 26
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
            //wczytanie pliku
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
        public void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox1.Text == "")
            {
                MessageBox.Show("Nie podano klucza lub tekstu do zakodowania");
            }
            else {
            textBox3.Text = koduj(textBox1.Text, textBox4.Text);
            }
        }
        //stworzenie długiego klucza (powtarzanie)
        protected string dajklucz(string napis_dokodowania, string klucz)
        {
            int x = napis_dokodowania.Length;
            for (int i = 0; ; i++)
            {
                if (x == i)
                    i = 0;
                if (klucz.Length == napis_dokodowania.Length)
                    break;
                klucz += klucz[i];
            }
            //zwraca długi klucz
            return klucz;
        }
        protected string koduj(string napis_dokodowania, string klucz)
        {
            //zwraca długi klucz 
            klucz = dajklucz(napis_dokodowania, klucz);
            //długi klucz wskakuje odrazu na miejsce starego w polu tekstowym
            textBox4.Text= dajklucz(napis_dokodowania, klucz);
            
            string zakodowany = "";
            for (int i = 0; i < napis_dokodowania.Length; i++)
            {
                int x2 = (napis_dokodowania[i] + klucz[i]) % 256;
                zakodowany += (char)x2;
            }
            return zakodowany;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //zapis klucza

            saveFileDialog2.FileName = "klucz";
            saveFileDialog2.Filter = "Dokument tekstowy (*.txt)|*.txt";
            saveFileDialog2.ShowDialog();

            string location;
            location = saveFileDialog2.FileName;
            File.WriteAllText(location, textBox4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //generowanie klucza
            if (textBox2.Text == "")
            {
                MessageBox.Show("Nie podano długości klucza");
            }
            else
            {
            int dlugosc_klucza = System.Convert.ToInt32(textBox2.Text);
            label2.Text = "Obecnie użyty klucz:" + textBox2.Text;
            generator(dlugosc_klucza);
            }
        }

        private string generator(int dlugosc_klucza)
        {
            Random rand = new Random();
            string klucz = "";
            for(int i=0; i<=dlugosc_klucza; i++)
            {
                
                klucz +=(char)rand.Next(31,256);
            }
            textBox4.Text = klucz;
            return klucz;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //wczytywanie klucza
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label3.Text = openFileDialog2.FileName;
                byte[] fileBytes2 = File.ReadAllBytes(label3.Text);
                string napis2 = Encoding.ASCII.GetString(fileBytes2);
                foreach (byte b2 in fileBytes2)
                    textBox4.Text = napis2.ToString();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //zapis pliku 
            saveFileDialog1.FileName = "zakodowany";
            saveFileDialog1.Filter = "Dokument tekstowy (*.txt)|*.txt";
            saveFileDialog1.ShowDialog();

            string location;
            location = saveFileDialog1.FileName;
            File.WriteAllText(location, textBox3.Text);
        }
        protected string dekoduj(string zakodowany, string klucz)
        {

            string odkodowany = "";

            for (int i = 0; i < zakodowany.Length; i++)
            {
                int x = (zakodowany[i] - klucz[i] + 256) % 256;
                odkodowany += (char)x;
            }
            return odkodowany;

        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Nie podano klucza");
            }
            else
            {
                textBox1.Text = dekoduj(textBox3.Text, textBox4.Text);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
