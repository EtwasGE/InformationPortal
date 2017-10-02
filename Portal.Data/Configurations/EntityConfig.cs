using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Portal.Core.Content.Entities.Common;

namespace Portal.Data.Configurations
{
    internal sealed class EntityConfig<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public EntityConfig()
        {
            ToTable($"Ip{typeof(TEntity).Name}s");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256); // for index

            Property(t => t.Name)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute($"IX_Name_{typeof(TEntity).Name}") {IsUnique = true}));
        }
    }
}
