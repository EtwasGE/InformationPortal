using System.Collections.Generic;
using Portal.Application.Roles.Dto;
using Portal.Application.Users.Dto;

namespace Portal.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}