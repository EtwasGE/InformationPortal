using System.Collections.Generic;
using System.Configuration;
using AutoMapper;
using MvcPaging;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;
using Portal.Core.Content.Entities;
using Portal.MapperConfig.Converters;

namespace Portal.MapperConfig.Profiles
{
    public class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            var authorsLimit = int.Parse(ConfigurationManager.AppSettings["AuthorsLimit"]);
            CreateMap<ICollection<Author>, string>()
                .ConvertUsing(new EntityCollectionToStringConverter<Author>(authorsLimit));

            CreateMap<IList<EntityItem>, string>()
                .ConvertUsing(new EntityItemListToStringConverter(authorsLimit));

            CreateMap<IPagedList<Book>, IPagedList<ShortBookDto>>()
                .ConvertUsing<EnumerableToPagedListConverter<Book, ShortBookDto>>();

            CreateMap<Book, ShortBookDto>()
                .ForMember(x => x.Authors, opt => opt.MapFrom(input => input.Authors));
        }
    }
}
