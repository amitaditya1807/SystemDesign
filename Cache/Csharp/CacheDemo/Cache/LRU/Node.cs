using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheDemo.Cache.LRU
{
    public class Node
    {
        public int Key { get; set; }
        public int Value { get; set; }

        public Node Prev { get; set; }
        public Node Next { get; set; }

        public Node(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
