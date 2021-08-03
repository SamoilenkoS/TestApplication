using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class WeatherForecastRepositoryEFCore : IWeatherForecastRepository
    {
        private readonly EFCoreContext _dbContext;

        public WeatherForecastRepositoryEFCore(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddWeatherForecast(WeatherForecastDTO weatherForecast)
        {
            _dbContext.WeatherForecasts.Add(weatherForecast);
            _dbContext.SaveChanges();

            return weatherForecast.Id;
        }

        public bool Delete(Guid id)
        {
            var entity = _dbContext.WeatherForecasts.FirstOrDefault(x => x.Id == id);
            var entityExist = entity != null;
            if (entityExist)
            {
                _dbContext.WeatherForecasts.Remove(entity);
                _dbContext.SaveChanges();
            }

            return entityExist;
        }

        public IEnumerable<WeatherForecastDTO> GetAll()
        {
            return _dbContext.WeatherForecasts.ToList();
        }

        public WeatherForecastDTO GetById(Guid id)
        {
            return _dbContext.WeatherForecasts.FirstOrDefault(x => x.Id == id);
        }

        public WeatherForecastDTO Update(WeatherForecastDTO weatherForecast)
        {
            _dbContext.Update(weatherForecast);
            _dbContext.SaveChanges();

            return weatherForecast;
        }
    }
}
