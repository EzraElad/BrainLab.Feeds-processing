using BrainLab.Feeds_processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Handlers
{
    public interface INotificationHandler
    {
        string ToJson(RequestModel request);

        List<string> CreateListString(RequestModel request);
    }
}
