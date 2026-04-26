using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cache.Core.Interfaces;
using CacheDemo.Core.Enums;
using CacheDemo.Factory;

namespace CacheDemo.Service
{
    public class CacheService
    {
        private readonly ICache _cache;

        public CacheService(ICache cache)
        {
            _cache = cache;
        }

        public int Get(int key)
        {
            Console.WriteLine($"[CacheService] Get key: {key}");

            int value = _cache.Get(key);

            if (value == -1)
                Console.WriteLine("[CacheService] Cache MISS");
            else
                Console.WriteLine("[CacheService] Cache HIT");

            return value;
        }

        public void Put(int key, int value)
        {
            Console.WriteLine($"[CacheService] Put key: {key}, value: {value}");
            _cache.Put(key, value);
        }
    }
}
