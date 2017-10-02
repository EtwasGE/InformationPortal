using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Abp.Zero.Ldap.Configuration;
using Portal.Core;

namespace Portal.Data.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly PortalDbContext _context;

        public DefaultSettingsCreator(PortalDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, CoreConsts.DefaultFromAddress);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, CoreConsts.DefaultFromDisplayName);

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, CoreConsts.DefaultLanguage);

            //Ldap
            AddSettingIfNotExists(LdapSettingNames.IsEnabled, "false");
            AddSettingIfNotExists(LdapSettingNames.IsEnabled, "true", 1);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}