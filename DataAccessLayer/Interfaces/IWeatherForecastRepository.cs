using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IWeatherForecastRepository
    {
        Guid AddWeatherForecast(WeatherForecastDTO weatherForecast);
        WeatherForecastDTO GetById(Guid id);
        IEnumerable<WeatherForecastDTO> GetAll();
        WeatherForecastDTO Update(WeatherForecastDTO weatherForecast);
        bool Delete(Guid id);
    }
}
