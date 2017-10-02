using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;

namespace Portal.Core.Cache.Author
{
    public class AuthorCache : EntityCache<Content.Entities.Author, AuthorCacheItem>, IAuthorCache, ITransientDependency
    {
        public AuthorCache(ICacheManager cacheManager, IRepository<Content.Entities.Author> repository) 
            : base(cacheManager, repository)
        {
        }
    }
}
