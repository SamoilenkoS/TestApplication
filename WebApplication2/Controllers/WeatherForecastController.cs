using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BussinessLayer;
using Microsoft.AspNetCore.Authorization;

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

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public Guid PostSomething(WeatherForecast weatherForecast)
        {
           return _weatherForecastService.AddWeatherForecast(weatherForecast);
        }

        //[AllowAnonymous]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.GetAll();
        }

        //[AllowAnonymous]
        [HttpGet("{id}")]
        public WeatherForecast GetById(Guid id)
        {
            return _weatherForecastService.GetById(id);
        }

        //[Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "Forecaster")]
        [HttpPut]
        public WeatherForecast Update(WeatherForecast objToUpdate)
        {
            return _weatherForecastService.Update(objToUpdate);
        }

        //[Authorize(Roles = "Administrator,Forecaster")]
        [HttpDelete("{id}")]
        public bool Remove(Guid id)
        {
            return _weatherForecastService.Delete(id);
        }
    }
}
