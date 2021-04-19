using AutoMapper;
using BrainLab.Feeds_processing.Factory;
using BrainLab.Feeds_processing.Helpers.Config;
using BrainLab.Feeds_processing.Helpers.IO;
using BrainLab.Feeds_processing.Helpers.IO.WrapperIO;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.DTO;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services
{
    public class NotificationProcessService : INotificationProcessService<RequestModel>
    {
        private readonly IConfiguration _config;
        private readonly IHelperIO _helperIO;
        private readonly IMapper _mapper;
        private readonly ConfigProvider _configProvider;

        public NotificationProcessService(IConfiguration config, IHelperIO helperIO, IMapper mapper, ConfigProvider configProvider)
        {
            _config = config;
            _helperIO = helperIO;
            _mapper = mapper;
            _configProvider = configProvider;
        }
        public async Task<ServiceResponse<string>> Handle(RequestModel requestModel, string path)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            NotificationFactory notificationFactory = new NotificationFactory(_mapper);
            var notificationHandler = notificationFactory.CreateNotificationHandler(requestModel);
            string deliveredJson = notificationHandler.ToJson();
            List<string> stringListToCount = notificationHandler.CreateListString(); 

            if (path is null)
            {
                path = $"{_configProvider.DefaultDir}/{requestModel.Source.ToLower()}";
            }
            WrapperIO wrapperService = 
                new WrapperIO(path, deliveredJson, stringListToCount, requestModel.Source.ToLower(), _helperIO, requestModel.Id);

            // service.CreateJson
            wrapperService.CreateJson();

            // service.CreateSummaryJson
            await wrapperService.CreateSummaryJson();

            response.Data = path;
            return response;
        }
    }
}
