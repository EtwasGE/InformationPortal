using Abp.Application.Services.Dto;

namespace Portal.Core.Cache.Book
{
    public class EntityItem : EntityDto
    {
        public string Name { get; set; }
        public int ContentsCount { get; set; }
    }
}