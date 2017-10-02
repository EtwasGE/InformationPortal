using Abp.Application.Navigation;
using Abp.Localization;
using Portal.Core;
using Portal.Core.Authorization;

namespace Portal.Web
{
    public class AppNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Books,
                        L("Books"),
                        url: "Books",
                        icon: "",
                        requiresAuthentication: true
                    ));
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Trainings,
                //        L("Trainings"),
                //        url: "Trainings",
                //        icon: "",
                //        requiresAuthentication: true
                //    ));

            // here Url this is ActionName and CustomObject this is ControllerName 
            var bookCatalog = new MenuDefinition("BookCatalogs", L("BookCatalogs"), "Books")
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.AllBooks,
                        L("AllBooks"),
                        url: "All",
                        icon: "fa fa-book",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.FavoriteBooks,
                        L("FavoriteBooks"),
                        url: "Favorites",
                        icon: "fa fa-star",
                        requiresAuthentication: true
                    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.AddBook,
                //        L("AddBook"),
                //        url: "Add",
                //        icon: "fa fa-plus-square",
                //        requiredPermissionName: PermissionNames.ContentAdd
                //    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.AllNotApprovedBooks,
                        L("AllNotApprovedBooks"),
                        url: "AllNotApproved",
                        icon: "fa fa-cloud-upload",
                        requiredPermissionName: PermissionNames.ContentChange
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.AllDeletedBooks,
                        L("AllDeletedBooks"),
                        url: "AllDeleted",
                        icon: "fa fa-trash",
                        requiredPermissionName: PermissionNames.ContentChange
                        ));

            context.Manager.Menus.Add("BookCatalogs", bookCatalog);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CoreConsts.LocalizationSourceName);
        }
    }
}
