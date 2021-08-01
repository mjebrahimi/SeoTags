using System;
using System.Collections.Generic;
using System.Text;

namespace SeoTags
{
    /// <summary>
    /// Create SEO tags (meta, link, twitter:, og:)
    /// </summary>
    public class SeoInfo
    {
        #region Properties
        /// <summary>
        /// Gets or sets the meta link.
        /// </summary>
        public MetaLink MetaLink { get; set; } = new();

        /// <summary>
        /// Gets or sets the twitter card.
        /// </summary>
        public TwitterCard TwitterCard { get; set; } = new();

        /// <summary>
        /// Gets or sets the open graph.
        /// </summary>
        public OpenGraph OpenGraph { get; set; } = new();

        /// <summary>
        /// Gets or sets the json ld.
        /// </summary>
        public JsonLd JsonLd { get; set; } = new();
        #endregion

        #region Methods        
        /// <summary>
        /// Renders the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Render(StringBuilder builder)
        {
            MetaLink?.Render(builder);
            TwitterCard?.Render(builder);
            OpenGraph?.Render(builder);
            JsonLd?.Render(builder);
        }

        /// <summary>
        /// Sets the site information.
        /// </summary>
        /// <param name="siteTitle">The site title.</param>
        /// <param name="siteTwitterId">The site twitter id.</param>
        /// <param name="siteFacebookId">The site facebook id.</param>
        /// <param name="openSearchUrl">The open search URL.</param>
        /// <param name="robots">The robots meta tag.</param>
        public SeoInfo SetSiteInfo(string siteTitle, string siteTwitterId = null, string siteFacebookId = null, string openSearchUrl = null, string robots = null)
        {
            siteTitle.EnsureNotNullOrWhiteSpace(nameof(siteTitle));

            MetaLink.SiteTitle = siteTitle;
            MetaLink.OpenSearchTitle = siteTitle;
            MetaLink.OpenSearchUrl = openSearchUrl;
            MetaLink.Robots = robots;

            TwitterCard.Site = siteTwitterId;

            OpenGraph.SiteName = siteTitle;
            OpenGraph.ArticlePublisher = siteFacebookId;
            return this;
        }

        /// <summary>
        /// Sets the open search.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        public SeoInfo SetOpenSearch(string title, string url)
        {
            MetaLink.SetOpenSearch(title, url);
            return this;
        }

        /// <summary>
        /// Sets the title format.
        /// </summary>
        /// <param name="titleFormat">The title format.</param>
        public SeoInfo SetTitleFormat(string titleFormat = "{0} - {1}")
        {
            MetaLink.SetTitleFormat(titleFormat);
            return this;
        }

        /// <summary>
        /// Sets the locales.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="localeAlternatives">The locale alternatives.</param>
        public SeoInfo SetLocales(string locale, string[] localeAlternatives = null)
        {
            OpenGraph.SetLocales(locale, localeAlternatives);
            return this;
        }

        /// <summary>
        /// Sets the paging information.
        /// </summary>
        /// <param name="prevUrl">The previous URL.</param>
        /// <param name="nextUrl">The next URL.</param>
        public SeoInfo SetPagingInfo(string prevUrl, string nextUrl)
        {
            MetaLink.SetPagingInfo(prevUrl, nextUrl);
            return this;
        }

        /// <summary>
        /// Sets the common information.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="description">The description.</param>
        /// <param name="url">The URL.</param>
        /// <param name="keywordTags">The keyword tags.</param>
        /// <param name="seeAlsoUrls">The see also urls.</param>
        public SeoInfo SetCommonInfo(string pageTitle, string description, string url, IEnumerable<string> keywordTags, IEnumerable<string> seeAlsoUrls = null)
        {
            MetaLink.SetCommonInfo(pageTitle, description, url, keywordTags);

            TwitterCard.SetCommonInfo(pageTitle, description);

            OpenGraph.SetCommonInfo(pageTitle, description, url, seeAlsoUrls);
            return this;
        }

        /// <summary>
        /// Sets the image information.
        /// </summary>
        /// <param name="url">The URL of image.</param>
        /// <param name="width">Width of the image.</param>
        /// <param name="height">Height of the image.</param>
        /// <param name="alt">Alt of the image.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="cardType">Type of the card.</param>
        public SeoInfo SetImageInfo(string url, int? width = null, int? height = null, string alt = null, string mimeType = null,
            TwitterCardType cardType = TwitterCardType.SummaryLargeImage)
        {
            MetaLink.Preloads.Add(new Preload(url, mimeType, PreloadType.Image));

            TwitterCard.SetImageInfo(url, width, height, alt, cardType);

            OpenGraph.SetImageInfo(url, width, height, alt, mimeType);
            return this;
        }

        /// <summary>
        /// Sets the article information.
        /// </summary>
        /// <param name="authorName">The name of author.</param>
        /// <param name="publishDate">The publish date of artile.</param>
        /// <param name="modifiedDate">The modified date of artile.</param>
        /// <param name="authorTwitterId">The twitter id of author.</param>
        /// <param name="authorFacebookId">The facebook id of author.</param>
        /// <param name="authorUrl">A url/link of author.</param>
        /// <param name="section">The section of artile (a high-level topic).</param>
        public SeoInfo SetArticleInfo(string authorName, DateTimeOffset publishDate, DateTimeOffset? modifiedDate = null,
            string authorTwitterId = null, string authorFacebookId = null, string authorUrl = null, string section = null)
        {
            MetaLink.SetAuthorInfo(authorName, authorUrl);

            TwitterCard.Creator = authorTwitterId;

            OpenGraph.SetArticleInfo(publishDate, authorFacebookId, section, MetaLink.Keywords, modifiedDate);
            return this;
        }

        /// <summary>
        /// Sets the video information.
        /// </summary>
        /// <param name="url">The URL of iframe player of video.</param>
        /// <param name="width">The width of video.</param>
        /// <param name="height">The height of video.</param>
        /// <param name="releaseDate">The release date of video.</param>
        /// <param name="durationSeconds">The duration seconds of video.</param>
        /// <param name="mimeType">The mime type of video.</param>
        public SeoInfo SetVideoInfo(string url, int? width = null, int? height = null, DateTimeOffset? releaseDate = null, int? durationSeconds = null, string mimeType = null)
        {
            OpenGraph.SetVideoInfo(url, width, height, releaseDate, durationSeconds, mimeType, MetaLink.Keywords);
            return this;
        }

        /// <summary>
        /// Sets the audio information.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="mimeType">The mime type.</param>
        /// <param name="creatorFacebookId">The creator (usually creator facebook id).</param>
        public SeoInfo SetAudioInfo(string url, string mimeType = null, string creatorFacebookId = null)
        {
            OpenGraph.SetAudioInfo(url, mimeType, creatorFacebookId);
            return this;
        }

        /// <summary>
        /// Sets the book information.
        /// </summary>
        /// <param name="authorFacebookId">The author (usually author facebook id).</param>
        /// <param name="isbn">The isbn of book.</param>
        /// <param name="bookReleaseDate">The book release date.</param>
        public SeoInfo SetBookInfo(string authorFacebookId, string isbn = null, DateTimeOffset? bookReleaseDate = null)
        {
            OpenGraph.SetBookInfo(authorFacebookId, isbn, bookReleaseDate, MetaLink.Keywords);
            return this;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="feedType">Type of the feed.</param>
        public SeoInfo AddFeed(string title, string url, FeedType feedType)
        {
            MetaLink.AddFeed(title, url, feedType);
            return this;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        public SeoInfo AddFeed(params Feed[] feeds)
        {
            MetaLink.AddFeed(feeds);
            return this;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        public SeoInfo AddFeed(IEnumerable<Feed> feeds)
        {
            MetaLink.AddFeed(feeds);
            return this;
        }

        /// <summary>
        /// Adds the DNS prefetch.
        /// </summary>
        /// <param name="dnsPrefetchUrls">The DNS prefetch urls.</param>
        public SeoInfo AddDnsPrefetch(params string[] dnsPrefetchUrls)
        {
            MetaLink.AddDnsPrefetch(dnsPrefetchUrls);
            return this;
        }

        /// <summary>
        /// Adds the DNS prefetch.
        /// </summary>
        /// <param name="dnsPrefetchUrls">The DNS prefetch urls.</param>
        public SeoInfo AddDnsPrefetch(IEnumerable<string> dnsPrefetchUrls)
        {
            MetaLink.AddDnsPrefetch(dnsPrefetchUrls);
            return this;
        }

        /// <summary>
        /// Adds preload.
        /// </summary>
        /// <param name="url">The URL of preload for href attribute.</param>
        /// <param name="mimeType">MimeType for the type attribute.</param>
        /// <param name="as">The as attrubite.</param>
        public SeoInfo AddPreload(string url, string mimeType = null, PreloadType? @as = null)
        {
            MetaLink.AddPreload(url, mimeType, @as);
            return this;
        }

        /// <summary>
        /// Adds the preload.
        /// </summary>
        /// <param name="preloads">The preloads.</param>
        public SeoInfo AddPreload(params Preload[] preloads)
        {
            MetaLink.AddPreload(preloads);
            return this;
        }

        /// <summary>
        /// Adds the preload.
        /// </summary>
        /// <param name="preloads">The preloads.</param>
        public SeoInfo AddPreload(IEnumerable<Preload> preloads)
        {
            MetaLink.AddPreload(preloads);
            return this;
        }

        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="data">The data.</param>
        public SeoInfo AddAdditionalInfo(string label, string data)
        {
            TwitterCard.AddAdditionalInfo(label, data);
            return this;
        }

        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        public SeoInfo AddAdditionalInfo(Dictionary<string, string> additionalInfo)
        {
            TwitterCard.AddAdditionalInfo(additionalInfo);
            return this;
        }

        /// <summary>
        /// Adds the see also urls.
        /// </summary>
        /// <param name="seeAlsoUrls">The see also urls.</param>
        public SeoInfo AddSeeAlsoUrls(params string[] seeAlsoUrls)
        {
            OpenGraph.AddSeeAlsoUrls(seeAlsoUrls);
            return this;
        }

        /// <summary>
        /// Adds the see also urls.
        /// </summary>
        /// <param name="seeAlsoUrls">The see also urls.</param>
        public SeoInfo AddSeeAlsoUrls(IEnumerable<string> seeAlsoUrls)
        {
            OpenGraph.AddSeeAlsoUrls(seeAlsoUrls);
            return this;
        }
        #endregion

        #region Examples
        //https://moz.com/beginners-guide-to-seo
        //https://www.semrush.com/blog/seo-checklist/
        //https://ahrefs.com/blog/seo-meta-tags/
        //https://devblogs.microsoft.com/dotnet/page/2/
        //https://devblogs.microsoft.com/dotnet/whats-new-in-windows-forms-in-net-6-0-preview-5/
        //https://www.dntips.ir/post/3343
        //https://blog.cloudboost.io/asp-net-mvc-learn-and-use-blueimp-jquery-file-upload-github-plugin-in-your-project-quickly-d647f02d0a0
        //https://dev.to/ankit01oss/5-github-projects-to-make-you-a-better-devops-engineer-2fkl
        //https://learnfiles.com/course/%D8%AF%D9%88%D8%B1%D9%87-%D8%A2%D9%85%D9%88%D8%B2%D8%B4-%D8%AC%D8%A7%D9%88%D8%A7-%D8%A7%D8%B3%DA%A9%D8%B1%DB%8C%D9%BE%D8%AA/
        //https://www.daneshjooyar.com/%D8%A2%D9%85%D9%88%D8%B2%D8%B4-%D8%B7%D8%B1%D8%A7%D8%AD%DB%8C-%D8%B3%D8%A7%DB%8C%D8%AA-%D9%88-%D8%A7%D9%BE%D9%84%DB%8C%DA%A9%DB%8C%D8%B4%D9%86-%D8%AA%D8%A7%DA%A9%D8%B3%DB%8C-%D8%A2%D9%86%D9%84%D8%A7/
        //https://www.digikala.com/product/dkp-2522021/%DA%A9%D8%B1%D9%88%D8%B3%D8%A7%D9%86-%DA%A9%D8%A7%DA%A9%D8%A7%D8%A6%D9%88-%D9%BE%DA%86-%D9%BE%DA%86-%D8%A8%D8%B3%D8%AA%D9%87-6-%D8%B9%D8%AF%D8%AF%DB%8C
        //https://www.alibaba.ir/mag/best-bodrum-hotels/
        //https://www.zoomit.ir/os/371851-install-windows-11-system-vm/

        //https://github.com/oopanuga/seo-pack
        //https://github.com/sebnilsson/AspNetSeo
        //https://github.com/wintoncode/Winton.AspNetCore.Seo
        //https://github.com/wangkanai/webmaster
        #endregion
    }
}
