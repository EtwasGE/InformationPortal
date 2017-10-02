using System.Linq;
using Portal.Core.Content.Builder.Common;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Builder
{
    public static class Director<TContent>
        where TContent : ContentEntityBase
    {
        public static IQueryable<TContent> Construct(BuilderBase<TContent> builder)
        {
            builder.Select();
            builder.Filter();
            builder.Sorting();
            return builder.Construct();
        }
    }
}
