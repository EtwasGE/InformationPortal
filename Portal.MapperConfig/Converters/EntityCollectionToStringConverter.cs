using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Portal.Core.Content.Entities.Common;


namespace Portal.MapperConfig.Converters
{
    public class EntityCollectionToStringConverter<TEntity> : ITypeConverter<ICollection<TEntity>, string>
        where TEntity : EntityBase
    {
        private readonly int? _limit;
        public EntityCollectionToStringConverter(int? limit = null)
        {
            _limit = limit;
        }

        public string Convert(ICollection<TEntity> source, string destination, ResolutionContext context)
        {
            if (source == null)
            {
                return string.Empty;
            }

            var items = source.Select(x => x.Name).ToList();
            return items.Any() ? MapperHelper.ListToString(items, _limit) : string.Empty;
        }
    }
}
