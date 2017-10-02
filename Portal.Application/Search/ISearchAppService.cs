using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Nest;

namespace Portal.Application.Search
{
    public interface ISearchAppService<TContentIndexItem> : IApplicationService
        where TContentIndexItem : EntityDto, IPassivable
    {
        Task<ISearchResponse<TContentIndexItem>> SearchAsync(Dto.SearchInput input);
    }
}
