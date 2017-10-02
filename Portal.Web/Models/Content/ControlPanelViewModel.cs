using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Application.Content;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;

namespace Portal.Web.Models.Content
{
    [AutoMapFrom(typeof(ShortBookDto), typeof(BookCacheItem))]
    public class ControlPanelViewModel : EntityDto
    {
        public ApproveDto Approve { get; set; }
        public DeleteDto Delete { get; set; }
        public FavoriteDto Favorite { get; set; }

        public string TextForClipboard { get; set; }
    }
}