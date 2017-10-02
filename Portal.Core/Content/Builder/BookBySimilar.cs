using Portal.Core.Cache.Book;
using Portal.Core.Content.Builder.Common;
using Portal.Core.Content.Entities;
using Portal.Core.Content.Strategy.Filter;
using Portal.Core.Content.Strategy.Select;
using Portal.Core.Content.Strategy.Sort;

namespace Portal.Core.Content.Builder
{
    public class BookBySimilar : BuilderBase<Book>
    {
        private readonly BookCacheItem _book;
        private readonly int _pageSize;

        public BookBySimilar(BookCacheItem book, int pageSize)
        {
            _book = book;
            _pageSize = pageSize;
        }

        public override void Select()
        {
            Context.AddSelectStrategy(new SelectByTitle<Book>(_book.Title));

            foreach (var author in _book.Authors)
            {
                Context.AddSelectStrategy(new SelectBookByAuthor(author.Id));
            }
        }

        public override void Filter()
        {
            Context.AddFilterStrategy(new FilterById<Book>(_book.Id));
            Context.AddFilterStrategy(new LimitedResult<Book>(_pageSize));
        }

        public override void Sorting()
        {
            Context.SetSortStrategy(new SortByTitle<Book>(false));
        }
    }
}
