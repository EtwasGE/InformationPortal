using System.Reflection;
using System.Web.Http;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Portal.Application;

namespace Portal.WebApi.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(ApplicationModule))]
    public class WebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //    .ForAll<IApplicationService>(typeof(ApplicationModule).Assembly, "app")
            //    .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
