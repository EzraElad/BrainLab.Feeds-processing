using AutoMapper;
using BrainLab.Feeds_processing.Factory;
using BrainLab.Feeds_processing.Helpers.Config;
using BrainLab.Feeds_processing.Helpers.IO;
using BrainLab.Feeds_processing.Helpers.IO.WrapperIO;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.DTO;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using BrainLab.Feeds_processing.Services.LoggerService;
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
        private readonly LoggerServiceProvider _loggerService;

        public NotificationProcessService(IConfiguration config, IHelperIO helperIO
                                            ,IMapper mapper,ConfigProvider configProvider,LoggerServiceProvider loggerService)
        {
            _config = config;
            _helperIO = helperIO;
            _mapper = mapper;
            _configProvider = configProvider;
            _loggerService = loggerService;
        }
        public async Task<ServiceResponse<string>> Handle(RequestModel requestModel, string path)
        {
            _loggerService.Log("Start processing and inisialize classes");
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
                new WrapperIO(path, deliveredJson, stringListToCount, requestModel.Source.ToLower(),
                                                _helperIO, requestModel.Id, _configProvider, _loggerService);
            _loggerService.Log("Start creating the first json");
            wrapperService.CreateJson();

            _loggerService.Log("Start creating the summary json");
            await wrapperService.CreateSummaryJson();

            response.Data = path;
            response.Message = "The process completed successfuly";
            return response;
        }
    }
}
