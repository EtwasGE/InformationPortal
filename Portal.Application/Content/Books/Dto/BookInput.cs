using Abp.Application.Services.Dto;

namespace Portal.Application.Content.Books.Dto
{
    public class BookInput : EntityDto
    {
        public int PageSize { get; set; }
    }
}
