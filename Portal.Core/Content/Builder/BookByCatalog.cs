using Portal.Core.Content.Entities;
using Portal.Core.Content.Strategy.Select;

namespace Portal.Core.Content.Builder
{
    public class BookByCatalog : AllBook
    {
        private readonly int _catalogId;

        public BookByCatalog(int catalogId)
        {
            _catalogId = catalogId;
        }
        
        public override void Select()
        {
            Context.AddSelectStrategy(new SelectByCatalog<Book, BookCatalog>(_catalogId));
        }
    }
}
