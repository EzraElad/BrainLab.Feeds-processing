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
        private readonly RequestModel _requestModel;
        private FacebookModel facebookModel;

        public FacebookHandler(IMapper mapper, RequestModel requestModel)
        {
            _mapper = mapper;
            _requestModel = requestModel;
            facebookModel = _mapper.Map<FacebookModel>(_requestModel);
        }

        public List<string> CreateListString()
        {
            List<string> stringList = new List<string>();
            foreach (var item in facebookModel.Posts)
            {
                stringList.Add(item.Content);
            }
            return stringList;
        }

        public string ToJson()
        {
            string deliveredJson = JsonSerializer.Serialize(facebookModel);
            return deliveredJson;
        }
    }
}
