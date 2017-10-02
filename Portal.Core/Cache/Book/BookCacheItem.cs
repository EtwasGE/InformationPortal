using System.Collections.Generic;
using Abp.AutoMapper;
using Portal.Core.Content;

namespace Portal.Core.Cache.Book
{
    [AutoMapFrom(typeof(Content.Entities.Book))]
    public class BookCacheItem : ContentDto
    {
        public string Description { get; set; }
        public string Issue { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string DatePublication { get; set; }

        public string Picture { get; set; }
        public string FilePath { get; set; }
        public string FileFormat { get; set; }

        public IList<EntityItem> Catalogs { get; set; }
        public IList<EntityItem> Authors { get; set; }
        public IList<EntityItem> Tags { get; set; }
    }
}