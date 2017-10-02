using System.Linq;
using Portal.Core.Content.Entities;

namespace Portal.Core.Content.Strategy.Sort
{
    public class SortBookByAuthor : ISortStrategy<Book>
    {
        private readonly bool _isDescending;

        public SortBookByAuthor(bool isDescending)
        {
            _isDescending = isDescending;
        }

        // TODO: change
        public IOrderedQueryable<Book> Sort(IQueryable<Book> source)
        {
            return (_isDescending
                    ? source.OrderByDescending(x => x.Authors.Any() ? x.Authors.FirstOrDefault().Name : "яяя")
                    : source.OrderBy(x => x.Authors.Any() ? x.Authors.FirstOrDefault().Name : "яяя"))
                .ThenByDescending(x => x.IsRecommend)
                .ThenBy(x => x.Title);
        }
    }
}
