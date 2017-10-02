using System.Threading.Tasks;
using Abp.Application.Services;
using Portal.Application.Configuration.Dto;

namespace Portal.Application.Configuration
{
    public interface IConfigurationAppService : IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}