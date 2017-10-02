using Abp.Web.Mvc.Views;
using Portal.Core;

namespace Portal.Web.Views
{
    public abstract class WebViewPageBase : WebViewPageBase<dynamic>
    {
    }

    public abstract class WebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected WebViewPageBase()
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
        }
    }
}