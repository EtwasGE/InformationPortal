using System.Collections.Generic;
using Abp.AutoMapper;
using Portal.Application.Content.Books.Dto;

namespace Portal.Web.Models.Content.Books
{
    [AutoMapFrom(typeof(BookOutput))]
    public class DetailBookViewModel
    {
        public FullBookViewModel Book { get; set; }
        public string DefaultPicture { get; set; }
        public IList<ShortBookViewModel> SimilarBooks { get; set; }
        public bool IsShownSimilarResult { get; set; }
    }
}