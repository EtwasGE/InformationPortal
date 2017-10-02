using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Portal.Application.Feedback.Dto;
using Portal.Core.Content;

namespace Portal.Web.Models.Feedback
{
    [AutoMapTo(typeof(AddErrorReportInput))]
    public class AddErrorReportViewModel
    {
        public int ContentId { get; set; }
        public MessageSubjectType Subject { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}