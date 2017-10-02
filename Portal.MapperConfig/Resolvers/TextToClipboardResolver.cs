using System;
using System.Linq;
using AutoMapper;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;

namespace Portal.MapperConfig.Resolvers
{
    public class TextForClipboardBookResolver : IValueResolver<object, object, string>
    {
        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            var bookCacheItem = source as BookCacheItem;
            if (bookCacheItem != null)
            {
                var authors = bookCacheItem.Authors.Select(x => x.Name).ToList();
                return bookCacheItem.Title + Environment.NewLine +
                       (authors.Any()
                           ? MapperHelper.ListToString(authors) + Environment.NewLine
                           : string.Empty) +
                       MapperHelper.GetUrlAction("Detail", "Books", new {bookId = bookCacheItem.Id});
            }

            var shortBookDto = source as ShortBookDto;
            if (shortBookDto != null)
            {
                return shortBookDto.Title + Environment.NewLine +
                       (shortBookDto.Authors != null
                           ? shortBookDto.Authors + Environment.NewLine
                           : string.Empty) +
                       MapperHelper.GetUrlAction("Detail", "Books", new {bookId = shortBookDto.Id});
            }

            throw new ArgumentOutOfRangeException();
        } 
    }
}
