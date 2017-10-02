using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework;

namespace Portal.Data.Repositories
{
    public class Repository<TEntity> : RepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public Repository(IDbContextProvider<PortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
