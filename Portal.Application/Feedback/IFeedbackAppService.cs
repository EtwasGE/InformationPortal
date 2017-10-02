using System.Threading.Tasks;
using Abp.Application.Services;
using Portal.Application.Feedback.Dto;

namespace Portal.Application.Feedback
{
    public interface IFeedbackAppService : IApplicationService
    {
        Task AddErrorReportAsync(AddErrorReportInput input);
        Task<AllErrorReportOutput> GetErrorReportsAsync(AllErrorReportInput input);
    }
}
