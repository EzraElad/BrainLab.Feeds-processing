using AutoMapper;
using BrainLab.Feeds_processing.Models;
using System;
using BrainLab.Feeds_processing.Models.Facebook;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BrainLab.Feeds_processing.Handlers
{
    public class FacebookHandler : INotificationHandler
    {
        private readonly IMapper _mapper;

        public FacebookHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<string> CreateListString(RequestModel request)
        {
            throw new NotImplementedException();
        }

        public string ToJson(RequestModel request)
        {
            FacebookModel FacebookModel = _mapper.Map<FacebookModel>(request);
            string deliveredJson = JsonSerializer.Serialize(FacebookModel);
            return deliveredJson;
        }
    }
}
