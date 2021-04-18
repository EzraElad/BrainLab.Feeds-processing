using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.IO.WrapperIO
{
    public interface IWrapperIO
    {
        void CreateJson();

        void CreateSummaryJson();
    }
}
