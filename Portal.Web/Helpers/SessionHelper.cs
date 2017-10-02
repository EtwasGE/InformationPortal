using System.Web;
using System.Web.Routing;

namespace Portal.Web.Helpers
{
    public static class SessionHelper
    {
        public static RouteData RouteData
        {
            get => Get<RouteData>();
            set => Set(value);
        }
        
        public static RouteValueDictionary RouteValueDictionary
        {
            get => Get<RouteValueDictionary>();
            set => Set(value);
        }

        private static T Get<T>()
            where T: class, new()
        {
            var value = HttpContext.Current.Session[typeof(T).Name];
            return value as T;
        }

        private static void Set<T>(T value)
        {
            HttpContext.Current.Session.Add(typeof(T).Name, value);
        }
    }
}
