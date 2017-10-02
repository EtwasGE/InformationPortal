using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;

namespace Portal.Application.Content
{
    [AutoMapFrom(typeof(ShortBookDto), typeof(BookCacheItem))]
    public class FavoriteDto : EntityDto
    {
        public bool IsFavorite { get; set; }
    }
}
