using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Runtime.Security;
using Microsoft.AspNet.Identity;
using Portal.Application.MultiTenancy.Dto;
using Portal.Core;
using Portal.Core.Authorization;
using Portal.Core.Authorization.Roles;
using Portal.Core.Authorization.Users;
using Portal.Core.Editions;
using Portal.Core.MultiTenancy;
using Portal.Data;

namespace Portal.Application.MultiTenancy
{
    [AbpAuthorize(PermissionNames.PagesTenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly DbMigrator _dbMigrator;

        public TenantAppService(
            IRepository<Tenant, int> repository,

            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,

            RoleManager roleManager,
            DbMigrator dbMigrator
        ) : base(repository)
        {
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _roleManager = roleManager;
            _dbMigrator = dbMigrator;
            _userManager = userManager;
        }

        public override async Task<TenantDto> Create(CreateTenantDto input)
        {
            CheckCreatePermission();

            //Create tenant
            var tenant = ObjectMapper.Map<Tenant>(input);
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id.

            //Create tenant database
            _dbMigrator.CreateOrMigrateForTenant(tenant);

            //We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); //To get static role ids

                //grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, CoreConsts.DefaultAdminUserName, CoreConsts.DefaultAdminPassword);
                CheckErrors(await _userManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id

                //Assign admin user to role!
                CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return MapToEntityDto(tenant);
        }

        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            //Manually mapped since TenantDto contains non-editable properties too.
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}