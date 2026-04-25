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
        public LeakyBucketRateLimiter(InMemoryStorage storage, int maxReq, int windowSize)
        {
            _storage = storage;
        }
        public bool AllowRequest(string userId)
        {
            return true;
        }
    }
}
