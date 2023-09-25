using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetMeetApp.Common.Interfaces;
using PetMeetApp.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IEmailService _emailService;


        public WeatherForecastController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var a = 1 - 1;
            return Ok(8 / a);
        }
    }
}
