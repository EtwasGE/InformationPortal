using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Entities
{
    public class Training : ContentEntityBase<Training, TrainingCatalog, TrainingViewer>
    {
        /// <summary>
        /// Компания создавшая тренинг.
        /// </summary>
        public virtual Company Company { get; set; }
    }
}
