using System.Collections.Generic;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Content.Entities
{
    public class Book : ContentEntityBase<Book, BookCatalog, BookViewer>
    {
        /// <summary>
        /// Издательство.
        /// </summary>
        public virtual Publisher Publisher { get; set; } 

        /// <summary>
        /// Версия издания.
        /// </summary>
        public virtual Issue Issue { get; set; }
        
        /// <summary>
        /// Коллекция авторов.
        /// </summary>
        public virtual ICollection<Author> Authors { get; set; }

        /// <summary>
        /// Коллекция тегов.
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }
    }
}