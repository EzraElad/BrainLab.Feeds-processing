using BrainLab.Feeds_processing.Models.BaseModels;
using BrainLab.Feeds_processing.Models.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Twitter
{
    public class TwitterModel : RequestBase
    {
        public List<Tweet> Tweets { get; set; }
    }
}
