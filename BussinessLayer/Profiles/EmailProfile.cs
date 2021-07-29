using AutoMapper;
using BussinessLayer.Models;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Profiles
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<ConfirmationMessageModel, EmailDTO>()
               .ForMember(x => x.UserId, options => options.MapFrom(src => src.UserId))
               .ForMember(x => x.ConfirmationMessage, options => options.MapFrom(src => src.ConfirmationMessage))
               .ForMember(x => x.IsConfirmed, options => options.MapFrom(src => true))
               .ForMember(x => x.Email, options => options.Ignore());
        }
    }
}
