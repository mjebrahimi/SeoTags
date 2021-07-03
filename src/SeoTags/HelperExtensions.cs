using SeoTags;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
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
            seoInfo.MetaLink.Render(builder);
            seoInfo.TwitterCard.Render(builder);
            seoInfo.OpenGraph.Render(builder);
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Render Meta tags and link tags.
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="metaLink">The meta link.</param>
        /// <returns>Output</returns>
        public static IHtmlContent MetaLink(this IHtmlHelper _, MetaLink metaLink)
        {
            var builder = new StringBuilder();
            metaLink.Render(builder);
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Render twitter card tags (twitter: meta tags)
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="twitterCard">The twitter card.</param>
        /// <returns>Output</returns>
        public static IHtmlContent TwitterCard(this IHtmlHelper _, TwitterCard twitterCard)
        {
            var builder = new StringBuilder();
            twitterCard.Render(builder);
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Render open graph tags (og: meta tags)
        /// </summary>
        /// <param name="_">The HTML helper.</param>
        /// <param name="openGraph">The open graph.</param>
        /// <returns>Output</returns>
        public static IHtmlContent OpenGraph(this IHtmlHelper _, OpenGraph openGraph)
        {
            var builder = new StringBuilder();
            openGraph.Render(builder);
            return new HtmlString(builder.ToString());
        }

        //public static IHtmlContent Icon(this IHtmlHelper _, FavIcon favIcon)
        //{
        //    //Microsoft.AspNetCore.Html.HtmlContentBuilder
        //    //Microsoft.AspNetCore.Html.HtmlContentBuilderExtensions
        //    //Microsoft.AspNetCore.Html.HtmlString (warp encoded html string)
        //    //Microsoft.AspNetCore.Html.HtmlFormattableString (same as HtmlString but formattable string)
        //    //Microsoft.AspNetCore.Mvc.ViewFeatures.StringHtmlContent (encode input string when writen)
        //    //Microsoft.AspNetCore.Mvc.Rendering.TagBuilder (html tag builder)
        //
        //    var builder = new StringBuilder();
        //    favIcon.Render(builder);
        //    return new HtmlString(builder.ToString());
        //}
    }
}
