using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Application.Content.Books.Dto;

namespace Portal.Web.Models.Content.Books
{
    [AutoMapFrom(typeof(ShortBookDto))]
    public class ShortBookViewModel : EntityDto
    {
        public string Title { get; set; } 
        public string Picture { get; set; }
        public string Authors { get; set; }
        
        public ContentInfoViewModel ContentInfo { get; set; }
        public ControlPanelViewModel ControlPanel { get; set; }
    }
}