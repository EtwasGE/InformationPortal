using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Portal.Core.Content.Entities;

namespace Portal.Application.Catalogs.Dto
{
    [AutoMapFrom(
        typeof(BookCatalog),
        typeof(TrainingCatalog)
    )]
    public class CatalogDto : EntityDto, IPassivable
    {
        public string Name { get; set; }
        public IList<CatalogDto> Childrens { get; set; }
        public bool IsActive { get; set; }
    }
}
