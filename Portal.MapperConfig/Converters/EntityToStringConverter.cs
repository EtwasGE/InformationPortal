using AutoMapper;
using Portal.Core.Content.Entities.Common;

namespace Portal.MapperConfig.Converters
{
    public class EntityToStringConverter : ITypeConverter<EntityBase, string>
    {
        public string Convert(EntityBase source, string destination, ResolutionContext context)
        {
            return source?.Name ?? string.Empty;
        }
    }
}
