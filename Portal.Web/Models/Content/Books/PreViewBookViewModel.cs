using Abp.AutoMapper;
using MvcPaging;
using Portal.Application.Content.Books.Dto;

namespace Portal.Web.Models.Content.Books
{
    [AutoMapFrom(typeof(AllBookOutput))]
    public class PreViewBookViewModel
    {
        public IPagedList<ShortBookViewModel> Books { get; set; }
        public string Header { get; set; }
        public bool IsShownResult { get; set; }
        public bool IsShownPagination { get; set; }
    }
}