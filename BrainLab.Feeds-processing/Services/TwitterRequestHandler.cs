using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services
{
    public class TwitterRequestHandler : IRequestHandler<TwitterModel>
    {
        public string Handle(TwitterModel requestModel, string path)
        {
            throw new NotImplementedException();
        }

        public string ToObject(TwitterModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
