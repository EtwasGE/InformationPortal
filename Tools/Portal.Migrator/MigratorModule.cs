using System.Data.Entity;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Portal.Data;
using Portal.MapperConfig.Profiles;

namespace Portal.Migrator
{
    [DependsOn(
        typeof(DataModule), 
        typeof(AbpAutoMapperModule)
        )]
    public class MigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<PortalDbContext>(null);
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.AddProfile<CommonProfile>();
                config.AddProfile<BookIndexProfile>();
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}