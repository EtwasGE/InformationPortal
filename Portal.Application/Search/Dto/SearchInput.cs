namespace Portal.Application.Search.Dto
{
    public class SearchInput
    {
        public string Query { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
