using AutoMapper;
using MvcPaging;

namespace Portal.MapperConfig.Converters
{
    public class PagedListToPagedListConverter<TModel> : ITypeConverter<IPagedList<TModel>, IPagedList<TModel>>
    {
        public IPagedList<TModel> Convert(IPagedList<TModel> source, IPagedList<TModel> destination, ResolutionContext context)
        {
            return source;
        }
    }
}
