using AutoMapper;
using MvcPaging;
using Portal.Application.Feedback.Dto;
using Portal.Core.Content.Entities;
using Portal.MapperConfig.Converters;

namespace Portal.MapperConfig.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<IPagedList<BookErrorReport>, IPagedList<ErrorReportDto>>()
                .ConvertUsing<EnumerableToPagedListConverter<BookErrorReport, ErrorReportDto>>();

            CreateMap<IPagedList<TrainingErrorReport>, IPagedList<ErrorReportDto>>()
                .ConvertUsing<EnumerableToPagedListConverter<TrainingErrorReport, ErrorReportDto>>();
        }
    }
}
