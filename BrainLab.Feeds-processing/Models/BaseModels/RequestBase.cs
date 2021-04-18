using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.BaseModels
{
    public class RequestBase
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Source { get; set; }

        [JsonPropertyName("careated_at")]
        public string CreatedAt { get; set; }
        public DateTime RecivedAt { get; set; } = DateTime.Now;
    }
}
