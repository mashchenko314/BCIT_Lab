using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Ref
    {
        public static bool GetPropertyAttribute(PropertyInfo checkType, Type attributeType, out object attribute)      // Проверка, что у свойства есть атрибут заданного типа
        {
            bool Result = false;
            attribute = null;
            var isAttribute = checkType.GetCustomAttributes(attributeType, false);      //Поиск атрибутов с заданным типом
            if (isAttribute.Length > 0)
            {
                Result = true;
                attribute = isAttribute[0];
            }
            return Result;
        }
        // Класс атрибута
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public class NewAttribute : Attribute
        {
            public NewAttribute() { }
            public NewAttribute(string DescriptionParam)
            {
                Description = DescriptionParam;
            }
            public string Description { get; set; }
        }
        public class sity
        {
            private int _numbers;
            private string _title;
            public sity() { }
            public sity(int c, string s)
            {
                numbers = c;
                title = s;
            }
            public sity(string s, int c)
            {
                numbers = c;
                title = s;
            }
            public int numbers
            {
                get { return _numbers; }
                private set { _numbers = value; }
            }
            public string title
            {
                get { return _title; }
                private set { _title = value; }
            }
            public int integration(int x, int y) { return x + y; }
            public int area;
            public int established_in;
            public string mayor;
        }

      
    }
}