using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;

namespace Portal.Core.Cache.Tag
{
    public class TagCache : EntityCache<Content.Entities.Tag, TagCacheItem>, ITagCache, ITransientDependency
    {
        public TagCache(ICacheManager cacheManager, IRepository<Content.Entities.Tag> repository) 
            : base(cacheManager, repository)
        {
        }
    }
}
