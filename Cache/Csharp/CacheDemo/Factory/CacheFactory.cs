using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cache.Core.Interfaces;
using CacheDemo.Cache;
using CacheDemo.Cache.LRU;
using CacheDemo.Core.Enums;

namespace CacheDemo.Factory
{
    public class CacheFactory
    {
        public static ICache CreateCache(CacheType type, int capacity)
        {
            switch (type)
            {
                case CacheType.LRU:
                    return new LRUCache(capacity);

                case CacheType.LFU:
                    throw new NotImplementedException("LFU not implemented yet");

                case CacheType.FIFO:
                    throw new NotImplementedException("FIFO not implemented yet");

                default:
                    throw new ArgumentException("Invalid cache type");
            }

        }
    }
}
