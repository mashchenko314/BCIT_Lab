using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string required_word = textBox3.Text;

            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            foreach (string word in list)
            {
                if (word.Contains(required_word))
                {
                    listBox1.BeginUpdate();
                    listBox1.Items.Add(word);
                    listBox1.EndUpdate();
                }
            }
            stopWatch.Stop();

            TimeSpan time = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10);

            textBox2.Text = elapsedTime;

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

            }

        }
    }
}
