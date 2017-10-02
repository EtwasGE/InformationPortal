using Abp.AutoMapper;
using MvcPaging;
using Portal.Application.Feedback.Dto;

namespace Portal.Web.Models.Feedback
{
    [AutoMapFrom(typeof(AllErrorReportOutput))]
    public class PreViewErrorReportViewModel
    {
        public IPagedList<ErrorReportViewModel> Reports { get; set; }
        public bool IsShownResult { get; set; }
        public bool IsShownPagination { get; set; }

        public string ActiveMenu { get; set; }
    }
}