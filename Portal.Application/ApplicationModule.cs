using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using Portal.Application.Catalogs;
using Portal.Application.Content.Books;
using Portal.Application.Feedback;
using Portal.Application.Search;
using Portal.Core;
using Portal.Core.Cache.Catalog;
using Portal.Core.Content.Entities;
using Portal.Core.ElasticSearch;

namespace Portal.Application
{
    [DependsOn(typeof(CoreModule))]
    public class ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.Register<IFeedbackAppService, FeedbackAppService<BookErrorReport>>(DependencyLifeStyle.Transient);
            IocManager.Register<IFeedbackAppService, FeedbackAppService<TrainingErrorReport>>(DependencyLifeStyle.Transient);

            IocManager.Register<ICatalogAppService, CatalogAppService<Book, BookCatalog>>(DependencyLifeStyle.Transient);
            IocManager.Register<ICatalogAppService, CatalogAppService<Training, TrainingCatalog>>(DependencyLifeStyle.Transient);

            IocManager.IocContainer.Register(
                Component
                    .For<IBookAppService>()
                    .ImplementedBy<BookAppService>()
                    .DependsOn(Dependency.OnComponent<ICatalogCache, CatalogCache<BookCatalog>>())
                    .IsDefault()
                    .Named("OverridingBookAppService")
                    .LifestyleTransient());

            IocManager.IocContainer.Register(
                Component
                    .For<ISearchAppService<BookIndexItem>>()
                    .ImplementedBy<BookSearchAppService>()
                    .IsDefault()
                    .Named("OverridingBookSearchAppService")
                    .LifestyleTransient());
        }
    }
}
