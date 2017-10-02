using System.Web.Mvc;
using System.Web.Routing;
using Abp.Web.Mvc.Authorization;
using Portal.Core.Content;
using Portal.Web.Controllers.Common;
using Portal.Web.Helpers;

namespace Portal.Web.Controllers
{
    [AbpMvcAuthorize]
    [RoutePrefix("trainings")]
    public class TrainingsController : ContentControllerBase
    {
        [Route("")]
        [Route("all")]
        [Route("all/page{page?}")]
        public ActionResult All(int? page)
        {
            return View("Pre");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "GET" && !filterContext.IsChildAction)
            {
                CookieHelper.ContentType = ContentType.Trainings;
                SessionHelper.RouteData = filterContext.RouteData;
                SessionHelper.RouteValueDictionary = new RouteValueDictionary(filterContext.ActionParameters);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}