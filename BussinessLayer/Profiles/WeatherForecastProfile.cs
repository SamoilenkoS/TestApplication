using AutoMapper;
using DataAccessLayer;

namespace BussinessLayer
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastDTO>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.Id))
                .ForMember(x => x.Date, options => options.MapFrom(src => src.Date))
                .ForMember(x => x.Summary, options => options.MapFrom(src => src.Summary))
                .ForMember(x => x.Temperature, options => options.MapFrom(src => src.TemperatureC));

            CreateMap<WeatherForecastDTO, WeatherForecast>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.Id))
                .ForMember(x => x.Date, options => options.MapFrom(src => src.Date))
                .ForMember(x => x.Summary, options => options.MapFrom(src => src.Summary))
                .ForMember(x => x.TemperatureC, options => options.MapFrom(src => src.Temperature))
                .ForMember(x => x.TemperatureF, options => options.MapFrom(src => src.Temperature * 2));
        }
    }
}
