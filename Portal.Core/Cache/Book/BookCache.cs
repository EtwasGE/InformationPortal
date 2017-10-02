using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;

namespace Portal.Core.Cache.Book
{
    public class BookCache : EntityCache<Content.Entities.Book, BookCacheItem>, IBookCache, ITransientDependency
    {
        public BookCache(ICacheManager cacheManager, IRepository<Content.Entities.Book> repository)
            : base(cacheManager, repository)
        {
        }
    }
}
