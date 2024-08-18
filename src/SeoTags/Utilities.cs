using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace SeoTags
{
    /// <summary>
    /// Utilities
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Returns the Display.Name attibute of the item.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="item">The enum item.</param>
        /// <returns>The Display.Name attibute</returns>
        public static string ToDisplay<TEnum>(this TEnum item) where TEnum : struct, Enum
        {
            //var enumType = item.GetType();
            //var name = Enum.GetName(enumType, item);
            //var attribute = enumType.GetMember(name)[0].GetCustomAttribute<DisplayAttribute>();

            var attribute = item.GetType().GetField(item.ToString()).GetCustomAttribute<DisplayAttribute>();
            if (attribute == null)
                return item.ToString();
            return attribute.ResourceType == null ? attribute.Name : attribute.GetName();
        }

        /// <summary>
        /// Ensures the item is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="name">The name of argument.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void EnsureNotNull<T>(this T item, string name, string message = null)
        {
            if (item is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        /// <summary>
        /// Ensures the string is not null or whitespace (or empty).
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="name">The name of argument.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void EnsureNotNullOrWhiteSpace(this string str, string name, string message = null)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException(name, message);
        }

        /// <summary>
        /// Ensures the enumerable has not null item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="name">The name of argument.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void EnsureNotNullItem<T>(this IEnumerable<T> enumerable, string name, string message = "Has null item")
        {
            if (enumerable.Any(p => p is null))
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        /// <summary>
        /// Ensures the enumerable is not null and has not null item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="name">The name of argument.</param>
        public static void EnsureNotNullAndNotNullItem<T>(this IEnumerable<T> enumerable, string name)
        {
            enumerable.EnsureNotNull(name);
            enumerable.EnsureNotNullItem(name);
        }

        /// <summary>
        /// Ensures the Enum value is valid.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enum">The enum.</param>
        /// <param name="name">The name of argument.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void EnsureIsValid<TEnum>(this TEnum @enum, string name, string message = null) where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), @enum) is false)
                throw new ArgumentOutOfRangeException($"{name} : {typeof(TEnum)}", message);
        }

        /// <summary>
        /// Determines whether the specified string is not null or whitespace (or empty).
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>
        ///   <c>true</c> if string is null or whitespace (or empty); otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str) is false;
        }

        /// <summary>
        /// Converts to ISO 8601 string.
        /// </summary>
        /// <param name="dateTimeOffset">The date time offset.</param>
        /// <param name="asUTC">Converts as UTC.</param>
        public static string ToStringIso8601(this DateTimeOffset dateTimeOffset, bool asUTC)
        {
            return (asUTC ? dateTimeOffset.ToUniversalTime() : dateTimeOffset).ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to URI.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A Uri instance</returns>
        public static Uri ToUri(this string url)
        {
            url.EnsureNotNullOrWhiteSpace(nameof(url));
            return new(url);
        }

        /// <summary>
        /// Initialize a new instance of the System.Uri class based on the specified base URI and relative URI string.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="relative">The relative URI to add to the base URI.</param>
        /// <returns>A Uri instance</returns>
        public static Uri Relative(this Uri uri, string relative)
        {
            uri.EnsureNotNull(nameof(uri));
            return new(uri, relative);
        }

        #region Mime Types
        //https://github.com/dotnet/aspnetcore/blob/main/src/Middleware/StaticFiles/src/FileExtensionContentTypeProvider.cs
        //https://stackoverflow.com/questions/1029740/get-mime-type-from-filename-extension
        //https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
        //https://www.freeformatter.com/mime-types-list.html
        //https://www.sitepoint.com/mime-types-complete-list/
        //https://github.com/hey-red/MimeTypesMap
        //https://github.com/khellang/MimeTypes
        //https://github.com/zone117x/MimeMapping
        //https://github.com/samuelneff/MimeTypeMap
        #endregion

        private static readonly FileExtensionContentTypeProvider contentTypeProvider = new();

        /// <summary>
        /// Given a file path, determine the MIME type.
        /// </summary>
        /// <param name="subpath">A file path.</param>
        /// <param name="defaultType">Default type if can not determine.</param>
        /// <returns>The resulting MIME type</returns>
        public static string GetContentType(string subpath, string defaultType = null)
        {
            if (contentTypeProvider.TryGetContentType(subpath, out var contentType))
                return contentType;
            return defaultType ?? throw new InvalidOperationException("Content type not found.");
        }

        /// <summary>
        /// Given a file path, determine the MIME type.
        /// </summary>
        /// <param name="filePath">A file path.</param>
        /// <param name="contentType">The resulting MIME type.</param>
        /// <returns>True if MIME type could be determined.</returns>
        public static bool TryGetContentType(string filePath, out string contentType)
        {
            return contentTypeProvider.TryGetContentType(filePath, out contentType);
        }

        /// <summary>
        /// Converts a string to an HTML-encoded string.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string HtmlEncode(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            return WebUtility.HtmlEncode(str);
        }

        /// <summary>
        /// Escapes non ASCII characters.
        /// </summary>
        /// <param name="str">The string to escape.</param>
        /// <returns>An escaped string</returns>
        public static string EscapeNonAscii(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var builder = new StringBuilder();
            foreach (var @char in str)
            {
                if (@char > 127)
                    builder.Append("\\u").Append(((int)@char).ToString("x4"));
                else
                    builder.Append(@char);
            }
            return builder.ToString();
        }

        #region UrlEncode
        ///// <summary>
        ///// Converts a string into a URL-encoded string.
        ///// </summary>
        ///// <param name="str">The string to URL-encode.</param>
        ///// <returns>A URL-encoded string</returns>
        //public static string UrlEncode(this string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str))
        //        return str;

        //    //Uri.EscapeDataString is better than WebUtility, HttpUtility, and etc.
        //    return Uri.EscapeDataString(str);
        //}

        ///// <summary>
        ///// Converts a URL-encoded string to decoded string.
        ///// </summary>
        ///// <param name="str">The string to URL-decoded.</param>
        ///// <returns>A URL-decoded string</returns>
        //public static string UrlDecode(this string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str))
        //        return str;

        //    //WebUtility.UrlDecode supports decoding all WebUtility, HttpUtility, Uri encoded strings.
        //    return WebUtility.UrlDecode(str);
        //    //return Uri.UnescapeDataString(str);
        //}

        ///// <summary>
        ///// Determines whether specified string is URL-encoded.
        ///// </summary>
        ///// <param name="str">The string to check.</param>
        ///// <returns>
        /////   <c>true</c> if string is URL-encoded; otherwise, <c>false</c>.
        ///// </returns>
        //public static bool IsUrlEncoded(this string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str))
        //        return false;

        //    return UrlDecode(str) != str;
        //}

        ///// <summary>
        ///// Converts a string to URL-encoded string if not encoded before.
        ///// </summary>
        ///// <param name="str">The string to URL-encode.</param>
        ///// <returns>A URL-encoded string</returns>
        //public static string UrlEncodeIfNeeded(this string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str))
        //        return str;

        //    return str.IsUrlEncoded() ? str : str.UrlEncode();
        //}
        #endregion
    }
}