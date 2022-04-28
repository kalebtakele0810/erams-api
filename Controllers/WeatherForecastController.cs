using ERAManagementSystem.Models;
using ERAManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERAManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IMailService _mailService;

        public WeatherForecastController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var mail = new MailRequest
            {
                To = "jayabaj799@yeafam.com",
                Subject = "Test Email",
                Body = "This is test email Ya Allah Make it work.",
                Attachments = null
            };

            try
            {
                await _mailService.SendEmailAsync(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}