using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Filter
{
    public class LimitedResult<TContent> : IFilterStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly int _pageSize;

        public LimitedResult(int pageSize)
        {
            _pageSize = pageSize;
        }

        public IQueryable<TContent> Filter(IQueryable<TContent> source)
        {
            return source.Take(_pageSize);
        }
    }
}
