using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class WeatherForecastRepositoryDB : IWeatherForecastRepository
    {
        public Guid AddWeatherForecast(WeatherForecastDTO weatherForecast)
        {
            throw new NotImplementedException();
        }

        public WeatherForecastDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WeatherForecastDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public WeatherForecastDTO Update(WeatherForecastDTO weatherForecast)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
