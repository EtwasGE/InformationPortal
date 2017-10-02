using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Portal.Application.MultiTenancy.Dto;

namespace Portal.Application.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
