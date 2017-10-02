using System.Collections.Generic;
using AutoMapper;
using Portal.Core.Cache.Book;
using Portal.Core.Content.Entities;
using Portal.MapperConfig.Converters;
using Portal.MapperConfig.Resolvers;

namespace Portal.MapperConfig.Profiles
{
    public class BookCacheProfile : Profile
    {
        public BookCacheProfile()
        {
            CreateMap<Author, EntityItem>().ConvertUsing<EntityToEntityItemConverter<Book>>();

            CreateMap<Tag, EntityItem>().ConvertUsing<EntityToEntityItemConverter<Book>>();

            CreateMap<BookCatalog, IList<EntityItem>>()
                .ConvertUsing<CatalogToEntityItemListConverter<Book, BookCatalog>>();

            CreateMap<Book, BookCacheItem>()
                .ForMember(x => x.Catalogs, opt => opt.MapFrom(input => input.Catalog))
                .ForMember(x => x.FileFormat, opt => opt.ResolveUsing<FileExtensionResolver>());
        }
    }
}
