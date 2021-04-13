using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models
{
    public class RequestModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Source { get; set; }
        public string CreatedAt { get; set; }
        public DateTime RecivedAt { get; set; } = DateTime.Now;
        // Add here your new formats 
        public object[] Posts { get; set; }
        public object[] Tweets { get; set; }
    }
}
