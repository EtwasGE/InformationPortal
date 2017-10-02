using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services;
using Portal.Application.Content.Books.Dto;

namespace Portal.Application.Content.Books
{
    public interface IBookAppService : IApplicationService
    {
        Task<BookOutput> GetByIdAsync(BookInput input);
        
        Task<AllBookOutput> GetAllAsync(AllBookInput input);
        Task<AllBookOutput> GetAllFavoritesByCurrentUserIdAsync(AllBookInput input);
        Task<AllBookOutput> GetAllByCatalogIdAsync(AllBookInput<int> input);
        Task<AllBookOutput> GetAllByAuthorIdAsync(AllBookInput<int> input);
        Task<AllBookOutput> GetAllByTagIdAsync(AllBookInput<int> input);
        Task<AllBookOutput> GetAllDeletedAsync(AllBookInput input);
        Task<AllBookOutput> GetAllNotApprovedAsync(AllBookInput input);

        Task<FavoriteDto> ChangeFavoriteAsync(int bookId);
        Task<FileStreamResult> GetFileAsync(int bookId);
        Task AddViewerAsync(int bookId);
        
        Task<ApproveDto> ApproveAsync(int bookId);
        Task<DeleteDto> DeleteOrUnDeleteAsync(int bookId);
    }
}