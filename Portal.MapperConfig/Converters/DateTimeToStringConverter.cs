using System;
using AutoMapper;

namespace Portal.MapperConfig.Converters
{
    public class DateTimeToStringConverter : ITypeConverter<DateTime?, string>
    {
        public string Convert(DateTime? source, string destination, ResolutionContext context)
        {
            return source?.ToString("yyyy") ?? string.Empty;
        }
    }
}
