using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class Rectangle : GiometricalFigure,IPrint
    {
        private double _property_width = 0;
        private double _property_length = 0;
        public Rectangle(double width, double length)
        {
            this._property_width = width;
            this._property_length = length;
        }
        public double property_length
        {
            get { return _property_length; }
            set { _property_length = value; }

        }
        public double property_width
        {
            get { return _property_width; }
            set { _property_width = value; }

        }
        public override double Square()
        {
            double s;
            s = _property_length * _property_width;
            return s;
        }
        public override string ToString()
        {
            string sl, s, s1,s2;
            s = Convert.ToString(property_length*property_width);
            s1 = Convert.ToString(property_length);
            s2 = Convert.ToString(property_width);
            sl = "длина= " + s1 + "; ширина= " + s2+"; площадь прямоугольника= " + s;
            return sl;
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
