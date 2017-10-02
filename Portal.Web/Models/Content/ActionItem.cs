using Abp.AutoMapper;
using Portal.Core.Cache.Book;

namespace Portal.Web.Models.Content
{
    [AutoMapFrom(typeof(EntityItem))]
    public class ActionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAction { get; set; }
    }
}