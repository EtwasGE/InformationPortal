using AutoMapper;

namespace Portal.MapperConfig.Converters
{
    public class ImageToBase64StringConverter : ITypeConverter<byte[], string>
    {
        public string Convert(byte[] source, string destination, ResolutionContext context)
        {
            if (source == null || source.Length == 0)
            {
                return string.Empty;
            }

            return $"data:image/jpeg;base64,{System.Convert.ToBase64String(source)}";
        }
    }
}
