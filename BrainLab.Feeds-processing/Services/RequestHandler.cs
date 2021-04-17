using BrainLab.Feeds_processing.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services
{
    public class RequestHandler : IRequestHandler<RequestModel>
    {
        private readonly IConfiguration _config;

        public RequestHandler(IConfiguration config)
        {
            _config = config;
        }
        public ServiceResponse<string> Handle(RequestModel requestModel, string path)
        {
            if(path is null)
            {
                path = _config.GetValue<string>("DefaultDirectory");
            }

            string source = requestModel.Source.ToLower();

            switch (source){
                case "facebook":
                    //DO work
                    break;
                case "twitter":
                    //do twit
                    break;
                default:
                    Console.WriteLine("Default case");
                    throw new KeyNotFoundException();
            }

            return new ServiceResponse<string>();
        }

        public string ToObject(RequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
