
using Lab_5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string word = this.textBox3.Text.Trim();            //Слово для поиска
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)      //Если слово для поиска не пусто
            {
                int maxDist;
                if (!int.TryParse(this.textBox4.Text.Trim(), out maxDist))
                {
                    MessageBox.Show("Необходимо указать максимальное расстояние");
                    return;
                }
                
                int ThreadCount;
                if (!int.TryParse(this.textBox5.Text.Trim(), out ThreadCount))
                {
                    MessageBox.Show("Необходимо указать количество потоков");
                    return;
                }
                Stopwatch timer = new Stopwatch();
                timer.Start();
                //-------------------------------------------------
                // Начало параллельного поиска
                //-------------------------------------------------
                List<ParallelSearchResult> Result = new List<ParallelSearchResult>();       //Результирующий список
                List<MinMax> arrayDivList = SubArrays.DivideSubArrays(0, list.Count, ThreadCount);      //Деление списка на фрагменты для параллельного запуска в потоках
                int count = arrayDivList.Count;
                Task<List<ParallelSearchResult>>[] tasks = new Task<List<ParallelSearchResult>>[count];    //Количество потоков соответствует количеству фрагментов массива
                for (int i = 0; i < count; i++)        //Запуск потоков
                {
                    List<string> tempTaskList = list.GetRange(arrayDivList[i].Min, arrayDivList[i].Max - arrayDivList[i].Min);     //Создание временного списка, чтобы потоки не работали параллельно с одной коллекцией
                    tasks[i] = new Task<List<ParallelSearchResult>>(
                     //Метод, который будет выполняться в потоке
                     ArrayThreadTask,
                     //Параметры потока
                     new ParallelSearchThreadParam()
                     {
                         tempList = tempTaskList,
                         maxDist = maxDist,
                         ThreadNum = i,
                         wordPattern = word
                     });
                    //Запуск потока
                    tasks[i].Start();
                }
                Task.WaitAll(tasks);
                timer.Stop();
                //Объединение результатов
                for (int i = 0; i < count; i++)
                {
                    Result.AddRange(tasks[i].Result);
                }
                //-------------------------------------------------
                // Завершение параллельного поиска
                //-------------------------------------------------
                timer.Stop();
                //Вывод результатов
                //Время поиска
                this.textBox6.Text = timer.Elapsed.ToString();
                //Вычисленное количество потоков
                this.textBox7.Text = count.ToString();
                //Начало обновления списка результатов
                this.listBox2.BeginUpdate();
                //Очистка списка
                this.listBox2.Items.Clear();
                //Вывод результатов поиска
                foreach (var x in Result)
                {
                    string temp = x.word + "(расстояние=" + x.dist.ToString() + " поток=" + x.ThreadNum.ToString() + ")";
                    this.listBox2.Items.Add(temp);
                }
                //Окончание обновления списка результатов
                this.listBox2.EndUpdate();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл и ввести слово для поиска");
            }
        }
        public class ParallelSearchResult
        {
            public string word { get; set; }// Найденное слово
            public int dist { get; set; }// Расстояние
            public int ThreadNum { get; set; }// Номер потока
        }
        public class MinMax
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public MinMax(int pmin, int pmax)
            {
                this.Min = pmin;
                this.Max = pmax;
            }
        }
        public static class SubArrays
        {
            // Деление массива на последовательности
            //beginIndex - Начальный индекс массива</param>
            //endIndex- Конечный индекс массива</param>
            //subArraysCount - Требуемое количество подмассивов</param>
            // <returns>Список пар с индексами подмассивов</returns>
            public static List<MinMax> DivideSubArrays(
            int beginIndex, int endIndex, int subArraysCount)
            {
                //Результирующий список пар с индексами подмассивов
                List<MinMax> result = new List<MinMax>();
                //Если число элементов в массиве слишком мало для деления
                //то возвращается массив целиком
                if ((endIndex - beginIndex) <= subArraysCount)
                {
                    result.Add(new MinMax(0, (endIndex - beginIndex)));
                }
                else
                {
                    //Размер подмассива
                    int delta = (endIndex - beginIndex) / subArraysCount;
                    //Начало отсчета
                    int currentBegin = beginIndex;
                    //Пока размер подмассива укладывается в оставшуюся
                    //последовательность
                    while ((endIndex - currentBegin) >= 2 * delta)
                    {
                        //Формируем подмассив на основе начала
                        //последовательности
                        result.Add(
                        new MinMax(currentBegin, currentBegin + delta));
                        //Сдвигаем начало последовательности
                        //вперед на размер подмассива
                        currentBegin += delta;
                    }
                    //Оставшийся фрагмент массива
                    result.Add(new MinMax(currentBegin, endIndex));
                }
                //Возврат списка результатов
                return result;
            }
        }
        public static List<ParallelSearchResult> ArrayThreadTask(object paramObj)
        {
            /// Выполняется в параллельном потоке для поиска строк
            ParallelSearchThreadParam param = (ParallelSearchThreadParam)paramObj;
            string wordUpper = param.wordPattern.Trim().ToUpper();  //Слово для поиска в верхнем регистре
            List<ParallelSearchResult> Result = new List<ParallelSearchResult>();//Результаты поиска в одном потоке
            foreach (string str in param.tempList) //Перебор всех слов во временном списке данного потока
            {
                int dist = DisDamLev.Distance(str.ToUpper(), wordUpper);//Вычисление расстояния Дамерау-Левенштейна
                if (dist <= param.maxDist)
                {
                    ParallelSearchResult temp = new ParallelSearchResult()
                    {
                        word = str,
                        dist = dist,
                        ThreadNum = param.ThreadNum
                    };
                    Result.Add(temp);
                }
            }
            return Result;
        }
        class ParallelSearchThreadParam
        {
            // Параметры которые передаются в поток
            // для параллельного поиска
            public List<string> tempList { get; set; } // Массив для поиска
            public string wordPattern { get; set; } // Слово для поиска>
            public int maxDist { get; set; } // Максимальное расстояние для нечеткого поиска
            public int ThreadNum { get; set; }   // Номер потока
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string TempReportFileName = "Report_" + DateTime.Now.ToString("dd_MM_yyyy_hhmmss"); //Имя файла отчета
            //Диалог сохранения файла отчета
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = TempReportFileName;
            fd.DefaultExt = ".html";
            fd.Filter = "HTML Reports|*.html";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string ReportFileName = fd.FileName;
                //-------------Формирование отчета----------
                StringBuilder b = new StringBuilder();
                b.AppendLine("<html>");
                b.AppendLine("<head>");
                b.AppendLine("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'/>");
                b.AppendLine("<title>" + "Отчет: " + ReportFileName + "</title>");
                b.AppendLine("</head>");
                b.AppendLine("<body>");
                b.AppendLine("<h1>" + "Отчет: " + ReportFileName + "</h1>");
                b.AppendLine("<table border='1'>");
                b.AppendLine("<tr>");
                b.AppendLine("<td>Время чтения из файла</td>");
                b.AppendLine("<td>" + this.textBox1.Text + "</td>");
                b.AppendLine("</tr>");
                b.AppendLine("<tr>");
                b.AppendLine("<td>Слово для поиска</td>");
                b.AppendLine("<td>" + this.textBox3.Text + "</td>");
                b.AppendLine("</tr>");
                b.AppendLine("<tr>");
                b.AppendLine("<td>Максимальное расстояние </td>");
                b.AppendLine("<td>" + this.textBox4.Text + "</td>");
                b.AppendLine("</tr>");
                b.AppendLine("<tr>");
                b.AppendLine("<td>Время четкого поиска</td>");
                b.AppendLine("<td>" + this.textBox2.Text + "</td>");
                b.AppendLine("</tr>");
                b.AppendLine("<tr>");
                b.AppendLine("<td>Время нечеткого поиска</td>");
                b.AppendLine("<td>" + this.textBox6.Text + "</td>");
                b.AppendLine("</tr>");
                b.AppendLine("<tr>");
                b.AppendLine("<td>Количество потоков</td>");
                b.AppendLine("<td>" + this.textBox5.Text + "</td>");
                b.AppendLine("</tr>");
                b.AppendLine("<tr valign='top'>");
                b.AppendLine("<td>Результаты поиска</td>");
                b.AppendLine("<td>");
                b.AppendLine("<ul>");
                foreach (var x in this.listBox2.Items)
                {
                    b.AppendLine("<li>" + x.ToString() + "</li>");
                }
                b.AppendLine("</ul>");
                b.AppendLine("</td>");
                b.AppendLine("</tr>");
                b.AppendLine("</table>");
                b.AppendLine("</body>");
                b.AppendLine("</html>");
                //Сохранение файла
                File.AppendAllText(ReportFileName, b.ToString());
                MessageBox.Show("Отчет сформирован. Файл: " + ReportFileName);
            }
        }
    }
}
