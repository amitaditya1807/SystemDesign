using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.Storage;

namespace RateLimiterDemo.RateLimiter
{
    public class SlidingWindowRateLimiter : IRateLimiter
    {
        private readonly InMemoryStorage _storage;
        public SlidingWindowRateLimiter(InMemoryStorage storage)
        {
            _storage = storage;
        }
        public bool AllowRequest(string userId)
        {
            return true;
        }
    }
}
