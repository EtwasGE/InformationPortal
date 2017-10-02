using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Portal.Data.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<PortalDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RepositoryBase(IDbContextProvider<PortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
