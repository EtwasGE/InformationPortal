using System.Collections.Generic;
using Portal.Core.Cache.Book;

namespace Portal.Application.Content.Books.Dto
{
    public class BookOutput
    {
        public BookCacheItem Book { get; set; }
        public IList<ShortBookDto> SimilarBooks { get; set; }
    }
}