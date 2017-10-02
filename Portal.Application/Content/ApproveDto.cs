using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;
using Portal.Core.Content;

namespace Portal.Application.Content
{
    [AutoMapFrom(typeof(ShortBookDto), typeof(BookCacheItem))]
    public class ApproveDto : EntityDto, IApproved
    {
        public string Title { get; set; }
        public bool IsApproved { get; set; }
    }
}
