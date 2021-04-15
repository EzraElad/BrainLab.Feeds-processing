using BrainLab.Feeds_processing.Helpers.IO;
using BrainLab.Feeds_processing.Models;
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
            string json = JsonSerializer.Serialize(requestModel);
            _helperIO.CreateJson(path, json, newDirectoryName.ToString());

            return "";
        }

        public string ToObject(FacebookModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
