using Abp.Domain.Entities.Caching;

namespace Portal.Core.Cache.Tag
{
    public interface ITagCache : IEntityCache<TagCacheItem>
    {
    }
}
