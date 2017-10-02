using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy
{
    public interface ISortStrategy<TContent>
        where TContent : ContentEntityBase 
    {
        IOrderedQueryable<TContent> Sort(IQueryable<TContent> source);
    }
}