using AutoMapper;
using DataAccessLayer;

namespace BussinessLayer
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(x => x.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(x => x.Temperature, opt => opt.MapFrom(src => src.TemperatureC));

            CreateMap<WeatherForecastDTO, WeatherForecast>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(x => x.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(x => x.TemperatureC, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(x => x.TemperatureF, opt => opt.MapFrom(src => src.Temperature * 2));
        }
    }
}
