using System.Collections.Generic;
using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;
using Portal.Core.Content.Entities;

namespace Portal.Core.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public string MiddleName { get; set; }
        public override string FullName => $"{Surname} {Name} {MiddleName}";

        public virtual ICollection<Book> FavouriteBooks { get; set; }
        public virtual ICollection<Training> FavouriteTrainings { get; set; }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string userName ,string password)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = userName,
                Name = userName,
                Surname = userName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };

            return user;
        }
    }
}