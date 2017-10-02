using System;
using Portal.Core.Content.Builder.Common;
using Portal.Core.Content.Entities;
using Portal.Core.Content.Strategy;
using Portal.Core.Content.Strategy.Sort;

namespace Portal.Core.Content.Builder
{
    public class AllBook : BuilderSortBase<Book>
    {
        public override void Sorting()
        {
            var sortStrategy = GetSortStrategy();
            Context.SetSortStrategy(sortStrategy);
        }

        private ISortStrategy<Book> GetSortStrategy()
        {
            switch (SortType)
            {
                case SortType.Name:
                    return new SortByTitle<Book>(IsDescending);
                case SortType.Author:
                    return new SortBookByAuthor(IsDescending);
                case SortType.View:
                    return new SortByViewer<Book, BookCatalog, BookViewer>(IsDescending);
                case SortType.Favorite:
                    return new SortByFavourite<Book>(IsDescending);
                case SortType.Date:
                    return new SortByDate<Book>(IsDescending);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
