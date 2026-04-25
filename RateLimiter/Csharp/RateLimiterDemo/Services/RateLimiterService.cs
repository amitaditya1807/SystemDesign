using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiterDemo.Core.Interfaces;

namespace RateLimiterDemo.Services
{
    public class RateLimiterService
    {
        private readonly IRateLimiter _ratelimiter;
        
        public RateLimiterService(IRateLimiter ratelimiter)
        {
            _ratelimiter = ratelimiter;
        }

        public bool HandleRequest(string userId)
        {
            return _ratelimiter.AllowRequest(userId);
        }
    }
}
