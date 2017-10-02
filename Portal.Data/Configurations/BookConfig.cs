using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Portal.Core.Content.Entities;

namespace Portal.Data.Configurations
{
    internal sealed class BookConfig : EntityTypeConfiguration<Book>
    {
        public BookConfig()
        {
            ToTable("IpBooks");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Title).IsRequired();
            Property(x => x.Description).IsRequired();
            Property(x => x.FilePath).IsRequired();

            Property(t => t.ViewersCount)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute("IX_ViewersCount_Book") { IsUnique = false }));

            HasOptional(x => x.Issue)
                .WithMany()
                .Map(m => m.MapKey("IssueId"));

            HasOptional(x => x.Publisher)
                .WithMany()
                .Map(m => m.MapKey("PublisherId"));

            HasOptional(x => x.Language)
                .WithMany()
                .Map(m => m.MapKey("LanguageId"));
            
            HasMany(x => x.Viewers)
                .WithRequired()
                .Map(m => m.MapKey("BookId"));

            //HasMany(x => x.Authors)
            //    .WithMany(x => x.Contents)
            //    .Map(x =>
            //    {
            //        x.MapLeftKey("BookId");
            //        x.MapRightKey("AuthorId");
            //        x.ToTable("IpBookJoinAuthor");
            //    });

            //HasMany(x => x.Tags)
            //    .WithMany(x => x.Contents)
            //    .Map(x =>
            //    {
            //        x.MapLeftKey("BookId");
            //        x.MapRightKey("TagId");
            //        x.ToTable("IpBookJoinTag");
            //    });

            HasMany(x => x.FavouriteUsers)
                .WithMany(x => x.FavouriteBooks)
                .Map(x =>
                {
                    x.MapLeftKey("BookId");
                    x.MapRightKey("UserId");
                    x.ToTable("IpBookJoinUser");
                });
        }
    }
}
