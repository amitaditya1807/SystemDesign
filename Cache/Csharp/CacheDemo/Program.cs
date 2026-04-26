using CacheDemo.Core.Enums;
using CacheDemo.Factory;
using CacheDemo.Service;

namespace CacheDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Cache system design !!");

            // create cache object
            var cache = CacheFactory.CreateCache(CacheType.LRU, 2);

            // create cacheService which takes cache as input
            var service = new CacheService(cache);

            // testing our service
            await (testing(service));

            Console.WriteLine("Simulation Complete");
        }

        static async Task testing(CacheService service)
        {
            service.Put(1, 1);
            service.Put(2, 2);

            Console.WriteLine(service.Get(1)); // HIT

            service.Put(3, 3); // evicts 2

            Console.WriteLine(service.Get(2)); // MISS
        }
    }
}