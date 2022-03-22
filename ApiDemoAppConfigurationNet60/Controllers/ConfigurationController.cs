using ApiDemoAppConfigurationCommon.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiDemoAppConfigurationNet60.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private Config _configuration;

        public ConfigurationController(IOptionsMonitor<Config> options)
        {

            _configuration = options.CurrentValue;

            options.OnChange(config =>
            {
                _configuration = config;
            });
        }

        [HttpGet(Name = "All")]
        public Config Get()
        {
            return _configuration;
        }
    }
}
