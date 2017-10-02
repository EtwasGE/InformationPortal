using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Portal.Core.Content.Entities.Common;

namespace Portal.Data.Configurations
{
    internal sealed class CatalogConfig<TContent, TCatalog> : EntityTypeConfiguration<TCatalog>
        where TCatalog : CatalogBase<TContent, TCatalog>
        where TContent : ContentEntityBase<TContent, TCatalog>
    {
        public CatalogConfig()
        {
            ToTable($"Ip{typeof(TCatalog).Name}s");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256); // for index

            Property(t => t.Name)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute($"IX_Name_{typeof(TCatalog).Name}") { IsUnique = false }));

            Property(t => t.Order)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute($"IX_Order_{typeof(TCatalog).Name}") { IsUnique = false }));

            HasMany(x => x.Childrens)
                .WithOptional(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

            // не используйте здесь Map(m => m.MapKey("CatalogId"),
            // потому что при выборке контента по каталогу выдаст исключение:
            // FK Constriant not found for association 'Portal.Data.BookCatalog_Contents' 
            // - must directly specify foreign keys on model to be able to apply this filter
            HasMany(x => x.Contents)
                .WithRequired(x => x.Catalog)
                .HasForeignKey(x => x.CatalogId);
        }
    }
}
