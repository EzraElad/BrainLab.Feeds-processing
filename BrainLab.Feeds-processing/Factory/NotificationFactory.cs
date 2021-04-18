using AutoMapper;
using BrainLab.Feeds_processing.Handlers;
using BrainLab.Feeds_processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Factory
{
    public class NotificationFactory
    {
        private readonly IMapper _mapper;

        public NotificationFactory(IMapper mapper)
        {
            _mapper = mapper;
        }
        public INotificationHandler CreateNotificationHandler(RequestModel request)
        {
            string source = request.Source.ToLower();
            if(source == "facebook")
            {
                return new FacebookHandler(_mapper, request);
            }
            else
            {
                return new TwitterHandler(_mapper, request);
            }
        }
    }
}
