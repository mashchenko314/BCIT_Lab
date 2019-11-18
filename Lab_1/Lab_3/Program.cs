using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_2;


namespace Lab_3
{
    class Program3
    {
        static void Main(string[] args)
        {
            Rectangle a = new Rectangle(2, 3);
            Kvadrat b = new Kvadrat(3,3);
            Circle c = new Circle(4);
            Rectangle d = new Rectangle(2, 1);
            Kvadrat e = new Kvadrat(2.5, 2.5);

            //Пример работы с ArrayList
            ArrayList list = new ArrayList() {a,b,c};
            foreach (GiometricalFigure figure in list)
            {
                
                Console.WriteLine(figure.Square().ToString());
            }

            //Пример работы с List<T> и его сортировка
            List<GiometricalFigure> list1 = new List<GiometricalFigure>();
            list1.Add(a);
            list1.Add(b);
            list1.Add(c);
            list1.Add(d);
            list1.Add(e);
            Console.WriteLine("Неотсортированный list:");
            foreach (GiometricalFigure figure in list1)
            {

                Console.WriteLine(figure.Square().ToString());
            }
            int n = list1.Count-1;
            GiometricalFigure figure1;
            for (int j=0; j < list1.Count-1; j++)
            {
                for (int i = 0; i < n; i++)
                    if (list1[i].CompareTo(list1[i + 1])>0) {
                        figure1 = list1[i];
                        list1[i] = list1[i + 1];
                        list1[i + 1] = figure1;
                    }
                n--;

            }
            Console.WriteLine("Отсортированный list:");
            foreach (GiometricalFigure figure in list1)
            {

                Console.WriteLine(figure.Square().ToString());
            }

            //Пример работы SimpleStack
            Console.WriteLine("SimpleStack:");
            SimpleStack<GiometricalFigure> figures = new SimpleStack<GiometricalFigure>();
            figures.Push(a);
            figures.Push(b);
            figures.Push(c);
            while (figures.Count > 0)
            {
              Console.WriteLine( figures.Pop());
            }

            //Пример работы Matrix
            Console.WriteLine("Matrix:");
            Rectangle nullElement = new Rectangle(0, 0);
            Matrix<GiometricalFigure> matrix = new Matrix<GiometricalFigure>(2, 2, 2, nullElement );
            matrix[1, 0, 0] = b;
            matrix[0, 1, 1] = a;
            Console.WriteLine(matrix.ToString());

            Console.ReadKey();
        }
    }
}
