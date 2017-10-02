using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Sort
{
    public class SortByFavourite<TContent> : ISortStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly bool _isDescending;

        public SortByFavourite(bool isDescending)
        {
            _isDescending = isDescending;
        }

        public IOrderedQueryable<TContent> Sort(IQueryable<TContent> source)
        {
            return (_isDescending
                    ? source.OrderByDescending(x => x.FavouriteUsers.Count)
                    : source.OrderBy(x => x.FavouriteUsers.Count))
                .ThenByDescending(x => x.IsRecommend)
                .ThenBy(x => x.Title);
        }
    }
}
