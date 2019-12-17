using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public delegate int PlusOrMinus(int p1, int p2);
    class Del
    {
        //Методы, реализующие делегат (методы "типа" делегата)
        public static int Plus(int p1, int p2) { return p1 + p2; }
        public static int Minus(int p1, int p2) { return p1 - p2; }
        static public void PlusOrMinusMethodFunc(string str, int i1, int i2, Func<int, int, int> PlusOrMinusParam)     // Использование обощенного делегата Func<>
        {
            int Result = PlusOrMinusParam(i1, i2);
            Console.WriteLine(str + Result.ToString());
            // Func<int, string, bool> - делегат принимает параметры типа int и string и возвращает bool
            // Если метод должен возвращать void, то используется делегат Action
            // Action<int, string> - делегат принимает параметры типа int и string и возвращает void
            // Action как правило используется для разработки групповых делегатов, которые используются в событиях
        }
        static public void PlusOrMinusMethod(string str, int i1, int i2, PlusOrMinus PlusOrMinusParam)        /// Использование делегата
        {
            int Result = PlusOrMinusParam(i1, i2);
            Console.WriteLine(str + Result.ToString());
        }
    }
}
