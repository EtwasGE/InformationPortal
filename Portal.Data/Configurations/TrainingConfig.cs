using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Portal.Core.Content.Entities;

namespace Portal.Data.Configurations
{
    internal sealed class TrainingConfig : EntityTypeConfiguration<Training>
    {
        public TrainingConfig()
        {
            ToTable("IpTrainings");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).IsRequired();
            Property(x => x.Description).IsRequired();
            Property(x => x.FilePath).IsRequired();

            Property(t => t.ViewersCount)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute("IX_ViewersCount_Training") { IsUnique = false }));

            HasOptional(x => x.Company)
                .WithMany()
                .Map(m => m.MapKey("CompanyId"));

            HasMany(x => x.Viewers)
                .WithRequired()
                .Map(m => m.MapKey("TrainingId"));

            HasMany(x => x.FavouriteUsers)
                .WithMany(x => x.FavouriteTrainings)
                .Map(x =>
                {
                    x.MapLeftKey("TrainingId");
                    x.MapRightKey("UserId");
                    x.ToTable("IpTrainingJoinUser");
                });
        }
    }
}
