using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models
{
    public class RequestModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Source { get; set; }

        [JsonPropertyName("careated_at")]
        public string CreatedAt { get; set; }
        public DateTime RecivedAt { get; set; } = DateTime.Now;
        // Add here your new formats 
        public List<Post> Posts { get; set; }
        public List<Tweet> Tweets { get; set; }
    }
}
