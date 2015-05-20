using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class SortedQueue<T>
    {
        T[] list;
        //lowest priority value is first out
        int[] priority;
        int size;
        int count;

        public SortedQueue()
        {
            this.size = 0;
            this.count = 0;
        }

        public void Enqueue(T item, int itemPriority)
        {
            //Check if queue size needs to be increased
            if (size == count)
            {
                size = count++;
                T[] newList = new T[size];
                int[] newPriority = new int[size];

                int i;
                for (i = size-1; i >= 1 && priority[i] > itemPriority; i--)
                {
                    newList[i] = list[i-1];
                    newPriority[i] = priority[i-1];
                }

                newList[i] = item;
                newPriority[i] = itemPriority;
                i--;

                while(i >= 0)
                {
                    newList[i] = list[i];
                    newPriority[i] = priority[i];
                }

                this.list = newList;
                this.priority = newPriority;
            }
        }
    }
}
