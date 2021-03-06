﻿using System;
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
            if (count >= size)
            {
                size = ++count;
                T[] newList = new T[size];
                int[] newPriority = new int[size];

                int i;
                for (i = size - 1; i >= 1 && priority[i-1] > itemPriority; i--)
                {
                    newList[i] = list[i - 1];
                    newPriority[i] = priority[i - 1];
                }

                newList[i] = item;
                newPriority[i] = itemPriority;
                i--;

                while (i >= 0)
                {
                    newList[i] = list[i];
                    newPriority[i] = priority[i];
                    i--;
                }

                this.list = newList;
                this.priority = newPriority;
            }
            else
            {
                if (count == 0)
                {
                    list[0] = item;
                    priority[0] = itemPriority;

                    count++;
                }
                else
                {

                    count++;

                    int i;
                    for (i = count - 1; i >= 1 && priority[i-1] > itemPriority; i--)
                    {
                        list[i] = list[i - 1];
                        priority[i] = priority[i - 1];
                    }

                    list[i] = item;
                    priority[i] = itemPriority;

                    //The rest of the list does not need to be changed
                }
            }
        }

        public T Dequeue()
        {
            if(count > 0)
            {
                T item = list[0];
                for (int i = 1; i < count; i++)
                {
                    list[i - 1] = list[i];
                    priority[i - 1] = priority[i];
                }
                count--;
                return item;
            }

            throw new Exception("Queue is empty");
        }

        public bool isEmpty()
        {
            return count == 0;
        }
    }
}
