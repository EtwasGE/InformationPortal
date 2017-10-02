using System.Linq;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Portal.Core.Content.Entities.Common;
using Portal.Core.Content.Strategy;

namespace Portal.Core.Content.Builder.Common
{
    public abstract class BuilderBase<TContent>
        where TContent : ContentEntityBase
    {
        protected BuilderBase()
        {
            var repository = IocManager.Instance.Resolve<IRepository<TContent>>();
            Context = new ContextStrategy<TContent>(repository.GetAll());
        }
        
        protected ContextStrategy<TContent> Context { get; }
        
        public virtual void Select()
        {
        }

        public virtual void Filter()
        {
        }
        
        public virtual void Sorting()
        {
        }

        public IQueryable<TContent> Construct()
        {
            Context.Select();
            Context.Filter();
            Context.Sorting();
            return Context.GetResult();
        } 
    }
}
