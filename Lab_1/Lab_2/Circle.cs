using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class Circle : GiometricalFigure,IPrint
    {
        private double _property_radius = 0;
        public double property_radius
        {
            get { return _property_radius; }
            set { _property_radius = value; }

        }
        public Circle (double radius)
        {
            this._property_radius = radius;
        }
        override public double Square()
        {
            return 2 * Math.PI * _property_radius;
        }
        public override string ToString()
        {
            string sl,s,s1;
            s = Convert.ToString(this.Square());
            s1 = Convert.ToString(_property_radius);
            sl = "радиус= " + s1 + "; площадь круга= " + s;
            return sl;
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
