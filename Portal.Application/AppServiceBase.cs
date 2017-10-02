using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using Portal.Core;
using Portal.Core.Authorization.Users;
using Portal.Core.MultiTenancy;

namespace Portal.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }
        public UserManager UserManager { get; set; }

        private User _currentUser;
        public User CurrentUser 
            => _currentUser ?? (_currentUser = UserManager.FindById(AbpSession.GetUserId()));

        protected AppServiceBase()
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
        }
        
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}