using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Portal.Core.Cache.Book;
using Portal.Core.Content.Entities.Common;

namespace Portal.MapperConfig.Converters
{
    public class CatalogToEntityItemListConverter<TContent, TCatalog> : ITypeConverter<TCatalog, IList<EntityItem>>
        where TContent : ContentEntityBase
        where TCatalog : CatalogBase<TContent, TCatalog>
    {
        public IList<EntityItem> Convert(TCatalog source, IList<EntityItem> destination, ResolutionContext context)
        {
            return SelectItems(source.Parent, source);
        }

        private static IList<EntityItem> SelectItems(params TCatalog[] catalogs)
        {
            return (from catalog in catalogs
                where catalog != null
                select new EntityItem
                {
                    Id = catalog.Id,
                    Name = catalog.Name,
                    ContentsCount = MapperHelper.GetContentsCount<TContent, TCatalog>(catalog)
                }).ToList();
        }

       
    }
}
