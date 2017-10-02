using System.Linq;
using Portal.Core.Content.Entities;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectBookByAuthor : ISelectStrategy<Book>
    {
        private readonly int _authorId;

        public SelectBookByAuthor(int authorId)
        {
            _authorId = authorId;
        }

        public IQueryable<Book> Select(IQueryable<Book> source)
        {
            return source.Where(x => x.Authors.Any(y => y.Id == _authorId));
        }
    }
}
