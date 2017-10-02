using System.Linq;

namespace Portal.Data.Seed.Tenant
{
    public class DefaultTenantCreator
    {
        private readonly PortalDbContext _context;

        public DefaultTenantCreator(PortalDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Core.MultiTenancy.Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Core.MultiTenancy.Tenant {TenancyName = Core.MultiTenancy.Tenant.DefaultTenantName, Name = Core.MultiTenancy.Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
