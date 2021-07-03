using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SeoTags
{
    internal static class Utils
    {
        //Create a dictionary of common mime types for images, videos, and audios
        //https://github.com/dotnet/aspnetcore/blob/main/src/Middleware/StaticFiles/src/FileExtensionContentTypeProvider.cs
        //https://stackoverflow.com/questions/1029740/get-mime-type-from-filename-extension
        //https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
        //https://www.freeformatter.com/mime-types-list.html
        //https://www.sitepoint.com/mime-types-complete-list/
        //https://github.com/hey-red/MimeTypesMap
        //https://github.com/khellang/MimeTypes
        //https://github.com/zone117x/MimeMapping
        //https://github.com/samuelneff/MimeTypeMap
        private static readonly IContentTypeProvider contentTypeProvider = new FileExtensionContentTypeProvider();

        public static string GetContentType(string subpath, string defaultType = null)
        {
            if (contentTypeProvider.TryGetContentType(subpath, out var contentType))
                return contentType;
            return defaultType ?? throw new InvalidOperationException("Content type not found.");
        }

        public static bool TryGetContentType(string subpath, out string contentType)
        {
            return contentTypeProvider.TryGetContentType(subpath, out contentType);
        }

        public static string ToDisplay<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            //var enumType = value.GetType();
            //var name = Enum.GetName(enumType, value);
            //var attribute = enumType.GetMember(name)[0].GetCustomAttribute<DisplayAttribute>();

            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayAttribute>();
            if (attribute == null)
                return value.ToString();
            return attribute.ResourceType == null ? attribute.Name : attribute.GetName();
        }

        public static void EnsureNotNullAndNotNullItem<T>(this IEnumerable<T> enumerable, string name)
        {
            enumerable.EnsureNotNull(name);
            enumerable.EnsureNotNullItem(name);
        }

        public static void EnsureNotNull<T>(this T obj, string name, string message = null)
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void EnsureNotNullItem<T>(this IEnumerable<T> enumerable, string name, string message = "Has null item")
        {
            if (enumerable.Any(p => p is null))
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void EnsureIsValid<TEnum>(this TEnum @enum, string name, string message = null) where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), @enum) is false)
                throw new ArgumentOutOfRangeException($"{name} : {typeof(TEnum)}", message);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
                action(item);
        }

        public static string ToStringISO8601(this DateTimeOffset dateTimeOffset, bool asUTC)
        {
            return (asUTC ? dateTimeOffset.ToUniversalTime() : dateTimeOffset).ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}
