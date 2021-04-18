using BrainLab.Feeds_processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services
{
    public interface INotificationProcessService<T>
    {
        ServiceResponse<string> Handle(T requestModel, string path);

        string ToJson(T requestModel);
    }
}
