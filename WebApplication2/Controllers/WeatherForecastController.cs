using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BussinessLayer;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpPost]
        public Guid PostSomething(WeatherForecast weatherForecast)
        {
           return _weatherForecastService.AddWeatherForecast(weatherForecast);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.GetAll();
        }

        [HttpGet("{id}")]
        public WeatherForecast GetById(Guid id)
        {
            return _weatherForecastService.GetById(id);
        }

        [HttpPut]
        public WeatherForecast Update(WeatherForecast objToUpdate)
        {
            return _weatherForecastService.Update(objToUpdate);
        }

        [HttpDelete("{id}")]
        public bool Remove(Guid id)
        {
            return _weatherForecastService.Delete(id);
        }
    }
}
