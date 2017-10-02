using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Core.Content.Entities;

namespace Portal.Core.Cache.Catalog
{
    [AutoMapFrom(
        typeof(BookCatalog), 
        typeof(TrainingCatalog)
        )]
    public class CatalogCacheItem : EntityDto
    {
        public string Name { get; set; }
    }
}
