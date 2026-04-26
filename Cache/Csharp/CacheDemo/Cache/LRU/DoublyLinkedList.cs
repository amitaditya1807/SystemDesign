using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheDemo.Cache.LRU
{
    public class DoublyLinkedList
    {
        private Node head;
        private Node tail;

        public DoublyLinkedList()
        {
            head = new Node(0, 0); // dummy head
            tail = new Node(0, 0); // dummy tail

            head.Next = tail;
            tail.Prev = head;
        }

        public void AddToFront(Node node)
        {
            node.Next = head.Next;
            node.Prev = head;

            head.Next.Prev = node;
            head.Next = node;
        }

        public void RemoveNode(Node node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }

        public void MoveToFront(Node node)
        {
            RemoveNode(node);
            AddToFront(node);
        }

        public Node RemoveLast()
        {
            Node lru = tail.Prev;
            RemoveNode(lru);
            return lru;
        }
    }
}
