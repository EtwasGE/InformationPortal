using MvcPaging;

namespace Portal.Web.Models.Content
{
    public class PaginationViewModel
    {
        public IPagedList Results { get; set; }
        public object RouteValues { get; set; }
        public string HttpMethod { get; set; } = "GET";
    }
}