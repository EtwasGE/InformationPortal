using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using Portal.Core;
using Portal.Core.Authorization.Roles;
using Portal.Core.Authorization.Users;
using Portal.Core.Content;
using Portal.Core.Content.Entities;
using Portal.Core.DataFilters;
using Portal.Core.MultiTenancy;
using Portal.Data.Configurations;

namespace Portal.Data
{
    public class PortalDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        public PortalDbContext()
            : base(CoreConsts.ConnectionStringName)
        {
        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in DataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of PortalDbContext since ABP automatically handles it.
         */
        public PortalDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        //This constructor is used in tests
        public PortalDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {
        }

        public PortalDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Filter(FilterNames.Approved, (IApproved entity) => entity.IsApproved, true);

            // book library
            modelBuilder.Configurations.Add(new BookConfig());
            modelBuilder.Configurations.Add(new EntityConfig<Author>());
            modelBuilder.Configurations.Add(new EntityConfig<Issue>());
            modelBuilder.Configurations.Add(new EntityConfig<Publisher>());
            modelBuilder.Configurations.Add(new EntityConfig<Tag>());
            modelBuilder.Configurations.Add(new ViewerConfig<BookViewer>());
            modelBuilder.Configurations.Add(new CatalogConfig<Book, BookCatalog>());
            modelBuilder.Configurations.Add(new ErrorReportConfig<Book, BookErrorReport>());

            // training library
            modelBuilder.Configurations.Add(new TrainingConfig());
            modelBuilder.Configurations.Add(new EntityConfig<Company>());
            modelBuilder.Configurations.Add(new ViewerConfig<TrainingViewer>());
            modelBuilder.Configurations.Add(new CatalogConfig<Training, TrainingCatalog>());
            modelBuilder.Configurations.Add(new ErrorReportConfig<Training, TrainingErrorReport>());

            // common
            modelBuilder.Configurations.Add(new EntityConfig<Language>());
        }
    }
}
