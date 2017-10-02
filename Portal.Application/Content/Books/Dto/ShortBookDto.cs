using Abp.AutoMapper;
using Portal.Core.Content;
using Portal.Core.Content.Entities;

namespace Portal.Application.Content.Books.Dto
{
    [AutoMapFrom(typeof(Book))]
    public class ShortBookDto : ContentDto
    {
        public string Picture { get; set; }
        public string Authors { get; set; }
    }
}