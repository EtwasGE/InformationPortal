using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Cache.Catalog
{
    public class CatalogCache<TCatalog> : EntityCache<TCatalog, CatalogCacheItem>, ICatalogCache, ITransientDependency
        where TCatalog : EntityBase
    {
        public CatalogCache(ICacheManager cacheManager, IRepository<TCatalog> repository)
            : base(cacheManager, repository)
        {
        }
    }
}
