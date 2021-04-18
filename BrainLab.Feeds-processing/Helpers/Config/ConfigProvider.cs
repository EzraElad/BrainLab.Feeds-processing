using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.Config
{
    public class ConfigProvider
    {
        private readonly IConfiguration _config;
        public string DefaultDir { get; set; }
        public string CounterApiUrl { get; set; }

        public ConfigProvider(IConfiguration config)
        {
            _config = config;
            DefaultDir = _config.GetValue<string>("DefaultDirectory");
        }

    }
}
