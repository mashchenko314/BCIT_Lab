using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Kvadrat:Rectangle,IPrint
    {
        public Kvadrat (double widht,double lenght) : base(widht,lenght) {
            property_width = lenght;
        }
        public override string ToString()
        {
            string sl, s, s1;
            s = Convert.ToString(property_length*property_length);
            s1 = Convert.ToString(property_length);
            sl = "длина= " + s1 + "; площадь квадрата= " + s;
            return sl;
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
