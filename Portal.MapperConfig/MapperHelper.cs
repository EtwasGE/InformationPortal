using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Dependency;
using Portal.Core.Content.Entities.Common;

namespace Portal.MapperConfig
{
    public static class MapperHelper
    {
        public static string ListToString(IList<string> items, int? limit = null)
        {
            if (limit == null || limit < 1 || items.Count <= limit)
                return string.Join(", ", items);
            return $"{string.Join(", ", items.Take((int)limit))}";
        }
        
        public static string GetUrlAction(string actionName, string controllerName, object routeValues)
        {
            var urlHelper = IocManager.Instance.Resolve<UrlHelper>();
            var protocol = urlHelper.RequestContext.HttpContext.Request.Url?.Scheme;
            var hostname = urlHelper.RequestContext.HttpContext.Request.Url?.Host;
            var routeValueDictionary = new RouteValueDictionary(routeValues);
            return urlHelper.Action(actionName, controllerName, routeValueDictionary, protocol, hostname);
        }

        public static int GetContentsCount<TContent, TCatalog>(TCatalog catalog)
            where TContent: ContentEntityBase
            where TCatalog : CatalogBase<TContent, TCatalog>
        {
            return catalog.Parent == null
                ? catalog.Contents.Count + catalog.Childrens.Sum(x => x.Contents.Count)
                : catalog.Contents.Count;
        }
    }
}
