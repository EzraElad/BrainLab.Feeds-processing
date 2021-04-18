using BrainLab.Feeds_processing.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.ServiceProtector
{
    public class ServiceProtector
    {
        public ConcurrentDictionary<string, DateTime> CD = new ConcurrentDictionary<string, DateTime>();
        public void ProtectionCheck(RequestModel request)
        {
            if (CD.TryGetValue(request.Id, out DateTime value))
            {
                // checks the value if there is less than a minute return if there is more
                TimeSpan span = request.RecivedAt.Subtract(value);
                var seconds = span.TotalSeconds;
                if (seconds < 10)
                {
                    throw new Exception("Could not process identical request in the same minute");
                }
                // delete the old value and enter a new one
                CD.TryRemove(request.Id, out DateTime dt);
                Console.WriteLine("wow");
            }
            CD.TryAdd(request.Id, request.RecivedAt);
        }
    }
}
