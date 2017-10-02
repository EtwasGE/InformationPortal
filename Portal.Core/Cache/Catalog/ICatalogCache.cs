using Abp.Domain.Entities.Caching;

namespace Portal.Core.Cache.Catalog
{
    public interface ICatalogCache : IEntityCache<CatalogCacheItem>
    {
    }
}
