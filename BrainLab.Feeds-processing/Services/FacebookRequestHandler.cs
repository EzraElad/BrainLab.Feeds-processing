using BrainLab.Feeds_processing.Helpers.IO;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.DTO;
using BrainLab.Feeds_processing.Models.Facebook;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services
{
    public class FacebookRequestHandler : IRequestHandler<FacebookModel>
    {
        private readonly IConfiguration _config;
        private readonly IHelperIO _helperIO;

        public FacebookRequestHandler(IConfiguration config , IHelperIO helperIO)
        {
            _config = config;
            _helperIO = helperIO;
        }
        public string Handle(FacebookModel requestModel, string path)
        {
            // Break up to couple parts
            // 1. Do the checks you need => nned to check for the minute check !
            // 2. make the notification.json
            // 3. make the summery.json


            if (path is null)
            {
                // default dir = C:\\Users\\Elad Ezra\\Desktop\\Tests\\BL
                path = String.Concat(_config.GetValue<string>("DefaultDirectory"),$"/{requestModel.Source.ToLower()}");
            }

            // Checks if there is a dir if not create
            bool isDirectory = _helperIO.IsDirectoryExists(path);
            if (!isDirectory)
            {
                _helperIO.CreateNewDirectory(path);
            }

            // Checks the highest number in the directory and return it
            int newDirectoryName = _helperIO.HighestNumberOfDirectories(path) + 1;

            // Create a new dir with the HighestNumber+1 
            _helperIO.CreateNewDirectory($"{path}/{newDirectoryName}");
            //create inside it the json
            Guid guid = Guid.NewGuid();
            string skinyGuid = Convert.ToBase64String(guid.ToByteArray());
            string json = JsonSerializer.Serialize(requestModel);
            _helperIO.CreateJson(path, json, newDirectoryName.ToString(), skinyGuid, true);


            // 3. Summery.json part
            // Write a function that sends the content to 
            // source , Id , number of words, newDirectoryName => Guid.summary.json
            int wordSum = SendFacebookContent(requestModel);
            SummaryDto summary = new SummaryDto()
            {
                Id = requestModel.Id,
                NumberOfWords = wordSum,
                Source = requestModel.Source
            };
            string summaryJson = JsonSerializer.Serialize(summary);
            _helperIO.CreateJson(path, summaryJson, newDirectoryName.ToString(), skinyGuid, false);



            return "";
        }

        public string ToObject(FacebookModel requestModel)
        {
            throw new NotImplementedException();
        }

        private int SendFacebookContent(FacebookModel model)
        {
            // Here we are going to mock an api request logic just to make it work...
            var posts = model.Posts;
            int sum = 0;
            foreach (var post in posts)
            {
                // Count word function
                sum += CountWords(post.Content);
            }

            return sum;
        }

        private int CountWords(string post)
        {
            return post.Split(' ').Length;
        }
    }
}
