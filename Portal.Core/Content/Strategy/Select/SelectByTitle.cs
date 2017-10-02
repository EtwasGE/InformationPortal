using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy.Select
{
    public class SelectByTitle<TContent> : ISelectStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private readonly string _title;

        public SelectByTitle(string title)
        {
            _title = title;
        }

        public IQueryable<TContent> Select(IQueryable<TContent> source)
        {
            return source.Where(x => x.Title.Contains(_title) || _title.Contains(x.Title));
        }
    }
}
