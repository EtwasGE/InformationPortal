using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Portal.Core.Cache.Author
{
    [AutoMapFrom(typeof(Content.Entities.Author))]
    public class AuthorCacheItem : EntityDto
    {
        public string Name { get; set; }
    }
}
