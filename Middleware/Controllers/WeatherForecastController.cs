using Microsoft.AspNetCore.Mvc;
using Middleware.Configuration.Filters;
using Middleware.Factory;
using Middleware.Interfaces;
using Middleware.Services;

namespace Middleware.Controllers
{
    [ApiController]
    [Route("[controller]")]

    [MyActionFilterAttribute("WeatherForecastController")]

    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEnumerable<IMailService> _mailService;
        private readonly MailFactory _mailFactory;

        /// <summary>
        /// This parameter allows the injection of a collection of services that implement the IMailService interface.
        /// The IEnumerable enables the controller to work with multiple mail service implementations if needed.
        /// </summary>
        /// <param name="mailFactory"></param>
        /// <param name="logger"></param>
        //public WeatherForecastController(IEnumerable<IMailService> mailService, ILogger<WeatherForecastController> logger)
        //{
        //    _mailService = mailService;
        //    _logger = logger;
        //}

        public WeatherForecastController(MailFactory mailFactory, ILogger<WeatherForecastController> logger)
        {
            _mailFactory = mailFactory;
            _logger = logger;
        }

        [HttpGet("[Action]")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Get ALL weather InFo");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("[Action]/{id}")]
        [MyActionFilterAttribute("GetById")]
        [MyAsyncActionFilterAttribute("GetByIdAsync", -1)]
        public IEnumerable<WeatherForecast> Get([FromRoute] int id)
        {
            /// to find specific service instance using iteration

            //foreach (var item in _mailService)
            //{
            //    if (item.GetType() == typeof(CloudMailService))
            //    {
            //        item.Send();
            //    }
            //}

            Console.WriteLine("The Get() action method is being executed.");

            _mailFactory.GetMailService("localMail").Send();

            return Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("[Action]")]
        [MyAsyncActionFilterAttribute("SearchInFo", -1)]
        public WeatherForecast SearchInFo([FromQuery] string keyWord)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(0),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}