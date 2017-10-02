using System.Data.Entity.Migrations;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using Portal.Core.ElasticSearch;
using Portal.Data.Seed.Content;
using Portal.Data.Seed.Host;
using Portal.Data.Seed.Tenant;

namespace Portal.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<PortalDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Portal";
        }

        protected override void Seed(PortalDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();

                //Content
                var esConfig = IocManager.Instance.Resolve<ElasticSearchConfiguration>();
                esConfig.DeleteIndex();
                esConfig.CreateIndex();

                new BookCatalogCreator(context).Create();
                new BookCreator(context).Create();

            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
