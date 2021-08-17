using AutoMapper;
using DataAccessLayer.Models;

namespace BusinessLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.Id))
                .ForMember(x => x.BirthDate, options => options.MapFrom(src => src.BirthDate))
                .ForMember(x => x.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(x => x.Password, options => options.Ignore())
                .ForMember(x => x.Email, options => options.Ignore());
        }
    }
}
