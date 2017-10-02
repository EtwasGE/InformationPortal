using Abp.AutoMapper;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;

namespace Portal.Web.Models.Content
{
    [AutoMapFrom(typeof(ShortBookDto), typeof(BookCacheItem))]
    public class ContentInfoViewModel
    {
        public string CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string CreatorUser { get; set; }

        public string DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        public string DeleterUser { get; set; }
        
        public string LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string LastModifierUser { get; set; }

        public int ViewersCount { get; set; }
        public int FavoritesCount { get; set; }
        public bool IsRecommend { get; set; }
    }
}