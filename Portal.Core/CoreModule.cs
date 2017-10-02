using System.Reflection;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero.Configuration;
using Portal.Core.Authorization;
using Portal.Core.Authorization.Roles;
using Portal.Core.Authorization.Users;
using Portal.Core.Cache.Catalog;
using Portal.Core.Configuration;
using Portal.Core.Content.Entities;
using Portal.Core.MultiTenancy;

#if WITHOUTLDAP
using Abp.Zero;
#else
using Abp.Zero.Ldap;
using Abp.Zero.Ldap.Configuration;
#endif

namespace Portal.Core
{
#if WITHOUTLDAP
    [DependsOn(typeof(AbpZeroCoreModule))]
#else
    [DependsOn(typeof(AbpZeroLdapModule))]
#endif
    public class CoreModule : AbpModule
    {
        public override void PreInitialize()
        {
#if !WITHOUTLDAP
            Configuration.Modules.ZeroLdap().Enable(typeof(LdapAuthenticationSource));
#endif
            Configuration.Auditing.IsEnabledForAnonymousUsers = false;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CoreConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Portal.Core.Localization.Source"
                        )
                    )
                );

            RoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<ICatalogCache, CatalogCache<BookCatalog>>(DependencyLifeStyle.Transient);
            IocManager.Register<ICatalogCache, CatalogCache<TrainingCatalog>>(DependencyLifeStyle.Transient);
        }
    }
}
