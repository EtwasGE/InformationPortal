using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Portal.Web.Helpers;

namespace Portal.Web.Extensions
{
    public static class AjaxHelperExtensions
    {
        // <a ...>
        //    <i class="iconCssClass"></i>
        //    <span class="spanHeaderCssClass">linkText</span> 
        //    <span class="spanControlCssClass">
        //       <i class="iconControlCssClass"></i>
        //    </span>
        // </a>
        public static MvcHtmlString ActionLinkTree(this AjaxHelper ajaxHelper, string linkText, string actionName,
            string controllerName, object routeValues, AjaxOptions ajaxOptions, string iconCssClass, string iconControlCssClass,
            string spanHeaderCssClass = null, string spanControlCssClass = null)
        {
            CheckNotNull(linkText, actionName, controllerName, iconCssClass, iconControlCssClass);

            var tagsString = TagBuilderHelper.Tree(linkText, iconCssClass, iconControlCssClass, spanHeaderCssClass, spanControlCssClass);
            var actionLink = ajaxHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                controllerName: controllerName, routeValues: routeValues, ajaxOptions: ajaxOptions).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagsString));
        }
        
        // <a ...>
        //    <i class="iconCssClass"></i>
        //    <span class="spanCssClass">linkText</span>
        // </a>
        public static MvcHtmlString ActionLinkIconText(this AjaxHelper ajaxHelper, string linkText, string actionName,
            string controllerName, AjaxOptions ajaxOptions, string iconCssClass, string spanCssClass = null, 
            object routeValues = null, object htmlAttributes = null)
        {
            CheckNotNull(linkText, actionName, controllerName, iconCssClass);

            var tagsString = TagBuilderHelper.IconText(linkText, iconCssClass, spanCssClass);
            var actionLink = ajaxHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                controllerName: controllerName, ajaxOptions: ajaxOptions, routeValues: routeValues,
                htmlAttributes: htmlAttributes).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagsString));
        }
        
        public static MvcHtmlString ActionLinkIconText(this AjaxHelper ajaxHelper, string linkText, string actionName,
            object routeValues, AjaxOptions ajaxOptions, string iconCssClass, string spanCssClass = null,
            object htmlAttributes = null)
        {
            CheckNotNull(linkText, actionName, iconCssClass);

            var tagsString = TagBuilderHelper.IconText(linkText, iconCssClass, spanCssClass);
            var actionLink = ajaxHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                ajaxOptions: ajaxOptions, routeValues: routeValues, htmlAttributes: htmlAttributes).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagsString));
        }

        // <a ...>
        //    <img src="imgUrl" class="imgCssClass"/></img>
        // </a>
        public static MvcHtmlString ActionLinkImg(this AjaxHelper ajaxHelper, string imgUrl, string actionName,
            string controllerName, AjaxOptions ajaxOptions, string imgCssClass ,object routeValues = null, object htmlAttributes = null)
        {
            CheckNotNull(actionName, controllerName);

            var tagString = TagBuilderHelper.Img(imgUrl, imgCssClass);
            var actionLink = ajaxHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                controllerName: controllerName, ajaxOptions: ajaxOptions, routeValues: routeValues,
                htmlAttributes: htmlAttributes).ToHtmlString();

            return new MvcHtmlString(actionLink.Replace(@"[replacetext]", tagString));
        }
        
        // <a ...>
        //    <i class="iconCssClass"></i>
        // </a>
        public static MvcHtmlString ActionLinkIcon(this AjaxHelper ajaxHelper, string actionName,
            string controllerName, AjaxOptions ajaxOptions, string iconCssClass, object routeValues = null, object htmlAttributes = null)
        {
            CheckNotNull(actionName, controllerName, iconCssClass);

            var tagString = TagBuilderHelper.Icon(iconCssClass);
            var actionLink = ajaxHelper.ActionLink(linkText: @"[replacetext]", actionName: actionName,
                controllerName: controllerName, ajaxOptions: ajaxOptions, routeValues: routeValues,
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