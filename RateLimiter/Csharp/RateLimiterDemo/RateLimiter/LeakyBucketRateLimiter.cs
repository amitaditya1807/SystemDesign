using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.Storage;

namespace RateLimiterDemo.RateLimiter
{
    public class LeakyBucketRateLimiter : IRateLimiter
    {
        private readonly InMemoryStorage _storage;
        private readonly int _capacity;
        private readonly int _leakRatePerSecond;

        private readonly Queue<DateTime> _queue = new Queue<DateTime>();
        private readonly object _lock = new object();

        public LeakyBucketRateLimiter(InMemoryStorage storage, int capacity, int leakRatePerSecond)
        {
            _capacity = capacity;
            _leakRatePerSecond = leakRatePerSecond;
            _storage = storage;
            StartLeaking();
        }

        private void StartLeaking()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    lock (_lock)
                    {
                        int toRemove = _leakRatePerSecond;

                        while (toRemove > 0 && _queue.Count > 0)
                        {
                            _queue.Dequeue();
                            toRemove--;
                        }
                    }

                    await Task.Delay(1000); // every second
                }
            });
        }

        public bool AllowRequest(string userId)
        {
            lock (_lock)
            {
                if (!_storage.Requests.ContainsKey(userId))
                {
                    _storage.Requests[userId] = new List<DateTime>(); // create list
                }
                if (_queue.Count >= _capacity)
                    return false;

                _storage.Save(userId, DateTime.UtcNow);
                _queue.Enqueue(DateTime.UtcNow);
                return true;
            }
        }
    }
}
