using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainLab.WordProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly ILogger<CounterController> _logger;

        public CounterController(ILogger<CounterController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public int WordCounter(IEnumerable<string> stringList)
        {
            int sum = 0;

            foreach (var str in stringList)
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    continue;
                }
                sum += str.Trim().Split(" ").Length;
            }

            return sum;
        }
    }
}
