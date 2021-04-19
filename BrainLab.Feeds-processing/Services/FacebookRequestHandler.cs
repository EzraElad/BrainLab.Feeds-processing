//using BrainLab.Feeds_processing.Helpers.IO;
//using BrainLab.Feeds_processing.Models;
//using BrainLab.Feeds_processing.Models.DTO;
//using BrainLab.Feeds_processing.Models.Facebook;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace BrainLab.Feeds_processing.Services
//{
//    public class FacebookRequestHandler : INotificationProcessService<FacebookModel>
//    {
//        private readonly IConfiguration _config;
//        private readonly IHelperIO _helperIO;

//        public FacebookRequestHandler(IConfiguration config , IHelperIO helperIO)
//        {
//            _config = config;
//            _helperIO = helperIO;
//        }
//        public Task<ServiceResponse<string>> Handle(FacebookModel requestModel, string path)
//        {
//            ServiceResponse<string> response = new ServiceResponse<string>();
//            // Break up to couple parts
//            // 1. Do the checks you need => need to check for the minute check !
//            // 2. make the notification.json
//            // 3. make the summery.json

//            //int newDirectoryName = _helperIO.HighestNumberOfDirectories(path) + 1;
//            //Guid guid = Guid.NewGuid();
//            //string skinyGuid = Convert.ToBase64String(guid.ToByteArray());


//            if (path is null)
//            {
//                // default dir = C:\\Users\\Elad Ezra\\Desktop\\Tests\\BL
//                path = String.Concat(_config.GetValue<string>("DefaultDirectory"),$"/{requestModel.Source.ToLower()}");
//            }
//            // check if the path is good

//            // Checks if there is a dir && the path is valid - if both true create
//            bool isDirectory = _helperIO.IsDirectoryExists(path);
//            if (!isDirectory)
//            {
//                // if good creates the dir if not return response
//                bool isGoodDirectory = _helperIO.TryCreateNewDirectory(path);
//                if (!isGoodDirectory)
//                {
//                    response.Message = "There is something wrong with the directory";
//                    response.Success = false;
//                    return response;
//                }
//            }

//            // Checks the highest number in the directory and return it
//            int newDirectoryName = _helperIO.HighestNumberOfDirectories(path) + 1;

//            // Create a new dir with the HighestNumber+1 
//            bool newDirSuccess = _helperIO.TryCreateNewDirectory($"{path}/{newDirectoryName}");

//            //create inside it the json
//            Guid guid = Guid.NewGuid();
//            string skinyGuid = Convert.ToBase64String(guid.ToByteArray());
//            string json = JsonSerializer.Serialize(requestModel);
//            _helperIO.CreateJson(path, json, newDirectoryName.ToString(), skinyGuid, true);

//            //CreateNotificationJson(path, requestModel);

//            // 3. Summery.json part
//            // Write a function that sends the content to 
//            // source , Id , number of words, newDirectoryName => Guid.summary.json
//            int wordSum = SendFacebookContent(requestModel);
//            SummaryDto summary = new SummaryDto()
//            {
//                Id = requestModel.Id,
//                NumberOfWords = wordSum,
//                Source = requestModel.Source
//            };
//            string summaryJson = JsonSerializer.Serialize(summary);
//            _helperIO.CreateJson(path, summaryJson, newDirectoryName.ToString(), skinyGuid, false);


//            response.Data = path;
//            return response;
//        }

//        public string ToJson(FacebookModel requestModel)
//        {
//            throw new NotImplementedException();
//        }

//        private int SendFacebookContent(FacebookModel model)
//        {
//            List<string> strList = new List<string>();
//            // Here we are going to mock an api request logic just to make it work...
//            var posts = model.Posts;
//            int sum = 0;
//            // TODO: need to check a bunch of stuff!

//            foreach (var post in posts)
//            {
//                // Count word function
//                strList.Add(post.Content);
                

//                sum += CountWords(post.Content);
//            }

//            // than I send the post request with the strList 
//            // and the service returns my sum

//            return sum;
//        }

//        private int CountWords(string post)
//        {
            
//            return post.Split(' ').Length;
//        }
//    }
//}
