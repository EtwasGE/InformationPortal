using System.Collections.Generic;
using Abp.Application.Navigation;
using Portal.Application.Catalogs.Dto;

namespace Portal.Web.Models.Layout
{
    public class LeftSideBarViewModel
    {
        public UserMenu Menu { get; set; }
        //public string ActiveMenuItemName { get; set; }
        public IList<CatalogDto> Catalogs { get; set; }
    }
}