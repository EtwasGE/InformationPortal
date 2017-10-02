using System.Linq;
using Portal.Core.Content.Entities.Common;
using Portal.Core.Specifications;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectByDeleted<TContent> : ISelectStrategy<TContent>
        where TContent : ContentEntityBase
    {
        public IQueryable<TContent> Select(IQueryable<TContent> source)
        {
            var specif = new DeletedSpecif<TContent>();
            return source.Where(specif);
        }
    }
}
