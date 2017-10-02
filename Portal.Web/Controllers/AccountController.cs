using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.UI;
using Abp.Web.Models;
using Portal.Web.Models;
using Portal.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Portal.Core.Authorization;
using Portal.Core.Authorization.Users;
using Portal.Core.MultiTenancy;
using Portal.Web.Controllers.Common;

namespace Portal.Web.Controllers
{
    public class AccountController : AppControllerBase
    {
        private readonly UserManager _userManager;
        private readonly LogInManager _logInManager;
        private readonly ILanguageManager _languageManager;
        private readonly ITenantCache _tenantCache;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(
            UserManager userManager,
            LogInManager logInManager,
            ILanguageManager languageManager, 
            ITenantCache tenantCache, 
            IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _logInManager = logInManager;
            _languageManager = languageManager;
            _tenantCache = tenantCache;
            _authenticationManager = authenticationManager;
        }

        #region Login / Logout

        public ActionResult Login(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return View(
                new LoginFormViewModel
                {
                    ReturnUrl = returnUrl
                });
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();

            var loginResult = await GetLoginResultAsync(
                loginModel.Username,
                loginModel.Password,
                GetTenancyNameOrNull()
                );

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string username, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(username, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, username, tenancyName);
            }
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string username, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", username));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "UserEmailIsNotConfirmedAndCanNotLogin");
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        #endregion
        
        #region Common private methods
        
        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        #endregion

        #region Common Partial Views

        [ChildActionOnly]
        public PartialViewResult _AccountLanguages()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
                    .Where(l => !l.IsDisabled)
                    .ToList(),
                CurrentUrl = Request.Path
            };

            return PartialView("_AccountLanguages", model);
        }

        #endregion
    }
}