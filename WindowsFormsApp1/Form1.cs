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
        public int klucz;
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                label1.Text = openFileDialog1.FileName;
                byte[] fileBytes = File.ReadAllBytes(label1.Text);
                string napis = Encoding.ASCII.GetString(fileBytes);
                
                textBox1.Text = napis.ToString();
                textBox3.Text = napis.ToString();
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
            if (textBox2.Text=="")
            {
                MessageBox.Show("Nie podano klucza");
            }
            else
            {
            klucz = System.Convert.ToInt32(textBox2.Text);
            label2.Text = "Obecnie użyty klucz:" + textBox2.Text;
            textBox3.Text = Encrypt(klucz, textBox1.Text);
            }
            
        }
        protected void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Nie podano klucza");
            }
            else
            {
                klucz = System.Convert.ToInt32(textBox2.Text);
                label2.Text = "Obecnie użyty klucz:" + textBox2.Text;
                textBox1.Text = Decrypt(klucz, textBox3.Text);
            }
            
        }

        public static string Encrypt(int klucz, string dokodowania)
        {
            List<string> lista = new List<string>();
            for (int i = 0; i < klucz; i++)
            {
                lista.Add("");
                //init
            }
        //początek
            int wiersz = 0;
            int kierunek = 1;
            foreach (char c in dokodowania)
            {
        //zmiana kierunku po natrafia na góre i dół płotka
                if (wiersz + kierunek == klucz || wiersz + kierunek == -1)
                {
                    kierunek = kierunek * (-1);
                }
                
                lista[wiersz] += c;//dodanie do danego wiersza(stringa) kolejnego znaku
                wiersz += kierunek;//wskazanie kolejnego wiersza
            }

            //zrzucanie wartości z listy do jednego napisu
            string tekst = "";
            foreach (string s in lista)
            {
                tekst += s;
            }

            return tekst;
        }

        public static string Decrypt(int klucz, string zakodowany)
        {
            int rozmiar = zakodowany.Length;
            List<List<int>> lista = new List<List<int>>();//podwójna lista
            for (int i = 0; i < klucz; i++)
            {
                lista.Add(new List<int>());
            }

            int wiersz = 0;
            int kierunek = 1;
            //wrzucanie wskażników do listy z poprawką na rozmiar żeby wiedzieć gdzie koniec
            for (int i = 0; i < rozmiar; i++)
            {
                //tak jak wyżej zmiana znaku po trafieniu w krawędzie siatki/płotka
                if (wiersz + kierunek == klucz || wiersz + kierunek == -1)
                {
                    kierunek = kierunek * (-1);
                }
                lista[wiersz].Add(i);//numerowanie pozycji dla liter
                wiersz += kierunek;
            }

            int licznik = 0;
            char[] buffer = new char[rozmiar];
            //wrzucanie liter dla odpowiednich wskażników 
            for (int i = 0; i < klucz; i++)
            {
                for (int j = 0; j < lista[i].Count; j++)
                {
                    //wrzucanie liter w odpowiadające im miejsce | w bufer żeby można było wyciągnać z niego odrazu znaki
                    buffer[lista[i][j]] = zakodowany[licznik];
                    licznik++;
                }
            }
            //new string bo rzutowanie bufera(char) na string
            return new string(buffer);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "zakodowany";
            saveFileDialog1.Filter= "Dokument tekstowy (*.txt)|*.txt";
            saveFileDialog1.ShowDialog();
            
            string location;
            location = saveFileDialog1.FileName;

            File.WriteAllText(location , textBox3.Text);
        }
    }
}