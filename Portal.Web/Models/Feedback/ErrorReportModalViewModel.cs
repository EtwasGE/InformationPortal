using System.Collections.Generic;
using System.Web.Mvc;

namespace Portal.Web.Models.Feedback
{
    public class ErrorReportModalViewModel
    {
        public int ContentId { get; set; }
        public IList<SelectListItem> Subjects { get; set; }
    }
}