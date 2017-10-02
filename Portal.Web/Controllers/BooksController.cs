using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Web.Mvc.Authorization;
using MvcPaging;
using Nest;
using Portal.Application.Content.Books;
using Portal.Application.Content.Books.Dto;
using Portal.Application.Search;
using Portal.Core.Authorization;
using Portal.Core.Content;
using Portal.Core.DataFilters;
using Portal.Core.ElasticSearch;
using Portal.Web.Controllers.Common;
using Portal.Web.Helpers;
using Portal.Web.Models.Content;
using Portal.Web.Models.Content.Books;
using SearchInput = Portal.Application.Search.Dto.SearchInput;

namespace Portal.Web.Controllers
{
    [AbpMvcAuthorize]
    [RoutePrefix("books")]
    public class BooksController : ContentControllerBase
    {
        private readonly IBookAppService _bookservice;
        private readonly ISearchAppService<BookIndexItem> _searchservice;

        public BooksController(
            IBookAppService bookservice,
            ISearchAppService<BookIndexItem> searchservice
            )
        {
            _bookservice = bookservice;
            _searchservice = searchservice;
        }
        
        // выбор всех книг
        [Route("")]
        [Route("all")]
        [Route("all/page{page?}")]
        public async Task<ActionResult> All(int? page)
        {
            var input = GetAllBookInput(page);
            var output = await _bookservice.GetAllAsync(input);
            return PreViewActionResult(output);
        }

        // выбор избранных книг
        [Route("favorites")]
        [Route("favorites/page{page?}")]
        public async Task<ActionResult> Favorites(int? page)
        {
            var input = GetAllBookInput(page);
            var output = await _bookservice.GetAllFavoritesByCurrentUserIdAsync(input);
            return PreViewActionResult(output);
        }

        // выбор книг по каталогу
        [Route("catalog/id{catalogId}")]
        [Route("catalog/id{catalogId}/page{page?}")]
        public async Task<ActionResult> Catalog(int catalogId, int? page)
        {
            var input = GetAllBookInput(catalogId, page);
            var output = await _bookservice.GetAllByCatalogIdAsync(input);
            return PreViewActionResult(output);
        }

        // выборка книг по автору
        [Route("author/id{authorId}")]
        [Route("author/id{authorId}/page{page?}")]
        public async Task<ActionResult> Author(int authorId, int? page)
        {
            var input = GetAllBookInput(authorId, page);
            var output = await _bookservice.GetAllByAuthorIdAsync(input);
            return PreViewActionResult(output);
        }

        // выборка книг по тегу
        [Route("tag/id{tagId}")]
        [Route("tag/id{tagId}/page{page?}")]
        public async Task<ActionResult> Tag(int tagId, int? page)
        {
            var input = GetAllBookInput(tagId, page);
            var output = await _bookservice.GetAllByTagIdAsync(input);
            return PreViewActionResult(output);
        }

        // детальный обзор книги
        [Route("detail/id{bookId}")]
        public async Task<ActionResult> Detail(int bookId)
        {
            var input = new BookInput
            {
                Id = bookId,
                PageSize = GetDefaultPageSize()
            };

            if (await IsGrantedAsync(PermissionNames.ContentChange))
            {
                CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete, FilterNames.Approved);
            }

            var output = await _bookservice.GetByIdAsync(input);
            var model = output.MapTo<DetailBookViewModel>();

            if (Request.IsAjaxRequest())
                return PartialView("_Detail", model);
            return View("Detail", model);
        }

        // поиск книг
        [HttpPost]
        [Route("search")]
        [Route("search/{query}/page{page?}")]
        public async Task<PartialViewResult> Search(string query, int? page)
        {
            var input = new SearchInput
            {
                Query = query,
                PageIndex = page - 1 ?? 0,
                PageSize = GetDefaultPageSize()
            };

            var output = await _searchservice.SearchAsync(input);
            var results = new PagedList<IHit<BookIndexItem>>(output.Hits, 
                input.PageIndex, input.PageSize, (int) output.Total);

            var model = new SearchBookViewModel
            {
                Query = query,
                Took = output.Took,
                Results = results,
                Suggestions = output.Suggest,
                IsShownResult = output.Hits.Any(),
                IsShownSuggest = output.Suggest.Values.Any(x => x.Any(y => y.Options.Any())),
                IsShownPagination = results.PageCount > 1
            };

            return PartialView("_SearchResult", model);
        }

        // получить файл
        [Route("file/id{contentId}")]
        public async Task<ActionResult> File(int contentId)
        {
            if (await IsGrantedAsync(PermissionNames.ContentChange))
            {
                CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete, FilterNames.Approved);
            }

            var file = await _bookservice.GetFileAsync(contentId);
            await _bookservice.AddViewerAsync(contentId);
            return file;
        }

        // изменить сортировку книг
        [HttpPost]
        [Route("sorting/{sortType}")]
        public ActionResult Sorting(SortType sortType)
        {
            CookieHelper.SortType = sortType;
            return RedirectToPreviousAction();
        }

        [HttpPost]
        [Route("descending/{isDescending}")]
        public ActionResult Descending(bool isDescending)
        {
            CookieHelper.OrderIsDescending = isDescending;
            return RedirectToPreviousAction();
        }

        // добавить / удалить книгу из избранного
        [HttpPost]
        [Route("favorite/id{contentId}")]
        public async Task<ActionResult> Favorite(int contentId)
        {
            var model = await _bookservice.ChangeFavoriteAsync(contentId);
            return PartialView("~/Views/Shared/Content/_Favorite.cshtml", model);
        }

        #region Permissions

        // удаленные книги
        [AbpMvcAuthorize(PermissionNames.ContentChange)]
        [Route("alldeleted")]
        [Route("alldeleted/page{page?}")]
        public async Task<ActionResult> AllDeleted(int? page)
        {
            var input = GetAllBookInput(page);
            var output = await _bookservice.GetAllDeletedAsync(input);
            return PreViewActionResult(output);
        }

        // неутвержденные книги
        [AbpMvcAuthorize(PermissionNames.ContentChange)]
        [Route("allnotapproved")]
        [Route("allnotapproved/page{page?}")]
        public async Task<ActionResult> AllNotApproved(int? page)
        {
            var input = GetAllBookInput(page);
            var output = await _bookservice.GetAllNotApprovedAsync(input);
            return PreViewActionResult(output);
        }

        // утвердить книгу
        [AbpMvcAuthorize(PermissionNames.ContentChange)]
        [HttpPost]
        [Route("approve/id{contentId}")]
        public async Task<ActionResult> Approve(int contentId)
        {
            var model = await _bookservice.ApproveAsync(contentId);
            return PartialView("~/Views/Shared/Content/_Approve.cshtml", model);
        }

        // удалить/восстановить книгу
        [AbpMvcAuthorize(PermissionNames.ContentChange)]
        [HttpPost]
        [Route("delete/id{contentId}")]
        public async Task<ActionResult> Delete(int contentId)
        {
            var model = await _bookservice.DeleteOrUnDeleteAsync(contentId);
            return PartialView("~/Views/Shared/Content/_Delete.cshtml", model);
        }

        #endregion

        #region ChildActionOnly

        [ChildActionOnly]
        public ActionResult SortingPanel()
        {
            var currentSort = CookieHelper.SortType;

            var sortings = new List<SortItem>
            {
                new SortItem(SortType.Name, L("SortHeaderName"),
                    L("SortTitleName"), SortType.Name == currentSort),

                new SortItem(SortType.Author, L("SortHeaderAuthor"),
                    L("SortTitleAuthor"), SortType.Author == currentSort),

                new SortItem(SortType.Date, L("SortHeaderDate"),
                    L("SortTitleDate"), SortType.Date == currentSort),

                new SortItem(SortType.View, L("SortHeaderView"),
                    L("SortTitleView"), SortType.View == currentSort),

                new SortItem(SortType.Favorite, L("SortHeaderFavorite"),
                    L("SortTitleFavorite"), SortType.Favorite == currentSort),
            };

            var model = new SortingViewModel
            {
                Sortings = sortings,
                IsDescending = CookieHelper.OrderIsDescending
            };

            return PartialView("~/Views/Shared/Content/_SortingPanel.cshtml", model);
        }

        #endregion

        #region Private Methods

        private ActionResult PreViewActionResult(AllBookOutput output)
        {
            var model = output.MapTo<PreViewBookViewModel>();

            if (Request.IsAjaxRequest())
                return PartialView("_Pre", model);
            return View("Pre", model);
        }

        private ActionResult RedirectToPreviousAction()
        {
            // TODO: check
            var action = SessionHelper.RouteData.Values["action"]?.ToString();
            var routeValues = SessionHelper.RouteValueDictionary;
            return RedirectToAction(action, routeValues);
        }

        private AllBookInput<T> GetAllBookInput<T>(T id, int? page)
        {
            var input = GetAllBookInput(page).MapTo<AllBookInput<T>>();
            input.Id = id;
            return input;
        }

        private AllBookInput GetAllBookInput(int? page)
        {
            var input = new AllBookInput
            {
                PageIndex = page - 1 ?? 0,
                PageSize = GetDefaultPageSize(),
                SortType = CookieHelper.SortType,
                OrderIsDescending = CookieHelper.OrderIsDescending
            };
            return input;
        }

        private int GetDefaultPageSize()
        {
            var width = CookieHelper.ScreenWidth;
            const int defaultPageSize = 3;

            if (width == default(int))
            {
                return defaultPageSize * 4;
            }

            if (width < 768)
            {
                // 1 column
                return defaultPageSize;
            }

            if (width >= 768 && width < 992)
            {
                // 2 columns
                return defaultPageSize * 2;
            }

            if (width >= 992 && width < 1900)
            {
                // 4 columns
                return defaultPageSize * 4;
            }

            // 6 columns
            return defaultPageSize * 6;
        }

        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "GET" && !filterContext.IsChildAction)
            {
                CookieHelper.ContentType = ContentType.Books;
                SessionHelper.RouteData = filterContext.RouteData;
                SessionHelper.RouteValueDictionary = new RouteValueDictionary(filterContext.ActionParameters);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}