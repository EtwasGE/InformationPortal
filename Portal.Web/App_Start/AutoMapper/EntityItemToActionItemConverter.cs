using AutoMapper;
using Portal.Core.Cache.Book;
using Portal.Web.Models.Content;

namespace Portal.Web.AutoMapper
{
    public class EntityItemToActionItemConverter : ITypeConverter<EntityItem, ActionItem>
    {
        public ActionItem Convert(EntityItem source, ActionItem destination, ResolutionContext context)
        {
            return new ActionItem
            {
                Id = source.Id,
                Name = source.Name,
                IsAction = source.ContentsCount > 1
            };
        }
    }
}