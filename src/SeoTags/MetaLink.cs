using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<string> DnsPrefetchUrls { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the preloads. (create rel='preload' link tag).
        /// </summary>
        public List<Preload> Preloads { get; set; } = new List<Preload>();

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
        public List<string> Keywords { get; set; } = new List<string>();

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
        public List<Feed> Feeds { get; set; } = new List<Feed>();
        #endregion

        #region Methods
        /// <summary>
        /// Renders tags to specified string builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Render(StringBuilder builder)
        {
            TitleFormat.EnsureNotNull(nameof(TitleFormat));
            SiteTitle.EnsureNotNull(nameof(SiteTitle));
            PageTitle.EnsureNotNull(nameof(PageTitle));
            Description.EnsureNotNull(nameof(Description));

            if (Charset is not null)
                builder.Append("<meta charset=\"").Append(Charset).AppendLine("\" />");
            if (ContentType is not null)
                builder.Append("<meta http-equiv=\"Content-Type\" content=\"").Append(ContentType).AppendLine("\" />");
            if (XUACompatible is not null)
                builder.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"").Append(XUACompatible).AppendLine("\" />");
            if (ViewPort is not null)
                builder.Append("<meta name=\"viewport\" content=\"").Append(ViewPort).AppendLine("\" />");
            builder.AppendLine();

            var dnsPrefetchUrls = DnsPrefetchUrls?.Distinct() ?? new List<string>();
            foreach (var item in dnsPrefetchUrls)
            {
                item.EnsureNotNull(nameof(DnsPrefetchUrls), "Has null item");
                builder.Append("<link rel=\"preconnect\" href=\"").Append(item).AppendLine("\" crossorigin />");
            }
            foreach (var item in dnsPrefetchUrls)
                builder.Append("<link rel=\"dns-prefetch\" href=\"").Append(item).AppendLine("\" />");
            foreach (var item in Preloads ?? new List<Preload>())
            {
                item.EnsureNotNull(nameof(Preloads), "Has null item");
                var type = item.MimeType ?? Utils.GetContentType(item.Url);
                var @as = item.As ?? DetectPreloadType(type);

                switch (@as)
                {
                    case PreloadType.Style:
                    case PreloadType.Script:
                        builder.Append("<link rel=\"preload\" as=\"").Append(@as.ToDisplay()).Append("\" href=\"").Append(item.Url).AppendLine("\" />");
                        break;
                    case PreloadType.Font:
                        builder.Append("<link rel=\"preload\" as=\"").Append(@as.ToDisplay()).Append("\" type=\"").Append(type).Append("\" href=\"").Append(item.Url).AppendLine("\" crossorigin />");
                        break;
                    default:
                        builder.Append("<link rel=\"preload\" as=\"").Append(@as.ToDisplay()).Append("\" type=\"").Append(type).Append("\" href=\"").Append(item.Url).AppendLine("\" />");
                        break;
                }
            }
            builder.AppendLine();

            var finalTitle = string.Format(TitleFormat, PageTitle, SiteTitle);
            builder.Append("<title>").Append(finalTitle).AppendLine("</title>");
            builder.Append("<meta name=\"title\" content=\"").Append(finalTitle).AppendLine("\" />");
            builder.Append("<meta name=\"description\" content=\"").Append(Description).AppendLine("\" />");
            if (Keywords?.Count > 0)
            {
                Keywords.EnsureNotNullItem(nameof(Keywords));
                builder.Append("<meta name=\"keywords\" content=\"").AppendJoin(", ", Keywords.Distinct()).AppendLine("\" />");
            }

            if (AuthorName is not null)
                builder.Append("<meta name=\"author\" content=\"").Append(AuthorName).AppendLine("\" />");
            if (AuthorUrl is not null)
                builder.Append("<link rel=\"author\" href=\"").Append(AuthorUrl).AppendLine("\" />");
            if (CanonicalUrl is not null)
                builder.Append("<link rel=\"canonical\" href=\"").Append(CanonicalUrl).AppendLine("\" />");
            if (PrevUrl is not null)
                builder.Append("<link rel=\"prev\" href=\"").Append(PrevUrl).AppendLine("\" />");
            if (NextUrl is not null)
                builder.Append("<link rel=\"next\" href=\"").Append(NextUrl).AppendLine("\" />");
            if (OpenSearchUrl is not null)
            {
                OpenSearchTitle.EnsureNotNull(nameof(OpenSearchTitle));
                builder.Append("<link rel=\"application/opensearchdescription+xml\" title=\"").Append(OpenSearchTitle).Append("\" href=\"").Append(OpenSearchUrl).AppendLine("\" />");
            }
            foreach (var item in Feeds ?? new List<Feed>())
            {
                item.EnsureNotNull(nameof(Feeds), "Has null item");
                builder.Append("<link rel=\"alternate\" type=\"").Append(item.FeedType.ToDisplay()).Append("\" title=\"").Append(item.Title).Append("\" href=\"").Append(item.Url).AppendLine("\" />");
            }

            builder.AppendLine();
        }

        /// <summary>
        /// Sets the title format.
        /// </summary>
        /// <param name="titleFormat">The title format.</param>
        public void SetTitleFormat(string titleFormat = "{0} - {1}")
        {
            titleFormat.EnsureNotNull(nameof(titleFormat));
            TitleFormat = titleFormat;
        }

        /// <summary>
        /// Sets the open search.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        public void SetOpenSearch(string title, string url)
        {
            title.EnsureNotNull(nameof(title));
            url.EnsureNotNull(nameof(url));

            OpenSearchTitle = title;
            OpenSearchUrl = url;
        }

        /// <summary>
        /// Sets the common information.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="description">The description.</param>
        /// <param name="url">The URL.</param>
        /// <param name="keywords">The keyword tags.</param>
        public void SetCommonInfo(string pageTitle, string description, string url, IEnumerable<string> keywords = null)
        {
            pageTitle.EnsureNotNull(nameof(pageTitle));
            description.EnsureNotNull(nameof(description));
            url.EnsureNotNull(nameof(url));
            keywords?.EnsureNotNullItem(nameof(keywords));

            PageTitle = pageTitle;
            Description = description;
            CanonicalUrl = url;
            Keywords = keywords?.ToList();
        }

        /// <summary>
        /// Sets the author information.
        /// </summary>
        /// <param name="authorName">Name of the author.</param>
        /// <param name="authorUrl">The author URL.</param>
        public void SetAuthorInfo(string authorName, string authorUrl)
        {
            AuthorName = authorName;
            AuthorUrl = authorUrl;
        }

        /// <summary>
        /// Sets the paging information.
        /// </summary>
        /// <param name="prevUrl">The previous URL.</param>
        /// <param name="nextUrl">The next URL.</param>
        public void SetPagingInfo(string prevUrl, string nextUrl)
        {
            PrevUrl = prevUrl;
            NextUrl = nextUrl;
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="feedType">Type of the feed.</param>
        public void AddFeed(string title, string url, FeedType feedType)
        {
            Feeds ??= new List<Feed>();
            Feeds.Add(new Feed(title, url, feedType));
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        public void AddFeed(params Feed[] feeds)
        {
            feeds.EnsureNotNullAndNotNullItem(nameof(feeds));

            Feeds ??= new List<Feed>();
            foreach (var feed in feeds)
                Feeds.Add(feed);
        }

        /// <summary>
        /// Adds the feed.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        public void AddFeed(IEnumerable<Feed> feeds)
        {
            feeds.EnsureNotNullAndNotNullItem(nameof(feeds));

            Feeds ??= new List<Feed>();
            foreach (var feed in feeds)
                Feeds.Add(feed);
        }

        /// <summary>
        /// Adds the DNS prefetch.
        /// </summary>
        /// <param name="dnsPrefetchUrls">The DNS prefetch urls.</param>
        public void AddDnsPrefetch(params string[] dnsPrefetchUrls)
        {
            dnsPrefetchUrls.EnsureNotNullAndNotNullItem(nameof(dnsPrefetchUrls));

            DnsPrefetchUrls ??= new List<string>();
            foreach (var item in dnsPrefetchUrls)
                DnsPrefetchUrls.Add(item);
        }

        /// <summary>
        /// Adds the DNS prefetch.
        /// </summary>
        /// <param name="dnsPrefetchUrls">The DNS prefetch urls.</param>
        public void AddDnsPrefetch(IEnumerable<string> dnsPrefetchUrls)
        {
            dnsPrefetchUrls.EnsureNotNullAndNotNullItem(nameof(dnsPrefetchUrls));

            DnsPrefetchUrls ??= new List<string>();
            foreach (var item in dnsPrefetchUrls)
                DnsPrefetchUrls.Add(item);
        }

        /// <summary>
        /// Adds preload.
        /// </summary>
        /// <param name="url">The URL of preload for href attribute.</param>
        /// <param name="mimeType">MimeType for the type attribute. (Default: if not set, try to detect by url file extension)</param>
        /// <param name="as">The as attrubite.</param>
        public void AddPreload(string url, string mimeType = null, PreloadType? @as = null)
        {
            Preloads ??= new List<Preload>();
            Preloads.Add(new Preload(url, mimeType, @as));
        }

        /// <summary>
        /// Adds the preload.
        /// </summary>
        /// <param name="preloads">The preloads.</param>
        public void AddPreload(params Preload[] preloads)
        {
            preloads.EnsureNotNullAndNotNullItem(nameof(preloads));

            Preloads ??= new List<Preload>();
            foreach (var item in preloads)
                Preloads.Add(item);
        }

        /// <summary>
        /// Adds the preload.
        /// </summary>
        /// <param name="preloads">The preloads.</param>
        public void AddPreload(IEnumerable<Preload> preloads)
        {
            preloads.EnsureNotNullAndNotNullItem(nameof(preloads));

            Preloads ??= new List<Preload>();
            foreach (var item in preloads)
                Preloads.Add(item);
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
            siteTitle.EnsureNotNull(nameof(siteTitle));
            pageTitle.EnsureNotNull(nameof(pageTitle));
            description.EnsureNotNull(nameof(description));
            canonicalUrl.EnsureNotNull(nameof(canonicalUrl));
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

    /// <summary>
    /// Feed
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="feedType">Type of the feed.</param>
        public Feed(string title, string url, FeedType feedType)
        {
            title.EnsureNotNull(nameof(title));
            url.EnsureNotNull(nameof(url));
            feedType.EnsureIsValid(nameof(feedType));

            Title = title;
            Url = url;
            FeedType = feedType;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the type of the feed.
        /// </summary>
        public FeedType FeedType { get; set; }
    }

    /// <summary>
    /// Type of feed
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// The RSS xml
        /// </summary>
        [Display(Name = "application/rss+xml")]
        Rss,

        /// <summary>
        /// The atom xml
        /// </summary>
        [Display(Name = "application/atom+xml")]
        Atom
    }

    /// <summary>
    /// Preload
    /// </summary>
    public class Preload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Preload"/> class.
        /// </summary>
        /// <param name="url">The URL of preload for href attribute.</param>
        /// <param name="mimeType">MimeType for the type attribute. (Default: if not set, try to detect by url file extension)</param>
        /// <param name="as">The as attrubite.</param>
        public Preload(string url, string mimeType = null, PreloadType? @as = null)
        {
            url.EnsureNotNull(nameof(url));
            @as?.EnsureIsValid(nameof(@as));

            Url = url;
            MimeType = mimeType;
            As = @as;
        }

        /// <summary>
        /// Gets or sets the URL of preload (href attribute).
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the MimeType (type attribute). (Default: if not set, try to detect by url file extension)
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets vlaue of (as attribute).
        /// </summary>
        public PreloadType? As { get; set; }
    }

    /// <summary>
    /// Type of preload (as attribute)
    /// </summary>
    public enum PreloadType
    {
        /// <summary>
        /// Audio file, as typically used in &lt;audio&gt;.
        /// </summary>
        [Display(Name = "audio")]
        Audio,

        /// <summary>
        /// An HTML document intended to be embedded by a &lt;frame&gt; or &lt;iframe&gt;.
        /// </summary>
        [Display(Name = "document")]
        Document,

        /// <summary>
        /// A resource to be embedded inside an &lt;embed&gt; element.
        /// </summary>
        [Display(Name = "embed")]
        Embed,

        /// <summary>
        /// Resource to be accessed by a fetch or XHR request, such as an ArrayBuffer or JSON file.
        /// </summary>
        [Display(Name = "fetch")]
        Fetch,

        /// <summary>
        /// Font file.
        /// </summary>
        [Display(Name = "font")]
        Font,

        /// <summary>
        /// Image file.
        /// </summary>
        [Display(Name = "image")]
        Image,

        /// <summary>
        /// A resource to be embedded inside an &lt;object&gt; element.
        /// </summary>
        [Display(Name = "object")]
        Object,

        /// <summary>
        /// JavaScript file.
        /// </summary>
        [Display(Name = "script")]
        Script,

        /// <summary>
        /// CSS stylesheet.
        /// </summary>
        [Display(Name = "style")]
        Style,

        /// <summary>
        /// WebVTT file.
        /// </summary>
        [Display(Name = "track")]
        Track,

        /// <summary>
        /// A JavaScript web worker or shared worker.
        /// </summary>
        [Display(Name = "worker")]
        Worker,

        /// <summary>
        /// Video file, as typically used in &lt;video&gt;.
        /// </summary>
        [Display(Name = "video")]
        Video
    }
}
