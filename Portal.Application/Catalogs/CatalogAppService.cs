using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Portal.Application.Catalogs.Dto;
using Portal.Core.Content.Entities.Common;

namespace Portal.Application.Catalogs
{
    [AbpAuthorize]
    public class CatalogAppService<TContent, TCatalog> : AppServiceBase, ICatalogAppService
        where TContent : ContentEntityBase
        where TCatalog : CatalogBase<TContent, TCatalog>
    {
        private readonly IRepository<TCatalog> _repository;
        public CatalogAppService(IRepository<TCatalog> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить список родительских каталогов.
        /// </summary>
        public IList<CatalogDto> GetParentCatalogs()
        {
            var catalogs = BuildCatalogs();

            foreach (var catalog in catalogs)
            {
                catalog.Childrens = BuildCatalogs(catalog.Id).ToList();
            }

            return catalogs.MapTo<IList<CatalogDto>>();
        }

        #region Private Methods
        private IQueryable<TCatalog> BuildCatalogs(int? parentId = null)
        {
            var catalogs = _repository.GetAll();
            return catalogs
                .Where(x => x.ParentId == parentId)
                .OrderBy(x => x.Order);
        }
        #endregion
    }
}