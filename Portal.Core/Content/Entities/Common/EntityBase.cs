using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Portal.Core.Content.Entities.Common
{
    public abstract class EntityBase : Entity,  ISoftDelete
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Используется для обозначения сущности как 'Deleted'. 
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    public abstract class EntityBase<TContent> : EntityBase
        where TContent : ContentEntityBase
    {
        /// <summary>
        /// Коллекция сущности <typeparam name="TContent"></typeparam>.
        /// </summary>
        public virtual ICollection<TContent> Contents { get; set; }
    }
}
