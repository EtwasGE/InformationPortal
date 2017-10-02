using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectByFavorite<TContent> : ISelectStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly long _userId;

        public SelectByFavorite(long userId)
        {
            _userId = userId;
        }

        public IQueryable<TContent> Select(IQueryable<TContent> source)
        {
            return source.Where(x => x.FavouriteUsers.Any(y => y.Id == _userId));
        }
    }
}
