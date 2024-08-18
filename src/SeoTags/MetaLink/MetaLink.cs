using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoTags
{
    /// <summary>
    /// Create meta and link tags
    /// </summary>
    public class MetaLink
    {
        #region Properties
        /// <summary>
        /// Gets or sets the charset meta tag. (default: "utf-8")
        /// </summary>
        public string Charset { get; set; } = "utf-8";

        /// <summary>
        /// Gets or sets the http-equi='Content-Type' meta tag. (default: "text/html; charset=utf-8")
        /// </summary>
        public string ContentType { get; set; } = "text/html; charset=utf-8";

        /// <summary>
        /// Gets or sets the http-equiv='X-UA-Compatible' meta tag. (default: "IE=edge, chrome=1")
        /// </summary>
        public string XUACompatible { get; set; } = "IE=edge, chrome=1";

        /// <summary>
        /// Gets or sets the name='viewport' meta tag. ("width=device-width, initial-scale=1")
        /// </summary>
        public string ViewPort { get; set; } = "width=device-width, initial-scale=1";

        /// <summary>
        /// Gets or sets the DNS prefetch urls. (create both rel='preconnect' and rel='dns-prefetch' link tags)
        /// </summary>
        public List<string> DnsPrefetchUrls { get; set; } = [];

        /// <summary>
        /// Gets or sets the preloads. (create rel='preload' link tag).
        /// </summary>
        public List<Preload> Preloads { get; set; } = [];

        /// <summary>
        /// Gets or sets the site title. (final title generate from SiteTitle and PageTitle by TitleFormat)
        /// </summary>
        public string SiteTitle { get; set; }

        /// <summary>
        /// Gets or sets the page title. (create both title tag and name='title' meta tag)
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the title format. (default is "{0} - {1}" which geneate "{SiteTitle} - {PageTitle}")
        /// </summary>
        public string TitleFormat { get; set; } = "{0} - {1}";

        /// <summary>
        /// Gets or sets the description. (create name='description' meta tag)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the keywords. (create name='keywords' meta tag)
        /// </summary>
        public List<string> Keywords { get; set; } = [];

        /// <summary>
        /// Gets or sets the canonical URL. (create rel='canonical' link tag)
        /// </summary>
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the author. (create name='author' meta tag)
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the author URL. (create rel='author' link tag)
        /// </summary>
        public string AuthorUrl { get; set; }

        /// <summary>
        /// Gets or sets the previous URL. (create rel='prev' link tag)
        /// </summary>
        public string PrevUrl { get; set; }

        /// <summary>
        /// Gets or sets the next URL. (create rel='next' link tag)
        /// </summary>
        public string NextUrl { get; set; }

        /// <summary>
        /// Gets or sets the title of the open search.
        /// </summary>
        public string OpenSearchTitle { get; set; }

        /// <summary>
        /// Gets or sets the open search URL. (create rel='application/opensearchdescription+xml' link tag)
        /// </summary>
        public string OpenSearchUrl { get; set; }

        /// <summary>
        /// Gets or sets the feeds. (create application/rss+xml and application/atom+xml link tags)
        /// </summary>
        public List<Feed> Feeds { get; set; } = [];

        /// <summary>
        /// Gets or sets the robots meta tag.
        /// </summary>
        public string Robots { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Renders tags to specified string builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Render(StringBuilder builder)
        {
            TitleFormat.EnsureNotNullOrWhiteSpace(nameof(TitleFormat));

            if (Charset.IsNotNullOrWhiteSpace())
                builder.Append("<meta charset=\"").Append(Charset.HtmlEncode()).AppendLine("\" />");
            if (ContentType.IsNotNullOrWhiteSpace())
                builder.Append("<meta http-equiv=\"Content-Type\" content=\"").Append(ContentType.HtmlEncode()).AppendLine("\" />");
            if (XUACompatible.IsNotNullOrWhiteSpace())
                builder.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"").Append(XUACompatible.HtmlEncode()).AppendLine("\" />");
            if (ViewPort.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"viewport\" content=\"").Append(ViewPort.HtmlEncode()).AppendLine("\" />");
            builder.AppendLine();

            if (DnsPrefetchUrls?.Count > 0)
            {
                foreach (var url in DnsPrefetchUrls)
                {
                    url.EnsureNotNullOrWhiteSpace(nameof(DnsPrefetchUrls), "Has null item");
                    builder.Append("<link rel=\"preconnect\" href=\"").Append(url).AppendLine("\" crossorigin />");
                }
                foreach (var url in DnsPrefetchUrls)
                    builder.Append("<link rel=\"dns-prefetch\" href=\"").Append(url).AppendLine("\" />");
            }
            if (Preloads?.Count > 0)
            {
                foreach (var item in Preloads)
                {
                    item.EnsureNotNull(nameof(Preloads), "Has null item");
                    item.Url.EnsureNotNullOrWhiteSpace(nameof(item.Url));
                    item.As?.EnsureIsValid(nameof(item.As));

                    var url = item.Url;
                    var type = item.MimeType?.HtmlEncode() ?? Utilities.GetContentType(url);
                    var @as = item.As ?? DetectPreloadType(type);

                    switch (@as)
                    {
                        case PreloadType.Style:
                        case PreloadType.Script:
                            builder.Append("<link rel=\"preload\" as=\"").Append(@as.ToDisplay()).Append("\" href=\"").Append(url).AppendLine("\" />");
                            break;
                        case PreloadType.Font:
                            builder.Append("<link rel=\"preload\" as=\"").Append(@as.ToDisplay()).Append("\" type=\"").Append(type).Append("\" href=\"").Append(url).AppendLine("\" crossorigin />");
                            break;
                        default:
                            builder.Append("<link rel=\"preload\" as=\"").Append(@as.ToDisplay()).Append("\" type=\"").Append(type).Append("\" href=\"").Append(url).AppendLine("\" />");
                            break;
                    }
                }
            }
            builder.AppendLine();

            var finalTitle = string.Format(TitleFormat, PageTitle, SiteTitle).HtmlEncode();
            builder.Append("<title>").Append(finalTitle).AppendLine("</title>");
            builder.Append("<meta name=\"title\" content=\"").Append(finalTitle).AppendLine("\" />");
            if (Description.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"description\" content=\"").Append(Description.HtmlEncode()).AppendLine("\" />");
            if (Keywords?.Count > 0)
            {
                var keywords = Keywords.Select(p =>
                {
                    p.EnsureNotNullOrWhiteSpace(nameof(Keywords));
                    return p.HtmlEncode();
                });
                builder.Append("<meta name=\"keywords\" content=\"").AppendJoin(", ", keywords).AppendLine("\" />");
            }

            if (AuthorName.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"author\" content=\"").Append(AuthorName.HtmlEncode()).AppendLine("\" />");
            if (AuthorUrl.IsNotNullOrWhiteSpace())
                builder.Append("<link rel=\"author\" href=\"").Append(AuthorUrl).AppendLine("\" />");
            if (CanonicalUrl.IsNotNullOrWhiteSpace())
                builder.Append("<link rel=\"canonical\" href=\"").Append(CanonicalUrl).AppendLine("\" />");
            if (PrevUrl.IsNotNullOrWhiteSpace())
                builder.Append("<link rel=\"prev\" href=\"").Append(PrevUrl).AppendLine("\" />");
            if (NextUrl.IsNotNullOrWhiteSpace())
                builder.Append("<link rel=\"next\" href=\"").Append(NextUrl).AppendLine("\" />");
            if (OpenSearchUrl.IsNotNullOrWhiteSpace())
            {
                OpenSearchUrl.EnsureNotNullOrWhiteSpace(nameof(OpenSearchUrl));
                OpenSearchTitle.EnsureNotNullOrWhiteSpace(nameof(OpenSearchTitle));
                builder.Append("<link rel=\"application/opensearchdescription+xml\" title=\"").Append(OpenSearchTitle.HtmlEncode()).Append("\" href=\"").Append(OpenSearchUrl).AppendLine("\" />");
            }
            if (Feeds?.Count > 0)
            {
                foreach (var item in Feeds)
                {
                    item.EnsureNotNull(nameof(Feeds), "Has null item");
                    item.Title.EnsureNotNullOrWhiteSpace(nameof(item.Title));
                    item.Url.EnsureNotNullOrWhiteSpace(nameof(item.Url));
                    item.FeedType.EnsureIsValid(nameof(item.FeedType));

                    builder.Append("<link rel=\"alternate\" type=\"").Append(item.FeedType.ToDisplay()).Append("\" title=\"").Append(item.Title.HtmlEncode()).Append("\" href=\"").Append(item.Url).AppendLine("\" />");
                }
            }
            if (Robots.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"robots\" content=\"").Append(Robots).AppendLine("\" />");

            builder.AppendLine();
        }

        /// <summary>
        /// Sets the title format.
        /// </summary>
        /// <param name="titleFormat">The title format.</param>
        public MetaLink SetTitleFormat(string titleFormat = "{0} - {1}")
        {
            titleFormat.EnsureNotNullOrWhiteSpace(nameof(titleFormat));
            TitleFormat = titleFormat;
            return this;
        }

        /// <summary>
        /// Sets the open search.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        public MetaLink SetOpenSearch(string title, string url)
        {
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            url.EnsureNotNullOrWhiteSpace(nameof(url));

            OpenSearchTitle = title;
            OpenSearchUrl = url;
            return this;
        }

        /// <summary>
        /// Sets the common information.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="description">The description.</param>
        /// <param name="url">The URL.</param>
        /// <param name="keywords">The keyword tags.</param>
        public MetaLink SetCommonInfo(string pageTitle, string description, string url, IEnumerable<string> keywords = null)
        {
            pageTitle.EnsureNotNullOrWhiteSpace(nameof(pageTitle));
            description.EnsureNotNullOrWhiteSpace(nameof(description));
            url.EnsureNotNullOrWhiteSpace(nameof(url));
            keywords?.EnsureNotNullItem(nameof(keywords));

            PageTitle = pageTitle;
            Description = description;
            CanonicalUrl = url;
            Keywords = keywords?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the author information.
        /// </summary>
        /// <param name="authorName">Name of the author.</param>
        /// <param name="authorUrl">The author URL.</param>
        public MetaLink SetAuthorInfo(string authorName, string authorUrl)
        {
            AuthorName = authorName;
            AuthorUrl = authorUrl;
            return this;
        }

        /// <summary>
        /// Sets the paging information.
        /// </summary>
        /// <param name="prevUrl">The previous URL.</param>
        /// <param name="nextUrl">The next URL.</param>
        public MetaLink SetPagingInfo(string prevUrl, string nextUrl)
        {
            PrevUrl = prevUrl;
            NextUrl = nextUrl;
            return this;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="feedType">Type of the feed.</param>
        public MetaLink AddFeed(string title, string url, FeedType feedType)
        {
            Feeds ??= [];
            Feeds.Add(new Feed(title, url, feedType));
            return this;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        public MetaLink AddFeed(params Feed[] feeds)
        {
            feeds.EnsureNotNullAndNotNullItem(nameof(feeds));

            Feeds ??= [];
            Feeds.AddRange(feeds);
            return this;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        public MetaLink AddFeed(IEnumerable<Feed> feeds)
        {
            feeds.EnsureNotNullAndNotNullItem(nameof(feeds));

            Feeds ??= [];
            Feeds.AddRange(feeds);
            return this;
        }

        /// <summary>
        /// Adds the DNS prefetch.
        /// </summary>
        /// <param name="dnsPrefetchUrls">The DNS prefetch urls.</param>
        public MetaLink AddDnsPrefetch(params string[] dnsPrefetchUrls)
        {
            dnsPrefetchUrls.EnsureNotNullAndNotNullItem(nameof(dnsPrefetchUrls));

            DnsPrefetchUrls ??= [];
            DnsPrefetchUrls.AddRange(dnsPrefetchUrls);
            return this;
        }

        /// <summary>
        /// Adds the DNS prefetch.
        /// </summary>
        /// <param name="dnsPrefetchUrls">The DNS prefetch urls.</param>
        public MetaLink AddDnsPrefetch(IEnumerable<string> dnsPrefetchUrls)
        {
            dnsPrefetchUrls.EnsureNotNullAndNotNullItem(nameof(dnsPrefetchUrls));

            DnsPrefetchUrls ??= [];
            DnsPrefetchUrls.AddRange(dnsPrefetchUrls);
            return this;
        }

        /// <summary>
        /// Adds preload.
        /// </summary>
        /// <param name="url">The URL of preload for href attribute.</param>
        /// <param name="mimeType">MimeType for the type attribute. (Default: if not set, try to detect by url file extension)</param>
        /// <param name="as">The as attrubite.</param>
        public MetaLink AddPreload(string url, string mimeType = null, PreloadType? @as = null)
        {
            Preloads ??= [];
            Preloads.Add(new Preload(url, mimeType, @as));
            return this;
        }

        /// <summary>
        /// Adds the preload.
        /// </summary>
        /// <param name="preloads">The preloads.</param>
        public MetaLink AddPreload(params Preload[] preloads)
        {
            preloads.EnsureNotNullAndNotNullItem(nameof(preloads));

            Preloads ??= [];
            Preloads.AddRange(preloads);
            return this;
        }

        /// <summary>
        /// Adds the preload.
        /// </summary>
        /// <param name="preloads">The preloads.</param>
        public MetaLink AddPreload(IEnumerable<Preload> preloads)
        {
            preloads.EnsureNotNullAndNotNullItem(nameof(preloads));

            Preloads ??= [];
            Preloads.AddRange(preloads);
            return this;
        }

        /// <summary>
        /// Creates an instance of MetaLink.
        /// </summary>
        /// <param name="siteTitle">The site title.</param>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="description">The description.</param>
        /// <param name="canonicalUrl">The canonical URL.</param>
        /// <param name="keywords">The keywords.</param>
        public static MetaLink CreateNew(string siteTitle, string pageTitle, string description, string canonicalUrl, IEnumerable<string> keywords = null)
        {
            siteTitle.EnsureNotNullOrWhiteSpace(nameof(siteTitle));
            pageTitle.EnsureNotNullOrWhiteSpace(nameof(pageTitle));
            description.EnsureNotNullOrWhiteSpace(nameof(description));
            canonicalUrl.EnsureNotNullOrWhiteSpace(nameof(canonicalUrl));
            keywords?.EnsureNotNullItem(nameof(keywords));

            return new MetaLink
            {
                SiteTitle = siteTitle,
                PageTitle = pageTitle,
                Description = description,
                CanonicalUrl = canonicalUrl,
                Keywords = keywords?.ToList()
            };
        }

        /// <summary>
        /// Detects the type of the preload by specified mime type
        /// </summary>
        /// <param name="type">Mime type.</param>
        private static PreloadType DetectPreloadType(string type)
        {
            return type switch
            {
                string s when s.Equals("text/css") => PreloadType.Style,
                string s when s.Equals("application/javascript") => PreloadType.Script,
                string s when s.Equals("text/javascript") => PreloadType.Script,
                string s when s.StartsWith("image/") => PreloadType.Image,
                string s when s.Contains("font") => PreloadType.Font,
                string s when s.StartsWith("video/") => PreloadType.Video,
                string s when s.StartsWith("audio/") => PreloadType.Audio,
                string s when s.Equals("text/vtt") => PreloadType.Track, //.vtt
                _ => throw new InvalidOperationException("PreloadType not found.")
            };
        }
        #endregion

        #region Examples
        /*
        <!-- RSS -->
        <link rel="alternate" type="application/rss+xml" title="@Model.SiteTitle" href="@Model.RssUrl" />
        <!-- Atom -->
        <link rel="alternate" type="application/atom+xml" title="@Model.SiteTitle" href="@Model.AtomUrl">
        */

        /*
        <meta charset="utf-8">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

        https://stackoverflow.com/questions/22059060/is-it-still-valid-to-use-ie-edge-chrome-1
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1" />

        <meta http-equiv="Content-Language" content="fa-IR" /> (deprecated) https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta
        <meta name="language" content="fa-IR" /> (it seems is not valid)

        https://developer.mozilla.org/en-US/docs/Web/HTML/Viewport_meta_tag
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=0, viewport-fit=cover, shrink-to-fit=no" />

        <!-- Preconnect and Dns-Prefetch -->
        <link rel="preconnect" href="https://fonts.gstatic.com/">
        <link rel="dns-prefetch" href="https://fonts.gstatic.com/">

        <!-- Preload Styles -->
        <link rel="preload" as="style" href="https://cdn.alibaba.ir/dist/5b2d60db/app.bde65bf.css" />
        <!-- Preload Scripts -->
        <link rel="preload" as="script" href="https://cdn.alibaba.ir/dist/5b2d60db/app.8e16b97.js" />
        <!-- Preload Images -->
        <link rel="preload" as="image" href="https://miro.medium.com/max/3840/1*e4TYwTEj4JaZ75MWOWy6yQ.jpeg" />
        <!-- Preload Fonts -->
        <link rel="preload" as="font" type="font/woff2" crossorigin href="https://cdn.alibaba.ir/dist/5b2d60db/fonts/alibaba.9e83f78.woff2" />
        <link rel="preload" as="font" type="font/woff2" crossorigin href="https://cdn.alibaba.ir/dist/5b2d60db/fonts/IRANSansWebFa.38d4b5f.woff2" />
        <link rel="preload" as="font" type="font/woff2" crossorigin href="https://cdn.alibaba.ir/dist/5b2d60db/fonts/IRANSansWebFa_Black.3d58553.woff2" />
        <link rel="preload" as="font" type="font/woff2" crossorigin href="https://cdn.alibaba.ir/dist/5b2d60db/fonts/IRANSansWebFa_Medium.8451859.woff2" />
        <link rel="preload" as="font" type="font/woff2" crossorigin href="https://cdn.alibaba.ir/dist/5b2d60db/fonts/IRANSansWebFa_Bold.924be0f.woff2" />
        <link rel="preload" as="font" type="font/woff2" crossorigin href="https://cdn.alibaba.ir/dist/5b2d60db/fonts/IRANSansWebFa_Light.d11c490.woff2" />

        <!-- General -->
        <title>@Model.PageTitle</title> عنوان صفحه ، حداکثر 60 کارکتر باشد
        <meta name="title" content="@Model.PageTitle" />
        <meta name="description" content="@Model.Description" /> عنوان صفحه ، حداکثر 160 کارکتر باشد
        <meta name="keywords" content="@Model.Tag" />
        <meta name="author" content="@Model.AuthorName" />


        <!-- Canonical -->
        <link rel="canonical" href="@Model.PageUrl" />
        <!-- Author -->
        <link rel="author" href="@Model.AuthorUrl" />
        <!-- Paging Navigation -->
        <link rel="prev" href="@Model.PrevUrl" />
        <link rel="next" href="@Model.NextUrl" />

        <!-- Open Search -->
        <link rel="search" type="application/opensearchdescription+xml" title="@Model.SiteName" href="@Model.OpenSearchUrl" />
        <link rel="search" type="application/opensearchdescription+xml" title="DEV Community" href="https://dev.to/open-search.xml" />
        <link rel="search" type="application/opensearchdescription+xml" title=".NET Tips Search" href="https://www.dntips.ir/opensearch" />
        <link rel="search" type="application/opensearchdescription+xml" title="Medium" href="https://blog.cloudboost.io/osd.xml" />

        <!-- RSS -->
        <link rel="alternate" type="application/rss+xml" title="@Model.SiteTitle" href="@Model.RssUrl" />
        <!-- Atom -->
        <link rel="alternate" type="application/atom+xml" title="@Model.SiteTitle" href="@Model.AtomUrl">
        */
        #endregion
    }
}
