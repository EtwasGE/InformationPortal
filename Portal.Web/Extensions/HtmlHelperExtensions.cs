using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Portal.Web.Helpers;

namespace Portal.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        // <a ...>
        //    <i class="iconCssClass"></i>
        //    <span class="spanCssClass">linkText</span>
        // </a>
        public static MvcHtmlString ActionLinkIconText(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName, string iconCssClass, string spanCssClass = null, object routeValues = null, object htmlAttributes = null)
        {
            CheckNotNull(linkText, actionName, controllerName, iconCssClass);

            var tagsString = TagBuilderHelper.IconText(linkText, iconCssClass, spanCssClass);
            var actionLink = htmlHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                controllerName: controllerName, routeValues: routeValues, htmlAttributes: htmlAttributes).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagsString));
        }

        public static MvcHtmlString ActionLinkIconText(this HtmlHelper htmlHelper, string linkText, string actionName,
            object routeValues, string iconCssClass, string spanCssClass = null, object htmlAttributes = null)
        {
            CheckNotNull(linkText, actionName, iconCssClass);

            var tagsString = TagBuilderHelper.IconText(linkText, iconCssClass, spanCssClass);
            var actionLink = htmlHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName, 
                routeValues: routeValues, htmlAttributes: htmlAttributes).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagsString));
        }

        // <a ...>
        //    <i class="iconCssClass"></i>
        // </a>
        public static MvcHtmlString ActionLinkIcon(this HtmlHelper htmlHelper, string actionName,
            string controllerName, string iconCssClass, object routeValues = null, object htmlAttributes = null)
        {
            CheckNotNull(actionName, controllerName, iconCssClass);

            var tagString = TagBuilderHelper.Icon(iconCssClass);
            var actionLink = htmlHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                controllerName: controllerName, routeValues: routeValues,
                htmlAttributes: htmlAttributes).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagString));
        }
        
        #region Private Methods
        private static void CheckNotNull(params string[] items)
        {
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item))
                    throw new ArgumentNullException(nameof(item));
            }
        }
        #endregion
    }
}