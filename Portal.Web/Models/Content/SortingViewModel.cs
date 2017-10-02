using System.Collections.Generic;

namespace Portal.Web.Models.Content
{
    public class SortingViewModel
    {
        public bool IsDescending { get; set; }
        public IList<SortItem> Sortings { get; set; }
    }
}