using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Sort
{
    public class SortByViewer<TContent, TCatalog, TViewer> : ISortStrategy<TContent>
        where TContent : ContentEntityBase<TContent, TCatalog, TViewer>
        where TCatalog : CatalogBase<TContent, TCatalog>
        where TViewer : ViewerBase
    {
        private readonly bool _isDescending;

        public SortByViewer(bool isDescending)
        {
            _isDescending = isDescending;
        }

        public IOrderedQueryable<TContent> Sort(IQueryable<TContent> source)
        {
            return (_isDescending
                    ? source.OrderByDescending(x => x.ViewersCount)
                    : source.OrderBy(x => x.ViewersCount))
                .ThenByDescending(x => x.IsRecommend)
                .ThenBy(x => x.Title);
        }
    }
}
