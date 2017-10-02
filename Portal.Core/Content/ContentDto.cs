using Abp.Application.Services.Dto;

namespace Portal.Core.Content
{
    public abstract class ContentDto : FullAuditedEntityDto, IApproved
    {
        public string Title { get; set; }
        public bool IsRecommend { get; set; }
        public int ViewersCount { get; set; }
        public int FavoritesCount { get; set; }

        public string CreatorUser { get; set; }
        public string DeleterUser { get; set; }

        public bool IsApproved { get; set; }
    }
}
