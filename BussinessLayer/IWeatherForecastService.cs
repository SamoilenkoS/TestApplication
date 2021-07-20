using System;
using System.Collections.Generic;

namespace BussinessLayer
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
