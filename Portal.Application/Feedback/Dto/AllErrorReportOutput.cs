using MvcPaging;

namespace Portal.Application.Feedback.Dto
{
    public class AllErrorReportOutput
    {
        public IPagedList<ErrorReportDto> Reports { get; set; }
    }
}
