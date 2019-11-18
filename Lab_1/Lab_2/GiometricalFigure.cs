using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public abstract class GiometricalFigure : IComparable
    {
        public abstract double Square();

        //public void Print() {
        //    Console.WriteLine(this.ToString());
        //}
        public int CompareTo(object o)
        {
            GiometricalFigure p = o as GiometricalFigure;
            return this.Square().CompareTo(p.Square());

        }
    }
}
