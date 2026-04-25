using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimiterDemo.Core.Interfaces;
using RateLimiterDemo.Storage;
using static System.Formats.Asn1.AsnWriter;

namespace RateLimiterDemo.RateLimiter
{
    public class FixedWindowRateLimiter : IRateLimiter
    {
        private int _counter = 0;
        private long _currentWindow = -1;

        private readonly int _maxRequests = 5;
        private readonly int _windowSize = 10;
        private readonly long _startTime;
        private readonly InMemoryStorage _storage;

        private readonly object _lock = new object();
        public FixedWindowRateLimiter(InMemoryStorage storage) 
        {
            _storage = storage;
            _startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        public bool AllowRequest(string userId)
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long window = (currentTime - _startTime) / _windowSize + 1;

            lock (_lock)
            {
                if (!_storage.Requests.ContainsKey(userId))
                {
                    _storage.Requests[userId] = new List<DateTime>(); // create list
                }
                //Console.WriteLine("userid " + userId);
                //Console.WriteLine("current window " + _currentWindow.ToString());
                Console.WriteLine("window " + window.ToString());
                // Same window
                if (_currentWindow == window)
                {
                    if (_counter >= _maxRequests)
                        return false;

                    _storage.Requests[userId].Add(DateTime.UtcNow);
                    _counter++;
                    return true;
                }

                // New window
                _storage.Requests[userId].Add(DateTime.UtcNow);
                _currentWindow = window;
                _counter = 1;
                return true;
            }
        }
    }
}
