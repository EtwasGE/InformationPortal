using System.Collections.Generic;

namespace Portal.Core.Content.Entities.Common
{
    public abstract class CatalogBase<TContent> : EntityBase<TContent>
        where TContent : ContentEntityBase
    {
        /// <summary>
        /// Порядок следования каталога в меню.
        /// </summary>
        public int Order { get; set; }
    }

    public abstract class CatalogBase<TContent, TCatalog> : CatalogBase<TContent>
        where TContent : ContentEntityBase
        where TCatalog : CatalogBase<TContent, TCatalog>
    {
        /// <summary>
        /// Внешний Id для связи с Parent.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родительский каталог.
        /// </summary>
        public virtual TCatalog Parent { get; set; }

        /// <summary>
        /// Коллекция дочерних каталогов.
        /// </summary>
        public virtual ICollection<TCatalog> Childrens { get; set; }
    }
}
