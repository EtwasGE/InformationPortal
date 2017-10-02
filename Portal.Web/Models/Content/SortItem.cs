using Portal.Core.Content;

namespace Portal.Web.Models.Content
{
    public class SortItem
    {
        public SortItem(SortType sortType, string displayName, string title, bool isActive = false)
        {
            SortType = sortType;
            DisplayName = displayName;
            Title = title;
            IsActive = isActive;
        }

        public SortType SortType { get; }
        public string DisplayName { get; }
        public string Title { get; }
        public bool IsActive { get; }
    }
}