using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.Storage;

namespace RateLimiterDemo.RateLimiter
{
    public class SlidingWindowRateLimiter : IRateLimiter
    {
        Queue<DateTime> queue = new Queue<DateTime>();

        private readonly int _maxRequests = 1;
        private readonly int _windowSize = 2;
        private readonly InMemoryStorage _storage;

        private readonly object _lock = new object();
        public SlidingWindowRateLimiter(InMemoryStorage storage)
        {
            _storage = storage;
        }
        public bool AllowRequest(string userId)
        {
            lock (_lock)
            {
                if (!_storage.Requests.ContainsKey(userId))
                {
                    _storage.Requests[userId] = new List<DateTime>(); // create list
                }
                var now = DateTime.UtcNow;
                var threshold = now.AddSeconds(-_windowSize);

                // remove old requests
                while (queue.Count > 0 && queue.Peek() < threshold)
                {
                    queue.Dequeue();
                }

                // check limit
                if (queue.Count >= _maxRequests)
                    return false;

                // allow request
                _storage.Save(userId, DateTime.UtcNow);
                queue.Enqueue(now);
                return true;
            }
        }
    }
}
