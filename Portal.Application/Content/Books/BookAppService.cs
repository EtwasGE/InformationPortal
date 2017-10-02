using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using MvcPaging;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Author;
using Portal.Core.Cache.Book;
using Portal.Core.Cache.Catalog;
using Portal.Core.Cache.Tag;
using Portal.Core.Content.Builder;
using Portal.Core.Content.Builder.Common;
using Portal.Core.Content.Entities;
using Portal.Core.DataFilters;

namespace Portal.Application.Content.Books
{
    // TODO: add unit tests
    [AbpAuthorize]
    public class BookAppService : AppServiceBase, IBookAppService
    {
        private readonly IBookCache _bookCache;
        private readonly IRepository<Book> _bookRepository;
        private readonly ICatalogCache _catalogCache;
        private readonly IAuthorCache _authorCache;
        private readonly ITagCache _tagCache;

        public BookAppService(
            IBookCache bookCache,
            IRepository<Book> bookRepository,
            ICatalogCache catalogCache,
            IAuthorCache authorCache,
            ITagCache tagCache)
        {
            _bookCache = bookCache;
            _bookRepository = bookRepository;
            _catalogCache = catalogCache;
            _authorCache = authorCache;
            _tagCache = tagCache;
        }

        public async Task<BookOutput> GetByIdAsync(BookInput input)
        {
            var book = await _bookCache.GetAsync(input.Id);
            if (book == null)
            {
                throw new UserFriendlyException(404, L("PageNotFoundTitle"), L("PageNotFoundDetails"));
            }

            var builder = new BookBySimilar(book, input.PageSize);
            var similarBooks = await GetAll(builder);

            var output = new BookOutput
            {
                Book = book,
                SimilarBooks = similarBooks
            };

            return output;
        }
        
        public async Task<AllBookOutput> GetAllAsync(AllBookInput input)
        {
            var builder = new AllBook();
            var books = await GetAll(input, builder);
            var header = L("AllBookHeader");
            return new AllBookOutput(books, header);
        }

        public async Task<AllBookOutput> GetAllFavoritesByCurrentUserIdAsync(AllBookInput input)
        {
            var builder = new BookByFavorite(AbpSession.GetUserId());
            var books = await GetAll(input, builder);
            var header = L("BookByFavoriteHeader");
            return new AllBookOutput(books, header);
        }

        public async Task<AllBookOutput> GetAllByCatalogIdAsync(AllBookInput<int> input)
        {
            var catalog = await _catalogCache.GetAsync(input.Id);
            if (catalog == null)
            {
                throw new UserFriendlyException(404, L("PageNotFoundTitle"), L("PageNotFoundDetails"));
            }

            var builder = new BookByCatalog(input.Id);
            var books = await GetAll(input, builder);
            var header = L("BookByCatalogHeader", catalog.Name);
            return new AllBookOutput(books, header);
        }

        public async Task<AllBookOutput> GetAllByAuthorIdAsync(AllBookInput<int> input)
        {
            var author = await _authorCache.GetAsync(input.Id);
            if (author == null)
            {
                throw new UserFriendlyException(404, L("PageNotFoundTitle"), L("PageNotFoundDetails"));
            }

            var builder = new BookByAuthor(input.Id);
            var books = await GetAll(input, builder);
            var header = L("BookByAuthorHeader", author.Name);
            return new AllBookOutput(books, header);
        }

        public async Task<AllBookOutput> GetAllByTagIdAsync(AllBookInput<int> input)
        {
            var tag = await _tagCache.GetAsync(input.Id);
            if (tag == null)
            {
                throw new UserFriendlyException(404, L("PageNotFoundTitle"), L("PageNotFoundDetails"));
            }

            var builder = new BookByTag(input.Id);
            var books = await GetAll(input, builder);
            var header = L("BookByTagHeader", tag.Name);
            return new AllBookOutput(books, header);
        }

        public async Task<AllBookOutput> GetAllDeletedAsync(AllBookInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete, FilterNames.Approved))
            {
                var builder = new AllDeletedBook();
                var books = await GetAll(input, builder);
                return new AllBookOutput(books, L("AllDeletedHeader"));
            }
        }

        public async Task<AllBookOutput> GetAllNotApprovedAsync(AllBookInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete, FilterNames.Approved))
            {
                var builder = new AllNotApprovedBook();
                var books = await GetAll(input, builder);
                return new AllBookOutput(books, L("AllNotApprovedHeader"));
            }
        }

        public async Task<FavoriteDto> ChangeFavoriteAsync(int bookId)
        {
            var book = await _bookRepository.GetAsync(bookId);
            if (book == null)
            {
                throw new AbpException("Book not found"); 
            }

            var isFavorite = CurrentUser.FavouriteBooks.Any(x => x.Id == bookId);
            if (isFavorite)
            {
                book.FavouriteUsers.Remove(CurrentUser);
                book.FavoritesCount = book.FavoritesCount - 1;
            }
            else
            {
                book.FavouriteUsers.Add(CurrentUser);
                book.FavoritesCount = book.FavoritesCount + 1;
            }

            return new FavoriteDto
            {
                Id = bookId,
                IsFavorite = !isFavorite
            };
        }

        public async Task<FileStreamResult> GetFileAsync(int bookId)
        {
            var book = await _bookCache.GetAsync(bookId);
            if (book == null)
            {
                throw new UserFriendlyException(404, L("PageNotFoundTitle"), L("PageNotFoundDetails"));
            }

            var uploadPath = ConfigurationManager.AppSettings["PathUploadBooks"];
            var filePath = HttpContext.Current.Server.MapPath(uploadPath + book.FilePath);

            if (!File.Exists(filePath))
            {
                throw new UserFriendlyException(404, L("PageNotFoundTitle"), L("PageNotFoundDetails"));
            }

            // TODO: use using? 
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            FileStreamResult fileStreamResult;

            var contentType = ConfigurationManager.AppSettings[book.FileFormat];
            if (contentType == null)
            {
                fileStreamResult = new FileStreamResult(fileStream, "application/octet-stream")
                {
                    FileDownloadName = book.Title + Path.GetExtension(filePath)
                };
            }
            else
            {
                fileStreamResult = new FileStreamResult(fileStream, contentType);
            }

            return fileStreamResult;
        }

        public async Task AddViewerAsync(int bookId)
        {
            var book = await _bookRepository.GetAsync(bookId);
            if (book == null)
            {
                throw new AbpException("Book not found");
            }

            // здесь необходимо было добавить id пользователя, 
            // чтобы подсчет просмотров выполнялся корректно
            book.Viewers.Add(new BookViewer { CreatorUserId = AbpSession.UserId });

            var results = new List<BookViewer>();

            foreach (var item in book.Viewers)
            {
                if (!results.Any(x => x.CreatorUserId == item.CreatorUserId
                     && x.CreationTime.Date == item.CreationTime.Date))
                {
                    results.Add(item);
                }
            }

            book.ViewersCount = results.Count;
        }
        
        public async Task<ApproveDto> ApproveAsync(int bookId)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete, FilterNames.Approved))
            {
                var book = await _bookRepository.GetAsync(bookId);
                if (book == null)
                {
                    throw new AbpException("Book not found");
                }

                book.IsApproved = true;

                if (book.IsDeleted)
                {
                    await DeleteOrUnDeleteAsync(book.Id);
                }
                
                return new ApproveDto
                {
                    Id = book.Id,
                    IsApproved = book.IsApproved,
                    Title = book.Title
                };
            }
        }

        public async Task<DeleteDto> DeleteOrUnDeleteAsync(int bookId)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete, FilterNames.Approved))
            {
                var book = await _bookRepository.GetAsync(bookId);
                if (book == null)
                {
                    throw new AbpException("Book not found");
                }

                // TODO: логику восстановления книги вынести в отдельный метод репозитория
                var isDeleted = book.IsDeleted;
                if (isDeleted)
                {
                    book.IsDeleted = false;
                    book.DeletionTime = null;
                    book.DeleterUserId = null;
                }
                else
                {
                    await _bookRepository.DeleteAsync(bookId);
                }

                return new DeleteDto
                {
                    Id = book.Id,
                    IsDeleted = !isDeleted,
                    Title = book.Title
                };
            }
        }

        #region Private Methods
        
        private Task<IPagedList<ShortBookDto>> GetAll(AllBookInput input, BuilderSortBase<Book> builder)
        {
            return Task.Run(() =>
            {
                builder
                    .SortingBy(input.SortType)
                    .OrderBy(input.OrderIsDescending);

                var books = Director<Book>
                    .Construct(builder)
                    .ToPagedList(input.PageIndex, input.PageSize)
                    .MapTo<IPagedList<ShortBookDto>>();

                return books;
            });
        }

        private Task<IPagedList<ShortBookDto>> GetAll(AllBookInput input, BuilderBase<Book> builder)
        {
            return Task.Run(() =>
            {
                var books = Director<Book>
                    .Construct(builder)
                    .ToPagedList(input.PageIndex, input.PageSize)
                    .MapTo<IPagedList<ShortBookDto>>();

                return books;
            });
        }

        private Task<IList<ShortBookDto>> GetAll(BuilderBase<Book> builder)
        {
            return Task.Run(() =>
            {
                var books = Director<Book>
                    .Construct(builder)
                    .MapTo<IList<ShortBookDto>>();

                return books;
            });
        }

        #endregion
    }
}