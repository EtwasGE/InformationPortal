using System.Collections.Generic;
using MvcPaging;
using Nest;
using Portal.Core.ElasticSearch;

namespace Portal.Web.Models.Content.Books
{
    //[AutoMapFrom(typeof(ISearchResponse<BookIndexItem>))]
    public class SearchBookViewModel
    {
        public string Query { get; set; }
        public long Took { get; set; }

        public IPagedList<IHit<BookIndexItem>> Results { get; set; }
        public IReadOnlyDictionary<string, Suggest<BookIndexItem>[]> Suggestions { get; set; }
        
        public bool IsShownResult { get; set; }
        public bool IsShownSuggest { get; set; }
        public bool IsShownPagination { get; set; }
    }
}