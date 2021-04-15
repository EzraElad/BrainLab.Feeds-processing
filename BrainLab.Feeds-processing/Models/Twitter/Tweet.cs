using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Twitter
{
    public class Tweet
    {
        [JsonPropertyName("created_at")]
        public string Created_At { get; set; }
        public long Id { get; set; }
        public string text { get; set; }
        public User User { get; set; }
    }
}
