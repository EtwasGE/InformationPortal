using System.Linq;
using AutoMapper;
using MvcPaging;
using Portal.Application.Feedback.Dto;
using Portal.MapperConfig.Converters;
using Portal.Web.Models.Feedback;

namespace Portal.Web.AutoMapper
{
    public class PreViewErrorReportViewModelProfile : Profile
    {
        public PreViewErrorReportViewModelProfile()
        {
            #region ErrorReportDto To ErrorReportViewModel
            
            CreateMap<ErrorReportDto, ErrorReportViewModel>()
                .ForMember(x => x.CreationTime,
                    opt => opt.ResolveUsing(input => input.CreationTime.ToShortDateString()));

            CreateMap<IPagedList<ErrorReportDto>, IPagedList<ErrorReportViewModel>>()
                .ConvertUsing<EnumerableToPagedListConverter<ErrorReportDto, ErrorReportViewModel>>();

            #endregion

            #region AllErrorReportOutput To PreViewErrorReportViewModel

            CreateMap<AllErrorReportOutput, PreViewErrorReportViewModel>()
                .ForMember(x => x.IsShownResult,
                    opt => opt.ResolveUsing(input => input.Reports.Any()))
                .ForMember(x => x.IsShownPagination,
                    opt => opt.ResolveUsing(input => input.Reports.PageCount > 1));

            #endregion
        }
    }
}