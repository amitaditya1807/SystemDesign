using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiterDemo.Core.Enums;
using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.RateLimiter;
using RateLimiterDemo.Storage;

namespace RateLimiterDemo.Factory
{
    public class RateLimiterFactory
    {
        public static IRateLimiter Create(RateLimiterType type, InMemoryStorage storage)
        {
            switch (type)
            {
                case RateLimiterType.FixedWindow:
                    return new FixedWindowRateLimiter(storage);

                case RateLimiterType.SlidingWindow:
                    return new SlidingWindowRateLimiter(storage);

                case RateLimiterType.TokenBucket:
                    return new TokenBucketRateLimiter(storage);

                case RateLimiterType.LeakyBucket:
                    return new LeakyBucketRateLimiter(storage);

                default:
                    throw new ArgumentException("Invalid Type");
            }
        }
    }
}
