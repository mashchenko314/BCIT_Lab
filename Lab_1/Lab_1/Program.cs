using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static public void Main(string[] args)
        {
            double a, b, c, D; string a_str, b_str, c_str; int f = 0;
            double y1, y2, x1, x2, x3, x4;
            if (args.Length == 0)
            {
                Console.Title = "Мащенко Елена ИУ5-35Б";
                Console.WriteLine("Введите коэффициенты для биквадратного уравнения");
                Console.WriteLine("a= ");
                a_str = Console.ReadLine();
                try
                {
                    a = Convert.ToDouble(a_str);
                }
                catch (FormatException)
                {
                    f = 1;
                    while (f == 1)
                    {
                        f = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка ввода");
                        Console.ResetColor();
                        Console.WriteLine("Введите коэффициент повторно, а= ");
                        a_str = Console.ReadLine();
                        try
                        {
                            a = Convert.ToDouble(a_str);
                        }
                        catch
                        {
                            f = 1;
                        }
                    }
                }
                a = Convert.ToDouble(a_str);
                Console.ResetColor();
                Console.WriteLine("b= ");
                b_str = Console.ReadLine();
                try
                {
                    b = Convert.ToDouble(b_str);
                }
                catch (FormatException)
                {
                    f = 1;
                    while (f == 1)
                    {
                        f = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка ввода");
                        Console.ResetColor();
                        Console.WriteLine("Введите коэффициент повторно, b= ");
                        b_str = Console.ReadLine();
                        try
                        {
                            b = Convert.ToDouble(b_str);
                        }
                        catch
                        {
                            f = 1;
                        }
                    }
                }
                b = Convert.ToDouble(b_str);
                Console.ResetColor();
                Console.WriteLine("c= ");
                c_str = Console.ReadLine();
                try
                {
                    c = Convert.ToDouble(c_str);
                }
                catch (FormatException)
                {
                    f = 1;
                    while (f == 1)
                    {
                        f = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Введите коэффициент повторно, c= ");
                        Console.ResetColor();
                        Console.WriteLine("Ошибка ввода");
                        c_str = Console.ReadLine();
                        try
                        {
                            c = Convert.ToDouble(c_str);
                        }
                        catch
                        {
                            f = 1;
                        }
                    }
                }
                Console.ResetColor();
                c = Convert.ToDouble(c_str);
            }
            else
            {
                a = Convert.ToDouble(args[0]);
                b = Convert.ToDouble(args[1]);
                c = Convert.ToDouble(args[2]);

            }
            D = Math.Pow(b, 2) - 4 * a * c;
            if (D < 0) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("D= " + D + "=> нет корней"); }
            else
            {
                if ((a == 0) && (b == 0)) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(" нет корней"); }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("D= " + D);
                    if (a == 0)
                    {
                        if ((-c) / b >= 0)
                        {
                            x1 = Math.Sqrt((-c) / b);
                            x2 = -x1;
                            Console.WriteLine("x1= " + x1);
                            Console.WriteLine("x2= " + x2);
                        }
                        else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("корней нет"); }
                    }
                    else
                    {
                        if (b == 0)
                        {
                            if ((-c) / a >= 0)
                            {
                                x1 = Math.Pow((-c) / a, 0.25);
                                x2 = -x1;
                                Console.WriteLine("x1= " + x1);
                                Console.WriteLine("x2= " + x2);
                            }
                            else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("корней нет"); }
                        }
                        else
                        {
                            if (D == 0)
                            {
                                y1 = (-b) / (2 * a);
                                x1 = Math.Sqrt(y1);
                                x2 = -x1;
                                if (y1 > 0)
                                {
                                    Console.WriteLine("x1= " + x1);
                                    Console.WriteLine("x2= " + x2);
                                }
                                if (y1==0)
                                {
                                    Console.WriteLine("x1= " + x1);
                                }
                                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("корней нет"); }
                            }
                            else
                            {
                                y1 = (-b + Math.Sqrt(D)) / (2 * a);
                                y2 = (-b - Math.Sqrt(D)) / (2 * a);
                                if ((y1 > 0) && (y2 > 0))
                                {
                                    x1 = Math.Sqrt(y1);
                                    x2 = -x1;
                                    x3 = Math.Sqrt(y2);
                                    x4 = -x3;
                                    Console.WriteLine("x1= " + x1);
                                    Console.WriteLine("x2= " + x2);
                                    Console.WriteLine("x3= " + x3);
                                    Console.WriteLine("x4= " + x4);

                                }
                                else if ((y1 <= 0) && (y2 > 0))
                                {
                                    if (y1 == 0) Console.WriteLine("x1= 0");
                                    x3 = Math.Sqrt(y2);
                                    x4 = -x3;
                                    Console.WriteLine("x1= " + x3);
                                    Console.WriteLine("x2= " + x4);

                                }
                                else if ((y2 <= 0) && (y1 > 0))
                                {
                                    if (y2 == 0) Console.WriteLine("x1= 0 ");
                                    x3 = Math.Sqrt(y1);
                                    x4 = -x3;
                                    Console.WriteLine("x1= " + x3);
                                    Console.WriteLine("x2= " + x4);
                                }
                                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("корней нет"); }


                            }
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}