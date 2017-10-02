using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Builder.Common
{
    public abstract class BuilderSortBase<TContent> : BuilderBase<TContent>
        where TContent : ContentEntityBase
    {
        protected SortType SortType { get; private set; }
        protected bool IsDescending { get; private set; }

        public BuilderSortBase<TContent> SortingBy(SortType sortType)
        {
            SortType = sortType;
            return this;
        }

        public BuilderSortBase<TContent> OrderBy(bool isDescending)
        {
            IsDescending = isDescending;
            return this;
        }
    }
}
