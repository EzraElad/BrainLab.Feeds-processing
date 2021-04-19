using AutoMapper;
using BrainLab.Feeds_processing.Helpers.ServiceProtector;
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
    [Route("api/[controller]")]
    public class FeedsController : ControllerBase
    {
        private readonly INotificationProcessService<RequestModel> _requestHandler;
        private readonly ServiceProtector _serviceProtector;
        public static ConcurrentDictionary<string, DateTime> CD = new ConcurrentDictionary<string, DateTime>();
        
        public FeedsController(INotificationProcessService<RequestModel> requestHandler, ServiceProtector serviceProtector)
        {
            _requestHandler = requestHandler;
            _serviceProtector = serviceProtector;
        }


        [HttpPost]
        public async Task<IActionResult> PostNotification(RequestModel request, [FromQuery] string configDirectory )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Your request model is not valid");
                }

                _serviceProtector.ProtectionCheck(request);

                return Ok(await _requestHandler.Handle(request, configDirectory));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
