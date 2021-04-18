using BrainLab.Feeds_processing.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Twitter
{
    public class User : UserBase
    {
        [JsonPropertyName("created_at")]
        public string Created_At { get; set; }
    }
}
