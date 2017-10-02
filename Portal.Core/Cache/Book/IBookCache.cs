using Abp.Domain.Entities.Caching;

namespace Portal.Core.Cache.Book
{
    public interface IBookCache : IEntityCache<BookCacheItem>
    {
    }
}
