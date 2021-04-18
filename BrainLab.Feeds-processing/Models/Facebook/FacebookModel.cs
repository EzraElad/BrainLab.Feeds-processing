using BrainLab.Feeds_processing.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Facebook
{
    public class FacebookModel : RequestBase
    {
        public List<Post> Posts { get; set; }
    }
}
