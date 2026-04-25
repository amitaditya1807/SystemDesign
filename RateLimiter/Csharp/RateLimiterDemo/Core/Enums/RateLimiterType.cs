using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiterDemo.Core.Enums
{
    public enum RateLimiterType
    {
        FixedWindow,
        SlidingWindow,
        TokenBucket,
        LeakyBucket
    }
}
