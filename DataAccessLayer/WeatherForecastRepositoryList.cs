using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class WeatherForecastRepositoryList : IWeatherForecastRepository
    {
        private static readonly List<WeatherForecastDTO> _weatherForecastDTOS;

        static WeatherForecastRepositoryList()
        {
            _weatherForecastDTOS = new List<WeatherForecastDTO>();
        }

        public Guid AddWeatherForecast(WeatherForecastDTO weatherForecast)
        {
            weatherForecast.Id = Guid.NewGuid();
            _weatherForecastDTOS.Add(weatherForecast);

            return weatherForecast.Id;
        }

        public WeatherForecastDTO GetById(Guid id)
        {
            return _weatherForecastDTOS.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<WeatherForecastDTO> GetAll()
        {
            return _weatherForecastDTOS;
        }

        public WeatherForecastDTO Update(WeatherForecastDTO objToUpdate)
        {
            var weatherForecast = _weatherForecastDTOS.Find(x => x.Id == objToUpdate.Id);
            _weatherForecastDTOS.Remove(weatherForecast);
            _weatherForecastDTOS.Add(objToUpdate);

            return objToUpdate;
        }

        public bool Delete(Guid id)
        {
            var weatherForecast = _weatherForecastDTOS.FirstOrDefault(x => x.Id == id);
            var result = false;
            if (weatherForecast != null)
            {
                _weatherForecastDTOS.Remove(weatherForecast);
                result = true;
            }

            return result;
        }
    }
}
