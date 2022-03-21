using ApiDemoAppConfigurationNetCore31.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiDemoAppConfigurationNetCore31.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IOptionsSnapshot<Config> _configuration; // Important

        public ConfigurationController(IOptionsSnapshot<Config> configuration)
        {
            _configuration = configuration; // Important
        }

        [HttpGet(Name = "All")]
        public Config Get()
        {
            return _configuration.Value; // Important
        }
    }
}
