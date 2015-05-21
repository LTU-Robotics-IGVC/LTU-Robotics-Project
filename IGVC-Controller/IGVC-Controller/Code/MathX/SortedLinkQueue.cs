using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class LinkNode<T>
    {
        public T item;
        public int priority;
        public LinkNode<T> leftNode;
        public LinkNode<T> rightNode;
    }

    class SortedLinkQueue<T>
    {
        LinkNode<T> firstNode;
        LinkNode<T> lastNode;

        int count;

        public SortedLinkQueue()
        {
            this.count = 0;
            firstNode = null;
            lastNode = null;
        }

        public void Enqueue(T item, int itemPriority)
        {
            LinkNode<T> node = new LinkNode<T>();
            node.item = item;
            node.priority = itemPriority;
            this.count++;

            if(firstNode == null)
            {
                //list is empty
                firstNode = node;
                lastNode = node;
            }
            else
            {
                //case : goes last on the list
                if(lastNode.priority < itemPriority)
                {
                    lastNode.rightNode = node;
                    node.leftNode = lastNode;
                    lastNode = node;
                    return;
                }

                //case : goes first on the list
                if(firstNode.priority > itemPriority)
                {
                    firstNode.leftNode = node;
                    node.rightNode = firstNode;
                    firstNode = node;
                    return;
                }

                //case : between first and last nodes
                //Note that we are starting from the first
                //as for pathfinding we are working with the shortest
                //paths so any new paths added are likely to go earlier on the list
                LinkNode<T> iterNode = firstNode;
                while(iterNode.rightNode != null && iterNode.priority <= itemPriority)
                {
                    iterNode = iterNode.rightNode;
                }
                //couple the nodes (2x2connections = 4 connections)
                node.rightNode = iterNode;
                node.leftNode = iterNode.leftNode;
                iterNode.leftNode.rightNode = node;
                iterNode.leftNode = node;
            }
        }

        public T Dequeue()
        {
            if(count > 0)
            {
                count--;
                LinkNode<T> node = firstNode;
                firstNode = firstNode.rightNode;
                if(firstNode != null)
                    firstNode.leftNode = null;
                return node.item;
            }

            throw new Exception("List is empty");
        }

        public bool isEmpty()
        {
            return count == 0;
        }
    }
}
