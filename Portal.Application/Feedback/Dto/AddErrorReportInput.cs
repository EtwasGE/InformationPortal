using Abp.AutoMapper;
using Portal.Core.Content;
using Portal.Core.Content.Entities;

namespace Portal.Application.Feedback.Dto
{
    [AutoMapTo(
        typeof(BookErrorReport), 
        typeof(TrainingErrorReport))]
    public class AddErrorReportInput
    {
        public int ContentId { get; set; }
        public MessageSubjectType Subject { get; set; }
        public string Comment { get; set; }
    }
}