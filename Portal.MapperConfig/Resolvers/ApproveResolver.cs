using AutoMapper;
using Portal.Application.Content;
using Portal.Core.Content;

namespace Portal.MapperConfig.Resolvers
{
    public class ApproveResolver : IValueResolver<ContentDto, object, ApproveDto>
    {
        public ApproveDto Resolve(ContentDto source, object destination, ApproveDto destMember, ResolutionContext context)
        {
            return new ApproveDto
            {
                Id = source.Id,
                IsApproved = source.IsApproved,
                Title = source.Title
            };
        }
    }
}
