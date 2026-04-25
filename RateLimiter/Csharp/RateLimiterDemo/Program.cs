using RateLimiterDemo.Core.Enums;
using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.Factory;
using RateLimiterDemo.Services;
using RateLimiterDemo.Storage;

namespace RateLimiterDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to RateLimiter Demo !!");

            // Initialize storage (Singleton)
            InMemoryStorage db = InMemoryStorage.GetInstance();

            // Create RateLimiter using Factory (Factory)
            IRateLimiter limiter = RateLimiterFactory.Create(RateLimiterType.FixedWindow, db);

            // Create Service
            RateLimiterService service = new RateLimiterService(limiter);

            // testing
            await(testing(service));

            // printStorage
            PrintStorage(db);
        }

        static async Task testing(RateLimiterService service)
        {
            // Simulate multiple users
            List<string> users = new List<string>
            {
                "Amit",
                "Pranshul",
                "Chandu"
            };

            List<Task> tasks = new List<Task>();

            foreach (var user in users)
            {
                tasks.Add(Task.Run(() => SendReq(user, service)));
            }
            //tasks.Add(Task.Run(() => SimulateUser("Amit", service)));

            // Wait for all users to complete
            await Task.WhenAll(tasks);

            Console.WriteLine("Simulation complete.");
        }

        static async Task SendReq(string userId, RateLimiterService service)
        {
            for (int i = 1; i <= 5; i++)
            {
                bool allowed = service.HandleRequest(userId);

                Console.WriteLine($"User: {userId}, Req: {i}, {(allowed ? "Allowed" : "Blocked")}");

                // simulate delay between requests
                //await Task.Delay(900);
            }
        }

        static void PrintStorage(InMemoryStorage storage)
        {
            Console.WriteLine("\n===== STORAGE DUMP =====");

            foreach (var kvp in storage.Requests)
            {
                Console.WriteLine($"\nUser: {kvp.Key}");

                foreach (var time in kvp.Value)
                {
                    Console.WriteLine("  " + time);
                }
            }

            Console.WriteLine("========================\n");
        }
    }
}