using AutoMapper;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Handlers
{
    public class TwitterHandler : INotificationHandler
    {
        private readonly IMapper _mapper;

        public TwitterHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<string> CreateListString(RequestModel request)
        {
            throw new NotImplementedException();
        }

        public string ToJson(RequestModel request)
        {
            TwitterModel twitterModel = _mapper.Map<TwitterModel>(request);
            string deliveredJson = JsonSerializer.Serialize(twitterModel);
            return deliveredJson;
        }
    }
}
