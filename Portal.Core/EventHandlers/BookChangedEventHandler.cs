using Portal.Core.Content.Entities;
using Portal.Core.ElasticSearch;

namespace Portal.Core.EventHandlers
{
    public class BookChangedEventHandler 
        : ContentChangedEventHandlerBase<Book, BookIndexItem>
    {
        public BookChangedEventHandler(ElasticSearchConfiguration config)
            : base(config)
        {
        }
    }
}