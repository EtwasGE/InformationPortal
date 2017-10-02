using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Portal.Core.Cache.Tag
{
    [AutoMapFrom(typeof(Content.Entities.Tag))]
    public class TagCacheItem : EntityDto
    {
        public string Name { get; set; }
    }
}
