using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectByCatalog<TContent, TCatalog> : ISelectStrategy<TContent>
        where TContent : ContentEntityBase<TContent, TCatalog>
        where TCatalog : CatalogBase<TContent, TCatalog>
    {
        private readonly int _catalogId;

        public SelectByCatalog(int catalogId)
        {
            _catalogId = catalogId;
        }

        public IQueryable<TContent> Select(IQueryable<TContent> source)
        {
            return source.Where(x => x.CatalogId == _catalogId || x.Catalog.ParentId == _catalogId);
        }
    }
}
