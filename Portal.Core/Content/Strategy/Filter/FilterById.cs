using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Filter
{
    public class FilterById<TContent> : IFilterStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly int _id;

        public FilterById(int id)
        {
            _id = id;
        }

        public IQueryable<TContent> Filter(IQueryable<TContent> source)
        {
            return source.Where(x => x.Id != _id);
        }
    }
}
