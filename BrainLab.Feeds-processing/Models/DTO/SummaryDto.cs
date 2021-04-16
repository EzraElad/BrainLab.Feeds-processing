using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.DTO
{
    public class SummaryDto
    {
        public string Source { get; set; }
        public string Id { get; set; }
        public int NumberOfWords { get; set; }
    }
}
