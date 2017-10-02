using Abp.AutoMapper;
using Portal.Application.Feedback.Dto;
using Portal.Core.Content;

namespace Portal.Web.Models.Feedback
{
    [AutoMapFrom(typeof(ErrorReportDto))]
    public class ErrorReportViewModel
    {
        public MessageSubjectType Subject { get; set; }
        public string Comment { get; set; }

        public int ContentId { get; set; }
        public string ContentTitle { get; set; }

        public string CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string CreatorUser { get; set; }
    }
}