using AutoMapper;
using BrainLab.Feeds_processing.Helpers.Generic;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using BrainLab.Feeds_processing.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedsController : ControllerBase
    {
        private readonly IRequestHandler<FacebookModel> _facebookHandler;
        private readonly IRequestHandler<TwitterModel> _twitterHandler;
        private readonly IMapper _mapper;
        private readonly IRequestHandler<RequestModel> _requestHandler;
        public Dictionary<string, dynamic> handlerDick = new Dictionary<string, dynamic>();

        public GenericDictionary genericDic = new GenericDictionary();
        public static ConcurrentDictionary<string, DateTime> CD = new ConcurrentDictionary<string, DateTime>();
        
        public FeedsController(IRequestHandler<FacebookModel> facebookHandler, IRequestHandler<TwitterModel> twitterHandler, IMapper mapper)
        {
            _facebookHandler = facebookHandler;
            _twitterHandler = twitterHandler;
            _mapper = mapper;
            handlerDick.Add("facebook", _facebookHandler);
            handlerDick.Add("twitter", _twitterHandler);
            //genericDic.Add<IRequestHandler<FacebookModel>>("facebook", _facebookHandler);
            //genericDic.Add<IRequestHandler<TwitterModel>>("facebook", _twitterHandler);
        }

        //public FeedsController(IRequestHandler<RequestModel> requestHandler)
        //{
        //    _requestHandler = requestHandler;
        //}
        /* 
            Create a reflaction:
            handlers = {
                "facebook" : new FacebookRequestHandler(),
                "twitter" : new TwitterRequestHandler()
            }
         */

        [HttpPost]
        public IActionResult PostNotification(RequestModel request, [FromQuery] string configDirectory )
        //public IActionResult PostNotification([FromBody] string requestModel)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            // check the config directory with env veriable

            // Parses the json 
            // string request = JSON.Parse(jsonRequest);
            // string handlers[request.source](jsonRequest)

            string source = request.Source.ToLower();
            if (CD.TryGetValue(request.Id, out DateTime value))
            {
                // checks the value if there is less than a minute return if there is more
                TimeSpan span = request.RecivedAt.Subtract(value);
                var seconds = span.TotalSeconds;
                if(seconds < 10)
                {
                    return BadRequest("Wont process two exact request in the same minute.");
                }
                // delete the old value and enter a new one
                CD.TryRemove(request.Id, out DateTime dt);
                Console.WriteLine("wow");
            }
            CD.TryAdd(request.Id, request.RecivedAt);

            // map the object in each time and than create a dictionary
            if (source == "facebook")
            {
                FacebookModel fbModel = _mapper.Map<FacebookModel>(request);
                _facebookHandler.Handle(fbModel, configDirectory);
            }
            else if(source == "twitter")
            {
                TwitterModel twitterModel = _mapper.Map<TwitterModel>(request);
                _twitterHandler.Handle(twitterModel, configDirectory);
            }

            

            //return Ok(_requestHandler.Handle(request, configDirectory));
            return Ok();
        }

        [HttpPost("CountWords")]
        public int CountWords(List<string> strList)
        {
            int sum = 0;

            foreach (var str in strList)
            {
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }
                if(str.Length == 1)
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
