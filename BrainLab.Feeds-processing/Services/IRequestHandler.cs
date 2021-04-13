using BrainLab.Feeds_processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services
{
    public interface IRequestHandler<T>
    {
        string Handle(T requestModel);

        string ToObject(T requestModel);
    }
}
