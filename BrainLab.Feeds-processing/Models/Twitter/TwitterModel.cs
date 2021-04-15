using BrainLab.Feeds_processing.Models.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Twitter
{
    public class TwitterModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Source { get; set; }
        public string CreatedAt { get; set; }
        public DateTime RecivedAt { get; set; } = DateTime.Now;
        public List<Tweet> Tweets { get; set; }
    }
}
