using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using Portal.Core;

namespace Portal.Web.Controllers.Common
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class AppControllerBase : AbpController
    {
        protected AppControllerBase()
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}