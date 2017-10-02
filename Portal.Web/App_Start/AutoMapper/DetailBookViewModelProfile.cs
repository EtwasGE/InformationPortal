using System.Configuration;
using System.Linq;
using Abp.AutoMapper;
using AutoMapper;
using Portal.Application.Content.Books.Dto;
using Portal.Core.Cache.Book;
using Portal.MapperConfig.Resolvers;
using Portal.Web.Models.Content;
using Portal.Web.Models.Content.Books;

namespace Portal.Web.AutoMapper
{
    public class DetailBookViewModelProfile : Profile
    {
        public DetailBookViewModelProfile()
        {
            #region BookCacheItem To FullBookViewModel, ContentInfoViewModel and ControlPanelViewModel

            CreateMap<EntityItem, ActionItem>()
                .ConvertUsing<EntityItemToActionItemConverter>();

            var tagsLimit = int.Parse(ConfigurationManager.AppSettings["TagsLimit"]);

            CreateMap<BookCacheItem, FullBookViewModel>()
                .ForMember(x => x.ContentInfo,
                    opt => opt.MapFrom(input => input.MapTo<ContentInfoViewModel>()))
                .ForMember(x => x.ControlPanel,
                    opt => opt.MapFrom(input => input.MapTo<ControlPanelViewModel>()))
                .ForMember(x => x.Authors,
                    opt => opt.MapFrom(input => input.Authors.OrderByDescending(x => x.ContentsCount)))
                .ForMember(x => x.Tags,
                    opt => opt.MapFrom(input => input.Tags.OrderByDescending(x => x.ContentsCount)
                        .Take(tagsLimit)));

            CreateMap<BookCacheItem, ContentInfoViewModel>()
                .ForMember(x => x.CreationTime,
                    opt => opt.ResolveUsing(input => input.CreationTime.ToShortDateString()));

            CreateMap<BookCacheItem, ControlPanelViewModel>()
                .ForMember(x => x.Approve,
                    opt => opt.ResolveUsing<ApproveResolver>())
                .ForMember(x => x.Delete,
                    opt => opt.ResolveUsing<DeleteResolver>())
                .ForMember(x => x.Favorite,
                    opt => opt.ResolveUsing<FavoriteBookResolver>())
                .ForMember(x => x.TextForClipboard,
                    opt => opt.ResolveUsing<TextForClipboardBookResolver>());

            #endregion

            #region BookOutput To DetailBookViewModel

            var defaultPicture = ConfigurationManager.AppSettings["DefaultImg"];

            CreateMap<BookOutput, DetailBookViewModel>()
                .ForMember(x => x.Book,
                    opt => opt.MapFrom(input => input.Book.MapTo<FullBookViewModel>()))
                .ForMember(x => x.IsShownSimilarResult,
                    opt => opt.ResolveUsing(input => input.SimilarBooks.Any()))
                .ForMember(x => x.DefaultPicture,
                    opt => opt.UseValue(defaultPicture));

            #endregion
        }
    }
}