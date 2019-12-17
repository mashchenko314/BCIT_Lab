using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    class Program
    {
        static void Main(string[] args)
        {
            //реализация делегатов

            int a = 3;
            int b = 2;
            Console.WriteLine("a = " + a.ToString());
            Console.WriteLine("b = " + b.ToString());
            Console.WriteLine("Вычисление a - b различными способами ");
            Console.WriteLine("\nИспользуемый далее метод принимает делегат в качестве одного из входных параметров");
            Delegates.Del.PlusOrMinusMethod("Параметр - метод, соответствующий делегату: ", a, b, Delegates.Del.Minus);
            Delegates.Del.PlusOrMinusMethod("Параметр - лямбда-выражение 1: ", a, b,
                (int x, int y) =>
                {
                    int z = x - y;
                    return z;
                }
            );
            Delegates.Del.PlusOrMinusMethod("Параметр - лямбда-выражение 2: ", a, b,
                (x, y) =>
                {
                    return x - y;
                }
            );
            Delegates.Del.PlusOrMinusMethod("Параметр - лямбда-выражение 3: ", a, b, (x, y) => x - y);
            Console.WriteLine("\nИспользуемый далее метод принимает обобщенный делегат Func<> в качестве одного из входных параметров");
            Delegates.Del.PlusOrMinusMethodFunc("Параметр - метод, соответствующий делегату: ", a, b, Delegates.Del.Minus);
            Delegates.Del.PlusOrMinusMethodFunc("Параметр - лямбда-выражение 1: ", a, b,
                (int x, int y) =>
                {
                    int z = x - y;
                    return z;
                }
            );
            Delegates.Del.PlusOrMinusMethodFunc("Параметр - лямбда-выражение 2: ", a, b,
                (x, y) =>
                {
                    return x - y;
                }
            );
            Delegates.Del.PlusOrMinusMethodFunc("Параметр - лямбда-выражение 3: ", a, b, (x, y) => x - y);

            //реализация рефлексии

            for (int i = 0; i < 100; i += 1)
                Console.Write("-");
            Console.WriteLine("\n");
            Type t = typeof(Reflection.Ref.sity);
            Console.WriteLine("Тип " + t.FullName + " унаследован от " + t.BaseType.FullName);
            Console.WriteLine("Пространство имен " + t.Namespace);
            Console.WriteLine("Находится в сборке " + t.AssemblyQualifiedName);
            Console.WriteLine("\nКонструкторы:");
            foreach (var x in t.GetConstructors())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nМетоды:");
            foreach (var x in t.GetMethods())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nСвойства:");
            foreach (var x in t.GetProperties())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nПоля данных (public):");
            foreach (var x in t.GetFields())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nСвойства, помеченные атрибутом:");
            foreach (var x in t.GetProperties())
            {
                object attrObj;
                if (Reflection.Ref.GetPropertyAttribute(x, typeof(Reflection.Ref.NewAttribute), out attrObj))
                {
                    Reflection.Ref.NewAttribute attr = attrObj as Reflection.Ref.NewAttribute;
                    Console.WriteLine(x.Name + " - " + attr.Description);
                }
            }
            Console.WriteLine("\nВызов метода:");
            //Создание объекта
            //Можно создать объект через рефлексию
            Reflection.Ref.sity fi = (Reflection.Ref.sity)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { });
            //Параметры вызова метода
            object[] parameters = new object[] { 3, 2 };
            object Result = t.InvokeMember("integration", BindingFlags.InvokeMethod, null, fi, parameters);    //Вызов метода
            Console.WriteLine("integration(30000,20000)={0}", Result);
            Console.ReadLine();
        }
    }
}



