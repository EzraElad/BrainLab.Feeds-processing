using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Facebook
{
    public class FacebookModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Source { get; set; }

        [JsonPropertyName("careated_at")]
        public string CreatedAt { get; set; }
        public DateTime RecivedAt { get; set; } = DateTime.Now;
        public List<Post> Posts { get; set; }
    }
}
