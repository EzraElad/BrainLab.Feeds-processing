using AutoMapper;
using BrainLab.Feeds_processing.Factory;
using BrainLab.Feeds_processing.Helpers.IO;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.DTO;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public NotificationProcessService(IConfiguration config, IHelperIO helperIO, IMapper mapper)
        {
            _config = config;
            _helperIO = helperIO;
            _mapper = mapper;
        }
        public ServiceResponse<string> Handle(RequestModel requestModel, string path)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            // Break up to couple parts
            // 1. make the notification.json
            // 2. make the summery.json

            // NotiFactory inisialize
            var test = new NotificationFactory(_mapper);
            var notificationHandler = test.CreateNotificationHandler(requestModel);
            notificationHandler.ToJson(requestModel);

            if (path is null)
            {
                // default dir = C:\\Users\\Elad Ezra\\Desktop\\Tests\\BL
                path = String.Concat(_config.GetValue<string>("DefaultDirectory"), $"/{requestModel.Source.ToLower()}");
            }
            // check if the path is good

            // Checks if there is a dir && the path is valid - if both true create
            bool isDirectory = _helperIO.IsDirectoryExists(path);
            if (!isDirectory)
            {
                // if good creates the dir if not return response
                bool isGoodDirectory = _helperIO.TryCreateNewDirectory(path);
                if (!isGoodDirectory)
                {
                    response.Message = "There is something wrong with the directory";
                    response.Success = false;
                    return response;
                }
            }

            // Checks the highest number in the directory and return it
            int newDirectoryName = _helperIO.HighestNumberOfDirectories(path) + 1;

            // Create a new dir with the HighestNumber+1 
            bool newDirSuccess = _helperIO.TryCreateNewDirectory($"{path}/{newDirectoryName}");

            //create inside it the json
            Guid guid = Guid.NewGuid();
            string skinyGuid = Convert.ToBase64String(guid.ToByteArray()).Replace("/", "");
            // Check the source and map to the correct model
            string deliveredJson = ToJson(requestModel);
            _helperIO.CreateJson(path, deliveredJson, newDirectoryName.ToString(), skinyGuid, true);

            //CreateNotificationJson(path, requestModel);

            // 3. Summery.json part
            // Write a function that sends the content to 
            // source , Id , number of words, newDirectoryName => Guid.summary.json
            int wordSum = 0;
            SummaryDto summary = new SummaryDto()
            {
                Id = requestModel.Id,
                NumberOfWords = wordSum,
                Source = requestModel.Source
            };
            string summaryJson = JsonSerializer.Serialize(summary);
            _helperIO.CreateJson(path, summaryJson, newDirectoryName.ToString(), skinyGuid, false);


            response.Data = path;
            return response;
        }

        public string ToJson(RequestModel requestModel)
        {
            string deliveredJson = String.Empty;
            switch (requestModel.Source.ToLower())
            {
                case "facebook":
                    FacebookModel FacebookModel = _mapper.Map<FacebookModel>(requestModel);
                    deliveredJson = JsonSerializer.Serialize(FacebookModel);
                    break;
                case "twitter":
                    TwitterModel TwitterModel = _mapper.Map<TwitterModel>(requestModel);
                    deliveredJson = JsonSerializer.Serialize(TwitterModel);
                    break;
                default:
                    throw new KeyNotFoundException();
            }
            return deliveredJson;
        }

        public int SendToCounter(RequestModel requestModel)
        {
            // Need to prepre List<string>
            List<string> stringList = new List<string>();
            switch (requestModel.Source.ToLower())
            {
                case "facebook":
                    foreach (var post in requestModel.Posts)
                    {
                        stringList.Add(post.Content);
                    }
                    break;
                case "twitter":
                    foreach (var tweet in requestModel.Tweets)
                    {
                        stringList.Add(tweet.text);
                    }
                    break;
                default:
                    throw new KeyNotFoundException();
            }
            // Post the request
            return CountWords(stringList);
        }

        public int CountWords(List<string> strList)
        {
            int sum = 0;

            foreach (var str in strList)
            {
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }
                if (str.Length == 1)
                {
                    sum += 1;
                    continue;
                }
                sum += str.Split(" ").Length;
            }

            return sum;
        }
    }
}
