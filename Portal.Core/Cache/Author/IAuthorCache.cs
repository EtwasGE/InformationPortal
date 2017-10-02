using Abp.Domain.Entities.Caching;

namespace Portal.Core.Cache.Author
{
    public interface IAuthorCache : IEntityCache<AuthorCacheItem>
    {
    }
}
