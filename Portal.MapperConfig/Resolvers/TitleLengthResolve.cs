using AutoMapper;
using Portal.Core.Content;

namespace Portal.MapperConfig.Resolvers
{
    public class TitleLengthResolve : IValueResolver<ContentDto, object, string>
    {
        private readonly int? _limit;
        public TitleLengthResolve(int? limit)
        {
            _limit = limit;
        }

        public string Resolve(ContentDto source, object destination, string destMember, ResolutionContext context)
        {
            if (_limit == null || source.Title.Length <= _limit)
            {
                return source.Title;
            }

            return source.Title.Substring(0, (int) _limit) + "...";
        }
    }
}
