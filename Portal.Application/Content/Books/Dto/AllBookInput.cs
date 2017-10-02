using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Portal.Core.Content;

namespace Portal.Application.Content.Books.Dto
{
    public class AllBookInput
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SortType SortType { get; set; }
        public bool OrderIsDescending { get; set; }
    }

    [AutoMapFrom(typeof(AllBookInput))]
    public class AllBookInput<TPrimaryKey> : AllBookInput, IEntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
