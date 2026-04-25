using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.Storage;

namespace RateLimiterDemo.RateLimiter
{
    public class TokenBucketRateLimiter : IRateLimiter
    {
        private readonly int _capacity;
        private readonly int _refillRate;
        private readonly int _inc;

        private int _tokens;
        private readonly object _lock = new object();

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private readonly InMemoryStorage _storage;
        public TokenBucketRateLimiter(InMemoryStorage storage, int capacity, int refillRate)
        {
            _capacity = capacity;   
            _refillRate = refillRate;
            _storage = storage;
            _inc = 2;
            StartRefillThread();    
        }

        private void StartRefillThread()
        {
            Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    lock (_lock)
                    {
                        _tokens = Math.Min(_capacity, _tokens + _inc);
                    }

                    await Task.Delay(_refillRate);
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
                if (_tokens < 1)return false;

                _storage.Save(userId, DateTime.UtcNow);
                _tokens--;
                return true;
            }
        }
    }
}
