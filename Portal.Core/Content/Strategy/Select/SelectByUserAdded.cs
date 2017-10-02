using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectByUserAdded<TContent> : ISelectStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly long _userId;

        public SelectByUserAdded(long userId)
        {
            _userId = userId;
        }

        public IQueryable<TContent> Select(IQueryable<TContent> source)
        {
            return source.Where(x => x.CreatorUserId == _userId);
        }
    }
}
