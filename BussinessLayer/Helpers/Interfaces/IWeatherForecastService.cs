using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Helpers.Interfaces
{
    public interface IWeatherForecastService
    {
        Guid AddWeatherForecast(WeatherForecast weatherForecast);
        WeatherForecast GetById(Guid id);
        IEnumerable<WeatherForecast> GetAll();
        WeatherForecast Update(WeatherForecast weatherForecast);
        bool Delete(Guid id);
    }
}
