using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.AutoMapper;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using Portal.Application;
using Portal.Application.Catalogs;
using Portal.Application.Feedback;
using Portal.Core.Content;
using Portal.Core.Content.Entities;
using Portal.Data;
using Portal.MapperConfig.Profiles;
using Portal.Web.AutoMapper;
using Portal.Web.Controllers;
using Portal.Web.Helpers;

//using Portal.WebApi.Api;

namespace Portal.Web
{
    [DependsOn(
        typeof(DataModule),
        typeof(ApplicationModule),
        typeof(AbpAutoMapperModule),
        //typeof(WebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class WebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<AppNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage(CoreConsts.ConnectionStringName);
            //});

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.AddProfile<CommonProfile>();
                config.AddProfile<BookIndexProfile>();
                config.AddProfile<BookCacheProfile>();

                config.AddProfile<BookDtoProfile>();
                config.AddProfile<CatalogDtoProfile>();

                config.AddProfile<AccountProfile>();
                config.AddProfile<FeedbackProfile>();

                config.AddProfile<PreViewBookViewModelProfile>();
                config.AddProfile<DetailBookViewModelProfile>();
                config.AddProfile<PreViewErrorReportViewModelProfile>();
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
            );

            IocManager.IocContainer.Register(
                Component
                    .For<UrlHelper>()
                    .UsingFactoryMethod(() =>
                    {
                        var context = new HttpContextWrapper(HttpContext.Current);
                        var routeData = RouteTable.Routes.GetRouteData(context);
                        return new UrlHelper(new RequestContext(context, routeData));
                    })
                    .LifestylePerWebRequest());

            IocManager.IocContainer.Register(
                Component
                    .For<LayoutController>()
                    .ImplementedBy<LayoutController>()
                    .DependsOn((kernel, dictionary) =>
                    {
                        switch (CookieHelper.ContentType)
                        {
                            case ContentType.Books:
                                dictionary[typeof(ICatalogAppService)] =
                                    kernel.Resolve<CatalogAppService<Book, BookCatalog>>();
                                dictionary["MainMenuName"] = "BookCatalogs";
                                break;
                            case ContentType.Trainings:
                                dictionary[typeof(ICatalogAppService)] =
                                    kernel.Resolve<CatalogAppService<Training, TrainingCatalog>>();
                                dictionary["MainMenuName"] = "TrainingCatalogs";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    })
                    .IsDefault()
                    .Named("OverridingLayoutController")
                    .LifestyleTransient());

            IocManager.IocContainer.Register(
                Component
                    .For<FeedbackController>()
                    .ImplementedBy<FeedbackController>()
                    .DependsOn((kernel, dictionary) =>
                    {
                        switch (CookieHelper.ContentType)
                        {
                            case ContentType.Books:
                                dictionary[typeof(IFeedbackAppService)] =
                                    kernel.Resolve<FeedbackAppService<BookErrorReport>>();
                                dictionary["ActiveMenuItemName"] = PageNames.Books;
                                break;
                            case ContentType.Trainings:
                                dictionary[typeof(IFeedbackAppService)] =
                                    kernel.Resolve<FeedbackAppService<TrainingErrorReport>>();
                                dictionary["ActiveMenuItemName"] = PageNames.Trainings;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    })
                    .IsDefault()
                    .Named("OverridingFeedbackController")
                    .LifestyleTransient());
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
