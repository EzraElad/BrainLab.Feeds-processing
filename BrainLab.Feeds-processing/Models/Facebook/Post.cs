using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Models.Facebook
{
    public class Post
    {
        public string Create_Date { get; set; }
        public int Id { get; set; }
        public string  Subject { get; set; }
        public string Content { get; set; }
        public Creator Creator{ get; set; }
    }
}
