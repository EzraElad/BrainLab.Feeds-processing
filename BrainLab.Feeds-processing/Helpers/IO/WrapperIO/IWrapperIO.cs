using BrainLab.Feeds_processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.IO.WrapperIO
{
    public interface IWrapperIO
    {
        ServiceResponse<string> CreateJson();

        Task CreateSummaryJson();
    }
}
