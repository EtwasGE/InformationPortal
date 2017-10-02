using AutoMapper;
using Portal.Core.Cache.Book;
using Portal.Core.Content.Entities.Common;

namespace Portal.MapperConfig.Converters
{
    public class EntityToEntityItemConverter<TContent> : ITypeConverter<EntityBase<TContent>, EntityItem>
        where TContent : ContentEntityBase
    {
        public EntityItem Convert(EntityBase<TContent> source, EntityItem destination, ResolutionContext context)
        {
            return new EntityItem
            {
                Id = source.Id,
                Name = source.Name ?? string.Empty,
                ContentsCount = source.Contents.Count
            };
        }
    }
}
