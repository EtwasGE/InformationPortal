using Portal.Core.Content.Builder.Common;
using Portal.Core.Content.Entities;
using Portal.Core.Content.Strategy.Select;
using Portal.Core.Content.Strategy.Sort;

namespace Portal.Core.Content.Builder
{
    public class AllDeletedBook : BuilderBase<Book>
    {
        public override void Select()
        {
            Context.AddSelectStrategy(new SelectByDeleted<Book>());
        }

        public override void Sorting()
        {
            Context.SetSortStrategy(new SortByDate<Book>(true));
        }
    }
}
