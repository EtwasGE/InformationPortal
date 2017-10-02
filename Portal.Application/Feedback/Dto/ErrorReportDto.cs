using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Core.Content;
using Portal.Core.Content.Entities;


namespace Portal.Application.Feedback.Dto
{
    [AutoMapFrom(
        typeof(BookErrorReport),
        typeof(TrainingErrorReport))]
    public class ErrorReportDto : CreationAuditedEntityDto
    {
        public MessageSubjectType Subject { get; set; }
        public string Comment { get; set; }

        public int ContentId { get; set; }
        public string ContentTitle { get; set; }

        public string CreatorUser { get; set; }
    }
}
