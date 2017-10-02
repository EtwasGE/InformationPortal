using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Sort
{
    public class SortByTitle<TContent> : ISortStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly bool _isDescending;

        public SortByTitle(bool isDescending)
        {
            _isDescending = isDescending;
        }

        public IOrderedQueryable<TContent> Sort(IQueryable<TContent> source)
        {
            return (_isDescending
                    ? source.OrderByDescending(x => x.Title)
                    : source.OrderBy(x => x.Title))
                .ThenByDescending(x => x.IsRecommend);
        }
    }
}
