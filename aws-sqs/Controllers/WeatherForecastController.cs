using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace aws_sqs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post(WeatherForecast data)
        {
            var credentials = new BasicAWSCredentials("AKIA33V5NB3Q3KXH4NQJ", "Uk8UCakM6nTGZ2r3HsH8tcChX5HeD16o/8L83tcg");
            var client = new AmazonSQSClient(credentials, Amazon.RegionEndpoint.EUCentral1);

            var request = new SendMessageRequest()
            {
                QueueUrl = "https://sqs.eu-central-1.amazonaws.com/815367261921/mehmet-demo",
                MessageBody = JsonSerializer.Serialize(data)
                  
            };

             await client.SendMessageAsync(request);
        }
    }
}