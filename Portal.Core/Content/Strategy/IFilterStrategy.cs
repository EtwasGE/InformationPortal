using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy
{
    public interface IFilterStrategy<TContent>
        where TContent : ContentEntityBase
    {
        IQueryable<TContent> Filter(IQueryable<TContent> source);
    }
}
