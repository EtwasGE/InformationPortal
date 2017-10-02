using MvcPaging;

namespace Portal.Application.Content.Books.Dto
{
    public class AllBookOutput
    {
        public AllBookOutput(IPagedList<ShortBookDto> books, string header)
        {
            Books = books;
            Header= header;
        }

        public IPagedList<ShortBookDto> Books { get; set; }
        public string Header { get; set; }
    }
}
