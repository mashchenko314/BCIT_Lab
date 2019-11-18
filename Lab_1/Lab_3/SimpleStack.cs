using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class SimpleStack<T> : SimpleList<T> where T : IComparable
    {
        public void Push(T element) {
            Add(element);
        }
        public T Pop()
        {
            T element = Get(Count - 1);
             GetItem(Count - 1);
            Count--;
            return element;

        }
    }
}
 