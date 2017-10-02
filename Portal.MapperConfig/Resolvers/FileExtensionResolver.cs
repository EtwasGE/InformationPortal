using System.IO;
using AutoMapper;
using Portal.Core.Content.Entities.Common;

namespace Portal.MapperConfig.Resolvers
{
    public class FileExtensionResolver : IValueResolver<ContentEntityBase, object, string>
    {
        public string Resolve(ContentEntityBase source, object destination, string destMember, ResolutionContext context)
        {
            return Path.GetExtension(source.FilePath)?.Replace(".", "").ToUpper();
        }
    }
}
