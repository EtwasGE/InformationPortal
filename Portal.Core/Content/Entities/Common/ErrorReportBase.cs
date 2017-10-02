using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Portal.Core.Authorization.Users;

namespace Portal.Core.Content.Entities.Common
{
    public abstract class ErrorReportBase<TContent> : CreationAuditedEntity<int, User>, IPassivable
        where TContent : ContentEntityBase
    {
        /// <summary>
        /// Тема сообщения об ошибке.
        /// </summary>
        public MessageSubjectType Subject { get; set; }

        /// <summary>
        /// Комментарий пользователя.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// True: Проблема открыта.
        /// False: Проблема закрыта.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Внешний Id для связи с Content.
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        /// Контент на который было создано это сообщение об ошибке.
        /// </summary>
        public virtual TContent Content { get; set; }
    }
}
