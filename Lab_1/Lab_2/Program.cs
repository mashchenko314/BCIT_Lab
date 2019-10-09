using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            double s;
            Circle b=new Circle(0), a=new Circle(3);
            Kvadrat c = new Kvadrat(5, 5);
            Rectangle d = new Rectangle(2, 4);
            Console.WriteLine("введите радиус круга:");
            s=Convert.ToDouble( Console.ReadLine());
            b.property_radius = s;
            a.Print();
            b.Print();
            c.Print();
            d.Print();
            Console.ReadLine();
        }
    }
}
