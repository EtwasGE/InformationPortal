using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Portal.Core.Cache.Book;

namespace Portal.MapperConfig.Converters
{
    public class EntityItemListToStringConverter : ITypeConverter<IList<EntityItem>, string>
    {
        private readonly int? _limit;
        public EntityItemListToStringConverter(int? limit = null)
        {
            _limit = limit;
        }

        public string Convert(IList<EntityItem> source, string destination, ResolutionContext context)
        {
            var items = source.Select(x => x.Name).ToList();
            return items.Any() ? MapperHelper.ListToString(items, _limit) : string.Empty;
        }
    }
}
