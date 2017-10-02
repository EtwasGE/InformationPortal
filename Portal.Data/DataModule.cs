using System.Data.Entity;
using System.Reflection;
using Abp.Domain.Repositories;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Castle.MicroKernel.Registration;
using Portal.Core;
using Portal.Core.DataFilters;
using Portal.Data.Repositories;

namespace Portal.Data
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(CoreModule))]
    public class DataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PortalDbContext>());

            Configuration.DefaultNameOrConnectionString = CoreConsts.ConnectionStringName;
            Configuration.UnitOfWork.RegisterFilter(FilterNames.Approved, true);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component
                    .For(typeof(IRepository<>))
                    .ImplementedBy(typeof(Repository<>))
                    .LifestyleTransient());
        }
    }
}
