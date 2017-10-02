using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy
{
    public interface ISelectStrategy<TContent>
        where TContent : ContentEntityBase
    {
        IQueryable<TContent> Select(IQueryable<TContent> source);
    }
}