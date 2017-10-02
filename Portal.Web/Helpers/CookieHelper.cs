using System;
using System.Configuration;
using System.Web;
using Portal.Core.Content;

namespace Portal.Web.Helpers
{
    public static class CookieHelper
    {
        private static readonly HttpContext HttpContext;
        private static readonly string NameCookieContentType;
        private static readonly string NameCookieScreenWidth;
        private static readonly string NameCookieSorting;
        private static readonly string NameCookieSortingDescending;

        private static SortType? _sortType;
        private static ContentType? _contentType;
        private static bool? _isDescending;
        private static int? _screenWidth;

        static CookieHelper()
        {
            HttpContext = HttpContext.Current;
            NameCookieContentType = ConfigurationManager.AppSettings["NameCookieContentType"];
            NameCookieScreenWidth = ConfigurationManager.AppSettings["NameCookieScreenWidth"];

            switch (ContentType)
            {
                case ContentType.Books:
                    NameCookieSorting = ConfigurationManager.AppSettings["NameCookieSortingBook"];
                    NameCookieSortingDescending = ConfigurationManager.AppSettings["NameCookieSortingBookDescending"];
                    break;
                case ContentType.Trainings:
                    NameCookieSorting = ConfigurationManager.AppSettings["NameCookieSortingTraining"];
                    NameCookieSortingDescending = ConfigurationManager.AppSettings["NameCookieSortingTrainingDescending"];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static SortType SortType
        {
            get
            {
                if (_sortType == null)
                {
                    _sortType = GetCookie<SortType>(NameCookieSorting);
                }

                return _sortType.Value;
            }

            set
            {
                SetCookie(value, NameCookieSorting);
                _sortType = value;
            }
        }

        public static ContentType ContentType
        {
            get
            {
                if (_contentType == null)
                {
                    _contentType = GetCookie<ContentType>(NameCookieContentType);
                }

                return _contentType.Value;
            }

            set
            {
                SetCookie(value, NameCookieContentType);
                _contentType = value;
            }
        }

        public static bool OrderIsDescending
        {
            get
            {
                if (_isDescending == null)
                {
                    _isDescending = GetCookie<bool>(NameCookieSortingDescending);
                }

                return _isDescending.Value;
            }
            set
            {
                SetCookie(value, NameCookieSortingDescending);
                _isDescending = value;
            }
        }

        public static int ScreenWidth
        {
            get
            {
                if (_screenWidth == null || _screenWidth == 0)
                {
                    _screenWidth = GetCookie<int>(NameCookieScreenWidth);
                }

                return _screenWidth.Value;
            }

            set
            {
                SetCookie(value, NameCookieScreenWidth);
                _screenWidth = value;
            }
        }

        private static T GetCookie<T>(string nameCookie) where T : struct
        {
            var type = typeof(T);
            var cookie = HttpContext.Request.Cookies[nameCookie];

            if (string.IsNullOrEmpty(cookie?.Value))
            {
                return default(T);
            }

            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, cookie.Value);
            }

            if (type == typeof(int) || type == typeof(bool))
            {
                var changeType = Convert.ChangeType(cookie.Value, type);
                if (changeType != null)
                {
                    return (T)changeType;
                }
            }

            throw new ArgumentException();
        }

        private static void SetCookie<T>(T value, string nameCookie)
        {
            var cookie = HttpContext.Request.Cookies[nameCookie];

            if (cookie == null)
            {
                HttpContext.Response.Cookies.Add(new HttpCookie(nameCookie, value.ToString()));
            }
            else
            {
                cookie.Value = value.ToString();
                HttpContext.Response.SetCookie(cookie);
            }
        }
    }
}
