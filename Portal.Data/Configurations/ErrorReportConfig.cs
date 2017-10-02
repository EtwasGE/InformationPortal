using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Portal.Core.Content.Entities.Common;

namespace Portal.Data.Configurations
{
    internal sealed class ErrorReportConfig<TContent, TErrorReport> : EntityTypeConfiguration<TErrorReport>
        where TContent : ContentEntityBase
        where TErrorReport : ErrorReportBase<TContent>
    {
        public ErrorReportConfig()
        {
            ToTable($"Ip{typeof(TContent).Name}ErrorReports");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Comment)
                .IsRequired();

            HasRequired(x => x.Content)
                .WithMany()
                .HasForeignKey(x => x.ContentId);
        }
    }
}
