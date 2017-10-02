using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Core.Cache.Book;

namespace Portal.Web.Models.Content.Books
{
    [AutoMapFrom(typeof(BookCacheItem))]
    public class FullBookViewModel : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Issue { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string DatePublication { get; set; }

        public string Picture { get; set; }
        public string FilePath { get; set; }
        public string FileFormat { get; set; }

        public IList<ActionItem> Catalogs { get; set; }
        public IList<ActionItem> Authors { get; set; }
        public IList<ActionItem> Tags { get; set; }

        public ContentInfoViewModel ContentInfo { get; set; }
        public ControlPanelViewModel ControlPanel { get; set; }
    }
}