using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> list = new List<string>();
        private void button3_Click(object sender, EventArgs e)
        {
            string word = this.textBox3.Text.Trim();        //Слово для поиска
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)     //Если слово для поиска не пусто
            {
                string wordUpper = word.ToUpper();   
                List<string> tempList = new List<string>();      
                Stopwatch t = new Stopwatch(); //определение времени поиска
                t.Start();
                foreach (string str in list)
                {
                    if (str.ToUpper().Contains(wordUpper))
                    {
                        tempList.Add(str);
                    }
                }
                if (tempList.Count == 0)
                {
                    MessageBox.Show("Искомое слово не найдено!");
                }
                t.Stop();
                this.textBox2.Text = t.Elapsed.ToString();
                listBox1.SelectedIndex = listBox1.FindStringExact(textBox3.Text);
                L_distanse(textBox3.Text);
            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл и ввести слово для поиска");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C:/mygit/BCIT_Lab/Lab_1";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                string path = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(path);

                string text = sr.ReadToEnd();

                char[] spl= { ' ', ',', '.', ';', ':', '(', ')', '\n' };
                string[] words_str = text.Split(spl);

                for (int j = 0; j < words_str.Length; j++)
                {
                    if ((String.Compare(words_str[j], "\0") != 0) && (String.Compare(words_str[j], "\n") != 0) && (String.Compare(words_str[j], "\r") != 0))
                    {
                        if (!list.Contains(words_str[j]))
                        {
                            list.Add(words_str[j]);
                        }
                    }

                }
                stopWatch.Stop();

                TimeSpan time = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10);
                textBox1.Text = elapsedTime;
                add_to_list_box(list);


            }

        }
        void add_to_list_box(List<string> arr)
        {
          
            listBox1.Items.Clear();
            listBox1.BeginUpdate();
            foreach (string l in arr)
            {
                listBox1.Items.Add(l);
            }
            listBox1.EndUpdate();
            
        }
        int L_distanse(string word)
        {
            listBox2.Items.Clear();
            int a = 0;
            bool f = int.TryParse(textBox4.Text, out a);
            if (!f || a < 0 || a % 1 != 0)
            {
                MessageBox.Show("Параметр введен неверно!");
                listBox2.Items.Clear();
                return 0;
            }
            listBox2.BeginUpdate();
            foreach (string s in listBox1.Items)
            {
                if (Lab_5.DisDamLev.Distance(word, s) <= a)
                    listBox2.Items.Add(s);
            }
            listBox2.EndUpdate();
            return 0;
        }

       
    }
}
