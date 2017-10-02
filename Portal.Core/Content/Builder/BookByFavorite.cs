using Portal.Core.Content.Entities;
using Portal.Core.Content.Strategy.Select;

namespace Portal.Core.Content.Builder
{
    public class BookByFavorite : AllBook
    {
        private readonly long _userId;

        public BookByFavorite(long userId)
        {
            _userId = userId;
        }

        public override void Select()
        {
            Context.AddSelectStrategy(new SelectByFavorite<Book>(_userId));
        }
    }
}
