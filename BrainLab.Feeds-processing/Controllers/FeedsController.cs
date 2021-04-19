using AutoMapper;
using BrainLab.Feeds_processing.Helpers.ServiceProtector;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using BrainLab.Feeds_processing.Services;
using BrainLab.Feeds_processing.Services.LoggerService;
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
    [Route("api/[controller]")]
    public class FeedsController : ControllerBase
    {
        private readonly INotificationProcessService<RequestModel> _requestHandler;
        private readonly ServiceProtector _serviceProtector;
        private readonly LoggerServiceProvider _loggerService;
        public static ConcurrentDictionary<string, DateTime> CD = new ConcurrentDictionary<string, DateTime>();
        
        public FeedsController(INotificationProcessService<RequestModel> requestHandler
                                , ServiceProtector serviceProtector, LoggerServiceProvider loggerService)
        {
            _requestHandler = requestHandler;
            _serviceProtector = serviceProtector;
            _loggerService = loggerService;
        }


        [HttpPost]
        public async Task<IActionResult> PostNotification(RequestModel request, [FromQuery] string configDirectory )
        {
            try
            {
                _loggerService.Log("Got the request");
                _loggerService.Log("Validate the model");
                if (!ModelState.IsValid)
                {
                    return BadRequest("Your request model is not valid");
                }
                _loggerService.Log("Model is valid");
                _loggerService.Log("Check for identicle request");
                _serviceProtector.ProtectionCheck(request);

                _loggerService.Log("There is no duplication, may start proccesing");
                return Ok(await _requestHandler.Handle(request, configDirectory));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
