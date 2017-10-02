using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Portal.Core.Content.Entities.Common;

namespace Portal.Data.Configurations
{
    internal sealed class ViewerConfig<TViewer> : EntityTypeConfiguration<TViewer>
        where TViewer : ViewerBase
    {
        public ViewerConfig()
        {
            ToTable($"Ip{typeof(TViewer).Name}s");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}