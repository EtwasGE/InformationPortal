using System.Threading.Tasks;
using Abp.Application.Services;
using Portal.Application.Sessions.Dto;

namespace Portal.Application.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
