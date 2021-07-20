using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DataAccessLayer;

namespace BussinessLayer
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IMapper _mapper;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository, IMapper mapper)
        {
            _weatherForecastRepository = weatherForecastRepository;
            _mapper = mapper;
        }

        public Guid AddWeatherForecast(WeatherForecast weatherForecast)
        {
            if (weatherForecast.TemperatureC < -50)
            {
                throw new ArgumentException("Temperature is too low!");
            }

            weatherForecast.Id = Guid.NewGuid();
            var dto = _mapper.Map<WeatherForecastDTO>(weatherForecast);

            return _weatherForecastRepository.AddWeatherForecast(dto);
        }

        public WeatherForecast GetById(Guid id)
        {
            var dto = _weatherForecastRepository.GetById(id);

            return _mapper.Map<WeatherForecast>(dto);
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            var dtos = _weatherForecastRepository.GetAll();

            return dtos.Select(dto => _mapper.Map<WeatherForecast>(dto));
        }

        public WeatherForecast Update(WeatherForecast weatherForecast)
        {
            var dto = _weatherForecastRepository.Update(_mapper.Map<WeatherForecastDTO>(weatherForecast));

            return _mapper.Map<WeatherForecast>(dto);
        }

        public bool Delete(Guid id)
        {
            return _weatherForecastRepository.Delete(id);
        }
    }
}
