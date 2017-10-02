using System;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Portal.Core.Content;
using Portal.Web.Controllers.Common;
using Portal.Web.Helpers;

namespace Portal.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AppControllerBase
    {
        public ActionResult Index()
        {
            switch (CookieHelper.ContentType)
            {
                case ContentType.Books:
                    return RedirectToAction("All", "Books");
                case ContentType.Trainings:
                    return RedirectToAction("All", "Trainings");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
	}
}