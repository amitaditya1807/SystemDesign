using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiterDemo.Storage
{
    public class InMemoryStorage
    {
        private static InMemoryStorage _instance;
        private static readonly object _lock = new();

        public Dictionary<string, List<DateTime>> Requests {  get; private set; }

        private InMemoryStorage() 
        {
            Requests = new Dictionary<string, List<DateTime>>();
        }

        public static InMemoryStorage GetInstance()
        {
            if(_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new InMemoryStorage();
                    }
                }
            }

            return _instance;
        }

        public void Save(string userId, DateTime time)
        {
            _instance.Requests[userId].Add(time);
        }
    }
}
