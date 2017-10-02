using System;
using System.IO;
using Abp.Reflection.Extensions;

namespace Portal.Core
{
    /// <summary>
    /// Central point for application version.
    /// </summary>
    public class VersionHelper
    {
        /// <summary>
        /// Gets current version of the application.
        /// It's also shown in the web page.
        /// </summary>
        public const string Version = "1.0.0.0";

        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate 
            => new FileInfo(typeof(VersionHelper).GetAssembly().Location).LastWriteTime;
    }
}
