using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cache.Core.Interfaces;

namespace CacheDemo.Cache.LRU
{
    public class LRUCache : ICache
    {
        private int capacity;
        private Dictionary<int, Node> map;
        private DoublyLinkedList dll;

        private readonly object _lock = new object();

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            map = new Dictionary<int, Node>();
            dll = new DoublyLinkedList();
        }

        public int Get(int key)
        {
            lock(_lock)
            {
                if (!map.ContainsKey(key))
                    return -1;

                Node node = map[key];
                dll.MoveToFront(node);

                return node.Value;
            }
        }

        public void Put(int key, int value)
        {
            lock (_lock)
            {
                if (map.ContainsKey(key))
                {
                    Node node = map[key];
                    node.Value = value;
                    dll.MoveToFront(node);
                }
                else
                {
                    if (map.Count == capacity)
                    {
                        Node lru = dll.RemoveLast();
                        map.Remove(lru.Key);
                    }

                    Node newNode = new Node(key, value);
                    dll.AddToFront(newNode);
                    map[key] = newNode;
                }
            }
        }
    }
}
