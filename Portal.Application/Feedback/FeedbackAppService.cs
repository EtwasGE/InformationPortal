using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using MvcPaging;
using Portal.Application.Feedback.Dto;
using Portal.Core.Authorization.Users;

namespace Portal.Application.Feedback
{
    [AbpAuthorize]
    public class FeedbackAppService<TErrorReport> : AppServiceBase, IFeedbackAppService
        where TErrorReport : CreationAuditedEntity<int, User>, IPassivable
    {
        private readonly IRepository<TErrorReport> _repository;
        public FeedbackAppService(IRepository<TErrorReport> repository)
        {
            _repository = repository;
        }

        public async Task AddErrorReportAsync(AddErrorReportInput input)
        {
            var errorReport = input.MapTo<TErrorReport>();
            await _repository.InsertAsync(errorReport);
        }

        public Task<AllErrorReportOutput> GetErrorReportsAsync(AllErrorReportInput input)
        {
            var reports = _repository
                .GetAll()
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreationTime);

            return Task.Run(() => new AllErrorReportOutput
            {
                Reports = reports
                    .ToPagedList(input.PageIndex, input.PageSize)
                    .MapTo<IPagedList<ErrorReportDto>>()
            });
        }
    }
}