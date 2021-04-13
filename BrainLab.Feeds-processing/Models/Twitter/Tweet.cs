using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Twitter
{
    public class Tweet
    {
        public string Create_At { get; set; }
        public int Id { get; set; }
        public string text { get; set; }
        public User User { get; set; }
    }
}
