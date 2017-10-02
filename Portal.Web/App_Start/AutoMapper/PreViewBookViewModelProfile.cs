using System.Configuration;
using System.Linq;
using Abp.AutoMapper;
using AutoMapper;
using MvcPaging;
using Portal.Application.Content.Books.Dto;
using Portal.MapperConfig.Converters;
using Portal.MapperConfig.Resolvers;
using Portal.Web.Models.Content;
using Portal.Web.Models.Content.Books;

namespace Portal.Web.AutoMapper
{
    public class PreViewBookViewModelProfile : Profile
    {
        public PreViewBookViewModelProfile()
        {
            #region ShortBookDto To ShortBookViewModel, ContentInfoViewModel and ControlPanelViewModel

            var titleLimit = int.Parse(ConfigurationManager.AppSettings["TitleLimit"]);

            CreateMap<ShortBookDto, ShortBookViewModel>()
                .ForMember(x => x.Title,
                    opt => opt.ResolveUsing(new TitleLengthResolve(titleLimit)))
                .ForMember(x => x.ContentInfo,
                    opt => opt.MapFrom(input => input.MapTo<ContentInfoViewModel>()))
                .ForMember(x => x.ControlPanel,
                    opt => opt.MapFrom(input => input.MapTo<ControlPanelViewModel>()));

            CreateMap<IPagedList<ShortBookDto>, IPagedList<ShortBookViewModel>>()
                .ConvertUsing<EnumerableToPagedListConverter<ShortBookDto, ShortBookViewModel>>();

            CreateMap<ShortBookDto, ContentInfoViewModel>()
                .ForMember(x => x.CreationTime,
                    opt => opt.ResolveUsing(input => input.CreationTime.ToShortDateString()));

            CreateMap<ShortBookDto, ControlPanelViewModel>()
                .ForMember(x => x.Approve,
                    opt => opt.ResolveUsing<ApproveResolver>())
                .ForMember(x => x.Delete,
                    opt => opt.ResolveUsing<DeleteResolver>())
                .ForMember(x => x.Favorite,
                    opt => opt.ResolveUsing<FavoriteBookResolver>())
                .ForMember(x => x.TextForClipboard,
                    opt => opt.ResolveUsing<TextForClipboardBookResolver>());

            #endregion

            #region AllBookOutput To PreViewBookViewModel
            
            CreateMap<AllBookOutput, PreViewBookViewModel>()
                .ForMember(x => x.IsShownResult,
                    opt => opt.ResolveUsing(input => input.Books.Any()))
                .ForMember(x => x.IsShownPagination,
                    opt => opt.ResolveUsing(input => input.Books.PageCount > 1));

            #endregion
        }
    }
}