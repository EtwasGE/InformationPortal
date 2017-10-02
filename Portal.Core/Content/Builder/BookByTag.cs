using Portal.Core.Content.Strategy.Select;

namespace Portal.Core.Content.Builder
{
    public class BookByTag : AllBook
    {
        private readonly int _tagId;

        public BookByTag(int tagId)
        {
            _tagId = tagId;
        }

        public override void Select()
        {
            Context.AddSelectStrategy(new SelectBookByTag(_tagId));
        }
    }
}
