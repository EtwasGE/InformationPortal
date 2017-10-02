using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;

namespace Portal.Application.Content
{
    [AutoMapFrom(typeof(ShortBookDto), typeof(BookCacheItem))]
    public class DeleteDto : EntityDto, ISoftDelete
    {
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
    }
}
