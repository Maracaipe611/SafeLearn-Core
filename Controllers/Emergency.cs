using Microsoft.AspNetCore.Mvc;
using SafeLearn.Configurations;
using Twilio;
using Twilio.Rest.Api.V2010;
using Twilio.Rest.Api.V2010.Account;

namespace SafeLearn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Emergency : Controller
    {

        private readonly ILogger<Emergency> _logger;
        private readonly ITwilioConfiguration twilioConfiguration;

        public Emergency(ILogger<Emergency> logger, ITwilioConfiguration twilioConfiguration)
        {
            _logger = logger;
            this.twilioConfiguration = twilioConfiguration;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            TwilioClient.Init(twilioConfiguration.AccountSID, twilioConfiguration.Auth);
            var call = CallResource.Create(
            url: new Uri("http://demo.twilio.com/docs/voice.xml"),
            to: new Twilio.Types.PhoneNumber("+5562993842895"),
            from: new Twilio.Types.PhoneNumber(twilioConfiguration.Number)
        );
            return Ok(call);
        }
    }
}