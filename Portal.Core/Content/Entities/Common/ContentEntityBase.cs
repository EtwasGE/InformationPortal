using System;
using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using Portal.Core.Authorization.Users;

namespace Portal.Core.Content.Entities.Common
{
    public abstract class ContentEntityBase : FullAuditedAggregateRoot<int, User>, IApproved
    {
        /// <summary>
        /// Название (заголовок).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Язык.
        /// </summary>
        public virtual Language Language { get; set; }

        /// <summary>
        /// Год издания.
        /// </summary>
        public DateTime? DatePublication { get; set; }

        /// <summary>
        /// Картинка обложки.
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// Ссылка на файл.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// True: Данная сущность прошла модерацию.
        /// False: Данная сущность не прошла модерацию.
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// True: Сущность рекомендуема к просмотру (лучшая).
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// Количество пользователей добавивших контент в избранное.
        /// </summary>
        public int FavoritesCount { get; set; }

        /// <summary>
        /// Коллекция пользователей у которых сущность в избранном.
        /// </summary>
        public virtual ICollection<User> FavouriteUsers { get; set; }
    }

    public abstract class ContentEntityBase<TContent, TCatalog> : ContentEntityBase
        where TContent : ContentEntityBase<TContent, TCatalog>
        where TCatalog : CatalogBase<TContent, TCatalog>
    {
        /// <summary>
        /// Внешний Id для связи с Catalog.
        /// </summary>
        public virtual int CatalogId { get; set; }

        /// <summary>
        /// Каталог.
        /// </summary>
        public virtual TCatalog Catalog { get; set; }
    }

    public abstract class ContentEntityBase<TContent, TCatalog, TViewer> : ContentEntityBase<TContent, TCatalog>
        where TContent : ContentEntityBase<TContent, TCatalog, TViewer>
        where TCatalog : CatalogBase<TContent, TCatalog>
        where TViewer : ViewerBase
    {
        /// <summary>
        /// Количество просмотров.
        /// </summary>
        public int ViewersCount { get; set; }

        /// <summary>
        /// Коллекция просмотров.
        /// </summary>
        public virtual ICollection<TViewer> Viewers { get; set; }
    }
}