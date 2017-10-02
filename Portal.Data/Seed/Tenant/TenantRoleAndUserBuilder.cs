using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Portal.Core;
using Portal.Core.Authorization;
using Portal.Core.Authorization.Roles;
using Portal.Core.Authorization.Users;

namespace Portal.Data.Seed.Tenant
{
    public class TenantRoleAndUserBuilder
    {
        private readonly PortalDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(PortalDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            //admin role

            var adminRole =
                _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin,
                    StaticRoleNames.Tenants.Admin) {IsStatic = true});
                _context.SaveChanges();

                //Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new AppAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }

            //approver role

            var approverRole =
                _context.Roles.FirstOrDefault(
                    r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Approver);
            if (approverRole == null)
            {
                approverRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Approver,
                    StaticRoleNames.Tenants.Approver) {IsStatic = true});
                _context.SaveChanges();

                //Grant permissions to approver role
                
                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.ContentChange,
                        IsGranted = true,
                        RoleId = approverRole.Id
                    });

                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.ContentAdd,
                        IsGranted = true,
                        RoleId = approverRole.Id
                    });
            }

            //user role

            var userRole =
                _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.User);
            if (userRole == null)
            {
                userRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.User,
                    StaticRoleNames.Tenants.User) {IsStatic = true, IsDefault = true});
                _context.SaveChanges();

                //Grant permissions to user role

                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.ContentAdd,
                        IsGranted = true,
                        RoleId = userRole.Id
                    });
            }

            //admin user

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == CoreConsts.DefaultAdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, CoreConsts.DefaultAdminEmailAddress, CoreConsts.DefaultAdminUserName, CoreConsts.DefaultAdminPassword);
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}