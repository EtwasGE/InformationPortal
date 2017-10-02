using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Sort
{
    public class SortByDate<TContent> : ISortStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly bool _isDescending;

        public SortByDate(bool isDescending)
        {
            _isDescending = isDescending;
        }

        public IOrderedQueryable<TContent> Sort(IQueryable<TContent> source)
        {
            return (_isDescending
                    ? source.OrderByDescending(x => x.CreationTime)
                    : source.OrderBy(x => x.CreationTime))
                .ThenByDescending(x => x.IsRecommend)
                .ThenBy(x => x.Title);
        }
    }
}
