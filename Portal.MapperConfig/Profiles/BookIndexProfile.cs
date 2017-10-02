using System.Collections.Generic;
using AutoMapper;
using Portal.Core.Content.Entities;
using Portal.Core.ElasticSearch;
using Portal.Core.Specifications;
using Portal.MapperConfig.Converters;

namespace Portal.MapperConfig.Profiles
{
    public class BookIndexProfile : Profile
    {
        public BookIndexProfile()
        {
            CreateMap<ICollection<Author>, string>()
                .ConvertUsing(new EntityCollectionToStringConverter<Author>());

            CreateMap<Book, BookIndexItem>()
                .ForMember(x => x.IsActive,
                    opt => opt.ResolveUsing(
                        input => new ApprovedAndNotDeletedSpecif<Book>().IsSatisfiedBy(input)));
        }
    }
}
