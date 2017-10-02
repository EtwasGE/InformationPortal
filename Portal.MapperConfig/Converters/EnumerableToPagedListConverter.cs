using System.Collections.Generic;
using AutoMapper;
using MvcPaging;

namespace Portal.MapperConfig.Converters
{
    public class EnumerableToPagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            var sourceList = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            var pagedList = new PagedList<TDestination>(sourceList, source.PageIndex, source.PageSize, source.TotalItemCount);
            return pagedList;
        }
    }

    
}
