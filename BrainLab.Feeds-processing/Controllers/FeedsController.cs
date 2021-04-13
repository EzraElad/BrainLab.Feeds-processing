using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using BrainLab.Feeds_processing.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IRequestHandler<RequestModel> _facebookHandler;
        private readonly IRequestHandler<RequestModel> _twitterHandler;
        public Dictionary<string, IRequestHandler<RequestModel>> handlerDick = new Dictionary<string, IRequestHandler<RequestModel>>();
        public FeedsController(IRequestHandler<RequestModel> facebookHandler, IRequestHandler<RequestModel> twitterHandler)
        {
            _facebookHandler = facebookHandler;
            _twitterHandler = twitterHandler;
            handlerDick.Add("facebook", _facebookHandler);
            handlerDick.Add("twitter", _twitterHandler);
        }
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
            // check the config directory with env veriable

            // Parses the json 
            // string request = JSON.Parse(jsonRequest);
            // string handlers[request.source](jsonRequest)

            string source = request.Source.ToLower();

            //handlerDick["facebook"];

            return Ok(handlerDick[source].Handle(request));
            _facebookHandler.Handle(request);

            return Ok();
        }
    }
}
