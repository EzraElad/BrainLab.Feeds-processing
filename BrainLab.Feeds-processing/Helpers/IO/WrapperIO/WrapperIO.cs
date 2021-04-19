using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.IO.WrapperIO
{
    public class WrapperIO : IWrapperIO
    {
        private readonly string _path;
        private readonly string _json;
        private readonly List<string> _stringList;
        private readonly string _source;
        private readonly IHelperIO _helperIO;
        private readonly string _id;
        private string _skinyGuid;
        private int _newDirectoryName;
        private static readonly HttpClient client = new HttpClient();

        public WrapperIO(string path, string json, List<string> stringList,
                                    string source, IHelperIO helperIO, string id)
        {
            _path = path;
            _json = json;
            _stringList = stringList;
            _source = source;
            _helperIO = helperIO;
            _id = id;
            Guid guid = Guid.NewGuid();
            _skinyGuid = Convert.ToBase64String(guid.ToByteArray()).Replace("/", "");
            _newDirectoryName = _helperIO.HighestNumberOfDirectories(_path) + 1;
        }
        public ServiceResponse<string> CreateJson()
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            bool isDirectory = _helperIO.IsDirectoryExists(_path);
            if (!isDirectory)
            {
                bool isGoodDirectory = _helperIO.TryCreateNewDirectory(_path);
                if (!isGoodDirectory)
                {
                    response.Message = "There is something wrong with the directory";
                    response.Success = false;
                    return response;
                }
            }

            bool newDirSuccess = _helperIO.TryCreateNewDirectory($"{_path}/{_newDirectoryName}");

            _helperIO.CreateJson(_path, _json, _newDirectoryName.ToString(), _skinyGuid, true);

            response.Data = _path;
            response.Message = "Json created successfuly!";
            return response;
        }

        public async Task CreateSummaryJson()
        {
            var contentTest = JsonSerializer.Serialize(_stringList);
            var content = new StringContent(contentTest, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44364/api/Counter", content);
            var responseString = await response.Content.ReadAsStringAsync();

            int wordSum = Int32.Parse(responseString);
            SummaryDto summary = new SummaryDto()
            {
                Id = _id,
                NumberOfWords = wordSum,
                Source = _source
            };
            string summaryJson = JsonSerializer.Serialize(summary);
            _helperIO.CreateJson(_path, summaryJson, _newDirectoryName.ToString(), _skinyGuid, false);
        }
    }
}
