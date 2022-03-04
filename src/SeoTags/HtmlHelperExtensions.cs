using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SeoTags;
using System;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    using SeoTags;
    using Microsoft.AspNetCore.Html;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Text;
    using System.Collections.Generic;

    /// <summary>
    /// HtmlHelper extensions for seo tags
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Sets the seo information.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="config">The configuration.</param>
        public static void SetSeoInfo(this IHtmlHelper htmlHelper, Action<SeoInfo> config)
        {
            var seoInfo = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            config(seoInfo);
        }

        /// <summary>
        /// Render seo tags, read seo tags automaticlty form services. (meta and link tags, twitter card and open graph)
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        public static IHtmlContent SeoTags(this IHtmlHelper htmlHelper)
        {
            var seoInfo = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return htmlHelper.SeoTags(seoInfo);
        }

        /// <summary>
        /// Render seo tags. (meta and link tags, twitter card and open graph)
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="seoInfo">The seo tags.</param>
        public static IHtmlContent SeoTags(this IHtmlHelper _, SeoInfo seoInfo)
        {
            var builder = new StringBuilder();
            seoInfo.Render(builder);
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Render Meta tags and link tags.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        public static IHtmlContent MetaLink(this IHtmlHelper htmlHelper)
        {
            var seoInfo = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return htmlHelper.MetaLink(seoInfo.MetaLink);
        }

        /// <summary>
        /// Render Meta tags and link tags.
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="metaLink">The meta link.</param>
        public static IHtmlContent MetaLink(this IHtmlHelper _, MetaLink metaLink)
        {
            var builder = new StringBuilder();
            metaLink.Render(builder);
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Render twitter card tags (twitter: meta tags)
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        public static IHtmlContent TwitterCard(this IHtmlHelper htmlHelper)
        {
            var seoInfo = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return htmlHelper.TwitterCard(seoInfo.TwitterCard);
        }

        /// <summary>
        /// Render twitter card tags (twitter: meta tags)
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="twitterCard">The twitter card.</param>
        public static IHtmlContent TwitterCard(this IHtmlHelper _, TwitterCard twitterCard)
        {
            var builder = new StringBuilder();
            twitterCard.Render(builder);
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Render open graph tags (og: meta tags)
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        public static IHtmlContent OpenGraph(this IHtmlHelper htmlHelper)
        {
            var seoInfo = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return htmlHelper.OpenGraph(seoInfo.OpenGraph);
        }

        /// <summary>
        /// Render open graph tags (og: meta tags)
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="openGraph">The open graph.</param>
        public static IHtmlContent OpenGraph(this IHtmlHelper _, OpenGraph openGraph)
        {
            var builder = new StringBuilder();
            openGraph.Render(builder);
            return new HtmlString(builder.ToString());

            //Microsoft.AspNetCore.Html.HtmlContentBuilder
            //Microsoft.AspNetCore.Html.HtmlContentBuilderExtensions
            //Microsoft.AspNetCore.Html.HtmlString (warp encoded html string)
            //Microsoft.AspNetCore.Html.HtmlFormattableString (same as HtmlString but formattable string)
            //Microsoft.AspNetCore.Mvc.ViewFeatures.StringHtmlContent (encode input string when writen)
            //Microsoft.AspNetCore.Mvc.Rendering.TagBuilder (html tag builder)
        }

        /// <summary>
        /// Render page title
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        public static string GetPageTitle(this IHtmlHelper _)
        {
            var seoInfo = _.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return seoInfo.MetaLink.PageTitle;
        }

        /// <summary>
        /// Render page description
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        public static string GetPageDescription(this IHtmlHelper _)
        {
            var seoInfo = _.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return seoInfo.MetaLink.Description;
        }

        /// <summary>
        /// Render page keywords
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        public static List<string> GetPageKeywords(this IHtmlHelper _)
        {
            var seoInfo = _.ViewContext.HttpContext.RequestServices.GetRequiredService<SeoInfo>();
            return seoInfo.MetaLink.Keywords;
        }
    }
}

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// HttpContext extensions for seo tags
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Sets the seo information.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <param name="config">The configuration.</param>
        public static void SetSeoInfo(this HttpContext httpContext, Action<SeoInfo> config)
        {
            var seoInfo = httpContext.RequestServices.GetRequiredService<SeoInfo>();
            config(seoInfo);
        }
    }
}