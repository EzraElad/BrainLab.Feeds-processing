using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BrainLab.WordProcess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainLab.WordProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly ILogger<CounterController> _logger;
        private readonly ICountService _countService;

        public CounterController(ILogger<CounterController> logger, ICountService countService)
        {
            _logger = logger;
            _countService = countService;
        }

        [HttpPost]
        public int WordCounter(IEnumerable<string> stringList)
        {
            return _countService.CountWords(stringList);
        }
    }
}
