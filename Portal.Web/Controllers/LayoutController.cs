using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using Portal.Application.Catalogs;
using Portal.Application.Sessions;
using Portal.Web.Controllers.Common;
using Portal.Web.Helpers;
using Portal.Web.Models;
using Portal.Web.Models.Layout;

namespace Portal.Web.Controllers
{
    public class LayoutController : AppControllerBase
    {
        private readonly ICatalogAppService _catalogAppService;
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly ILanguageManager _languageManager;

        public LayoutController(
            ICatalogAppService catalogAppService,
            IUserNavigationManager userNavigationManager,
            ISessionAppService sessionAppService,
            ILanguageManager languageManager)
        {
            _catalogAppService = catalogAppService;
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _languageManager = languageManager;
        }

        // this property is injection (see WebModule)
        public string MainMenuName { get; set; }

        [HttpPost]
        public void UpdateScreenWidth(int value)
        {
            CookieHelper.ScreenWidth = value;
        }

        [ChildActionOnly]
        public PartialViewResult SideBarNav(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                ActiveMenuItemName = activeMenu
            };

            return PartialView("_SideBarNav", model);
        }

        [ChildActionOnly]
        public PartialViewResult SideBarUserArea()
        {
            var model = new SideBarUserAreaViewModel
            {
                LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations())
            };

            return PartialView("_SideBarUserArea", model);
        }
        
        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages()
            };

            return PartialView("_LanguageSelection", model);
        }

        [ChildActionOnly]
        public PartialViewResult LeftSideBar()
        {
            var viewModel = new LeftSideBarViewModel
            {
                Catalogs = _catalogAppService.GetParentCatalogs(),
                Menu = AsyncHelper.RunSync(
                    () => _userNavigationManager.GetMenuAsync(MainMenuName, AbpSession.ToUserIdentifier()))
            };

            return PartialView("_LeftSideBar", viewModel);
        }

        //TODO: add permission
        [ChildActionOnly]
        public PartialViewResult RightSideBar()
        {
            //var themeName = SettingManager.GetSettingValue(AppSettingNames.UiTheme);

            //var viewModel = new RightSideBarViewModel
            //{
            //    CurrentTheme = UiThemes.All.FirstOrDefault(t => t.CssClass == themeName)
            //};

            //return PartialView("_RightSideBar", viewModel);

            return PartialView("_RightSideBar");
        }
    }
}