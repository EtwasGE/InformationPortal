using System;
using AutoMapper;
using Portal.Core.Authorization.Users;
using Portal.Core.Content.Entities;
using Portal.MapperConfig.Converters;

namespace Portal.MapperConfig.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Issue, string>().ConvertUsing<EntityToStringConverter>();
            CreateMap<Publisher, string>().ConvertUsing<EntityToStringConverter>();
            CreateMap<Language, string>().ConvertUsing<EntityToStringConverter>();
            CreateMap<Author, string>().ConvertUsing<EntityToStringConverter>();
            CreateMap<Tag, string>().ConvertUsing<EntityToStringConverter>();
            CreateMap<User, string>().ConvertUsing<UserToStringConverter>();
            CreateMap<DateTime?, string>().ConvertUsing<DateTimeToStringConverter>();
            CreateMap<byte[], string>().ConvertUsing<ImageToBase64StringConverter>();
        }
    }
}
