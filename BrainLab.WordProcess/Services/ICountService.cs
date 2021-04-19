using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.WordProcess.Services
{
    public interface ICountService
    {
        int CountWords(IEnumerable<string> stringList);
    }
}
