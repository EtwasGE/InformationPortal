using System.Collections.Generic;
using System.Linq;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Strategy
{
    public class ContextStrategy<TContent>
        where TContent : ContentEntityBase
    {
        private IQueryable<TContent> _source;
        private readonly IList<ISelectStrategy<TContent>> _selects;
        private readonly IList<IFilterStrategy<TContent>> _filters;
        private ISortStrategy<TContent> _sort;

        public ContextStrategy(IQueryable<TContent> source)
        {
            _source = source;
            _selects = new List<ISelectStrategy<TContent>>();
            _filters = new List<IFilterStrategy<TContent>>();
        }

        public void AddSelectStrategy(ISelectStrategy<TContent> strategy)
        {
            _selects.Add(strategy);
        }

        public void AddFilterStrategy(IFilterStrategy<TContent> strategy)
        {
            _filters.Add(strategy);
        }

        public void SetSortStrategy(ISortStrategy<TContent> strategy)
        {
            _sort = strategy;
        }

        public IQueryable<TContent> GetResult()
        {
            return _source;
        }

        public void Select()
        {
            if (_selects.Any())
            {
                _source = _selects
                    .TakeWhile(filterStrategy => _source.Any())
                    .Aggregate<ISelectStrategy<TContent>, IQueryable<TContent>>(null,
                        (current, strategy) => current?.Union(strategy.Select(_source))
                                                     ?? strategy.Select(_source))
                    .Distinct();
            }
        }

        public void Filter()
        {
            foreach (var filter in _filters)
            {
                _source = filter.Filter(_source);
            }
        }

        public void Sorting()
        {
            if (_sort != null && _source.Any())
            {
                _source = _sort.Sort(_source);
            }
        }
    }
}