using Portal.Core.Content.Strategy.Select;

namespace Portal.Core.Content.Builder
{
    public class BookByAuthor : AllBook
    {
        private readonly int _authorId;

        public BookByAuthor(int authorId)
        {
            _authorId = authorId;
        }

        public override void Select()
        {
            Context.AddSelectStrategy(new SelectBookByAuthor(_authorId));
        }
    }
}
