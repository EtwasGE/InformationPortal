using System.Linq;
using Portal.Core.Content.Entities;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectBookByTag : ISelectStrategy<Book>
    {
        private readonly int _tagId;

        public SelectBookByTag(int tagId)
        {
            _tagId = tagId;
        }

        public IQueryable<Book> Select(IQueryable<Book> source)
        {
            return source.Where(x => x.Tags.Any(y => y.Id == _tagId));
        }
    }
}
