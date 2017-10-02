using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Runtime.Session;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Portal.Application.Content;
using Portal.Core.Authorization.Users;

namespace Portal.MapperConfig.Resolvers
{
    public class FavoriteBookResolver : IValueResolver<EntityDto<int>, object, FavoriteDto>
    {
        public FavoriteDto Resolve(EntityDto<int> source, object destination, FavoriteDto destMember, ResolutionContext context)
        {
            var userManager = IocManager.Instance.Resolve<UserManager>();
            var currentUserId = userManager.AbpSession.GetUserId();
            var currentUser = userManager.FindById(currentUserId);

            return new FavoriteDto
            {
                Id = source.Id,
                IsFavorite = currentUser.FavouriteBooks.Any(x => x.Id == source.Id)
            };
        }
    }
}
