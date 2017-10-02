using AutoMapper;
using Portal.Application.Content;
using Portal.Core.Content;

namespace Portal.MapperConfig.Resolvers
{
    public class DeleteResolver : IValueResolver<ContentDto, object, DeleteDto>
    {
        public DeleteDto Resolve(ContentDto source, object destination, DeleteDto destMember, ResolutionContext context)
        {
            return new DeleteDto
            {
                Id = source.Id,
                IsDeleted = source.IsDeleted,
                Title = source.Title
            };
        }
    }
}
