using System.Linq;
using Portal.Core.Content.Entities.Common;
using Portal.Core.Specifications;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectByNotApproved<TContent> : ISelectStrategy<TContent>
        where TContent : ContentEntityBase
    {
        public IQueryable<TContent> Select(IQueryable<TContent> source)
        {
            var specif = new NotApprovedAndNotDeletedSpecif<TContent>();
            return source.Where(specif);
        }
    }
}
