using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace SmartStore.Core
{
    public static class HttpExtensions
    {
        /// <summary>
        /// Gets a value which indicates whether the HTTP connection uses secure sockets (HTTPS protocol).
        /// Works with Cloud's load balancers.
        /// </summary>
        public static bool IsHttps(this HttpRequest request)
        {
            if (request.IsHttps)
                return true;

            // Handle load balancer scenarios
            var forwardedProto = request.Headers["X-Forwarded-Proto"].FirstOrDefault();
            if (string.Equals(forwardedProto, "https", StringComparison.OrdinalIgnoreCase))
                return true;

            var forwardedSsl = request.Headers["X-Forwarded-Ssl"].FirstOrDefault();
            if (string.Equals(forwardedSsl, "on", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether the specified URL is local to the application.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the URL is local; otherwise, false.</returns>
        public static bool IsAppLocalUrl(this HttpRequest request, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (url.StartsWith("~/"))
                return true;

            if (url.StartsWith("//") || url.StartsWith("http://") || url.StartsWith("https://"))
                return false;

            return Uri.IsWellFormedUriString(url, UriKind.Relative);
        }

        /// <summary>
        /// Gets the raw URL of the request including query string.
        /// </summary>
        public static string GetRawUrl(this HttpRequest request)
        {
            return request.GetEncodedUrl();
        }

        /// <summary>
        /// Gets the display URL of the request.
        /// </summary>
        public static string GetDisplayUrl(this HttpRequest request)
        {
            return request.GetDisplayUrl();
        }

        /// <summary>
        /// Determines if the current request is an AJAX request.
        /// </summary>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                return false;

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// Gets the user's IP address from the request, handling proxy scenarios.
        /// </summary>
        public static string GetUserIpAddress(this HttpRequest request)
        {
            // Check for forwarded IP first (proxy scenarios)
            var forwardedFor = request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                var ips = forwardedFor.Split(',');
                if (ips.Length > 0)
                {
                    return ips[0].Trim();
                }
            }

            var realIp = request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            // Fall back to connection remote IP
            return request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
        }

        /// <summary>
        /// Gets the user agent string from the request.
        /// </summary>
        public static string GetUserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;
        }

        /// <summary>
        /// Determines if the request is from a mobile device.
        /// </summary>
        public static bool IsMobileDevice(this HttpRequest request)
        {
            var userAgent = request.GetUserAgent().ToLowerInvariant();
            
            var mobileKeywords = new[]
            {
                "mobile", "android", "iphone", "ipad", "ipod", "blackberry", 
                "windows phone", "palm", "symbian", "opera mini", "opera mobi"
            };

            return mobileKeywords.Any(keyword => userAgent.Contains(keyword));
        }
    }
}
