using Abp.Authorization;
using Abp.Authorization.Roles;
using AutoMapper;
using Portal.Application.Roles.Dto;
using Portal.Application.Sessions.Dto;
using Portal.Application.Users.Dto;
using Portal.Core.Authorization.Roles;
using Portal.Core.Authorization.Users;

namespace Portal.MapperConfig.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            // Role and permission
            CreateMap<Permission, string>().ConvertUsing(input => input.Name);
            CreateMap<RolePermissionSetting, string>().ConvertUsing(input => input.Name);

            CreateMap<User, UserLoginInfoDto>()
                .ForMember(x => x.CreationTime,
                    opt => opt.ResolveUsing(input => input.CreationTime.ToLongDateString()));

            CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
            CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());

            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
