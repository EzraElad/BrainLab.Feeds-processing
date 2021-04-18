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
        private readonly RequestModel _requestModel;
        private TwitterModel twitterModel;

        public TwitterHandler(IMapper mapper, RequestModel requestModel)
        {
            _mapper = mapper;
            _requestModel = requestModel;
            twitterModel = _mapper.Map<TwitterModel>(_requestModel);
        }

        public List<string> CreateListString()
        {
            List<string> stringList = new List<string>();
            foreach (var item in twitterModel.Tweets)
            {
                stringList.Add(item.text);
            }
            return stringList;
        }

        public string ToJson()
        {
            //TwitterModel twitterModel = _mapper.Map<TwitterModel>(_requestModel);
            string deliveredJson = JsonSerializer.Serialize(twitterModel);
            return deliveredJson;
        }
    }
}
