using AutoMapper;
using Portal.Core.Authorization.Users;

namespace Portal.MapperConfig.Converters
{
    public class UserToStringConverter : ITypeConverter<User, string>
    {
        public string Convert(User source, string destination, ResolutionContext context)
        {
            return source?.UserName ?? string.Empty;
        }
    }
}
