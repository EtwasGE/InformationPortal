using System.Collections.Generic;
using Abp.Application.Services;
using Portal.Application.Catalogs.Dto;

namespace Portal.Application.Catalogs
{
    public interface ICatalogAppService : IApplicationService
    {
        IList<CatalogDto> GetParentCatalogs();
    }
}