using AutoMapper;
using Portal.Application.Catalogs.Dto;
using Portal.Core.Content.Entities;

namespace Portal.MapperConfig.Profiles
{
    public class CatalogDtoProfile : Profile
    {
        public CatalogDtoProfile()
        {
            CreateMap<BookCatalog, CatalogDto>()
                .ForMember(x => x.IsActive,
                    opt => opt.ResolveUsing(input => MapperHelper.GetContentsCount<Book, BookCatalog>(input) > 0));

            CreateMap<TrainingCatalog, CatalogDto>()
                .ForMember(x => x.IsActive,
                    opt => opt.ResolveUsing(input => MapperHelper.GetContentsCount<Training, TrainingCatalog>(input) > 0));
        }
    }
}
