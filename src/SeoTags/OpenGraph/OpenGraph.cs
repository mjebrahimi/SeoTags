using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoTags
{
    /// <summary>
    /// Create meta tags for OpenGraph (og:)
    /// </summary>
    public class OpenGraph
    {
        #region Properties
        /// <summary>
        /// Gets or sets the type of card. (og:type)
        /// </summary>
        public OpenGraphType? Type { get; set; }

        /// <summary>
        /// Gets or sets the title. (og:title)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description. (og:description)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL. (og:url)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name of the site. (og:site_name)
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the locale. (og:locale)
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the locale alternatives. (og:locale:alternate)
        /// </summary>
        public List<string> LocaleAlternatives { get; set; } = [];

        /// <summary>
        /// Gets or sets the image URL. (og:image and og:image:secure_url if url starts with https:)
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the image MIME. (og:image:type) (Default: if not set, try to detect by image url file extension)
        /// </summary>
        public string ImageMimeType { get; set; }

        /// <summary>
        /// Gets or sets the width of the image. (og:image:width)
        /// </summary>
        public int? ImageWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the image. (og:image:height)
        /// </summary>
        public int? ImageHeight { get; set; }

        /// <summary>
        /// Gets or sets the image alt. (og:image:alt)
        /// </summary>
        public string ImageAlt { get; set; }

        /// <summary>
        /// Gets or sets the article publisher. (article:publisher) (usually facebook id of publisher)
        /// </summary>
        public string ArticlePublisher { get; set; }

        /// <summary>
        /// Gets or sets the article author. (article:author) (usually facebook id of author)
        /// </summary>
        public string ArticleAuthor { get; set; }

        /// <summary>
        /// Gets or sets the article publish time in ISO 8601 in UTC. (article:published_time)
        /// </summary>
        public DateTimeOffset? ArticlePublishTime { get; set; }

        /// <summary>
        /// Gets or sets the article modified time in ISO 8601 in UTC. (article:modified_time)
        /// </summary>
        public DateTimeOffset? ArticleModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the article expiration time in ISO 8601 in UTC. (article:expiration_time)
        /// </summary>
        public DateTimeOffset? ArticleExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets the article section (a high-level topic). (article:section)
        /// </summary>
        public string ArticleSection { get; set; }

        /// <summary>
        /// Gets or sets the article tags. (article:tag)
        /// </summary>
        public List<string> ArticleTags { get; set; } = [];

        /// <summary>
        /// Gets or sets the URL of iframe player of video. (og:video and og:video:secure_url if url starts with https:)
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the video MIME. (og:video:type) (Default: if not set, try to detect by video url file extension. set text/html if can not detect)
        /// </summary>
        public string VideoMimeType { get; set; }

        /// <summary>
        /// Gets or sets the width of the video. (og:video:width)
        /// </summary>
        public int? VideoWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the video. (og:video:height)
        /// </summary>
        public int? VideoHeight { get; set; }

        /// <summary>
        /// Gets or sets the video duration seconds. (og:video:duration)
        /// </summary>
        public int? VideoDurationSeconds { get; set; }

        /// <summary>
        /// Gets or sets the video release date in ISO 8601 in UTC. (og:video:release_date)
        /// </summary>
        public DateTimeOffset? VideoReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the video tags. (og:video:tag)
        /// </summary>
        public List<string> VideoTags { get; set; } = [];

        /// <summary>
        /// Gets or sets the audio URL. (og:audio and og:audio:secure_url if url starts with https:)
        /// </summary>
        public string AudioUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the audio MIME. (og:audio:type) (Default: if not set, try to detect by audio url file extension)
        /// </summary>
        public string AudioMimeType { get; set; }

        /// <summary>
        /// Gets or sets the music creator. (music:creator) (usually facebook id of creator)
        /// </summary>
        public string MusicCreator { get; set; }

        /// <summary>
        /// Gets or sets the book author. (book:author) (usually facebook id of book author)
        /// </summary>
        public string BookAuthor { get; set; }

        /// <summary>
        /// Gets or sets the book isbn. (book:isbn)
        /// </summary>
        public string BookISBN { get; set; }

        /// <summary>
        /// Gets or sets the book releas date in ISO 8601 in UTC. (book:release_date)
        /// </summary>
        public DateTimeOffset? BookReleasDate { get; set; }

        /// <summary>
        /// Gets or sets the book tags. (book:tag)
        /// </summary>
        public List<string> BookTags { get; set; } = [];

        /// <summary>
        /// Gets or sets the product price amount. (product:price:amount and og:price:amount)
        /// </summary>
        public int? ProductPriceAmount { get; set; }

        /// <summary>
        /// Gets or sets the product price currency. (product:price:currency and og:price:currency)
        /// </summary>
        public string ProductPriceCurrency { get; set; }

        /// <summary>
        /// Gets or sets the see also urls. (og:see_also)
        /// </summary>
        public List<string> SeeAlsoUrls { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating render date times as UTC. (default: <see langword="true"/>)
        /// </summary>
        public bool RenderDateTimeAsUTC { get; set; } = true;
        #endregion

        #region Methods
        /// <summary>
        /// Renders tags to specified string builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Render(StringBuilder builder)
        {
            Validate();

            if (Type is not null)
                builder.Append("<meta property=\"og:type\" content=\"").Append(Type.Value.ToDisplay()).AppendLine("\" />");
            if (Title.IsNotNullOrWhiteSpace())
                builder.Append("<meta property=\"og:title\" content=\"").Append(Title.HtmlEncode()).AppendLine("\" />");
            if (Description.IsNotNullOrWhiteSpace())
                builder.Append("<meta property=\"og:description\" content=\"").Append(Description.HtmlEncode()).AppendLine("\" />");
            if (Url.IsNotNullOrWhiteSpace())
                builder.Append("<meta property=\"og:url\" content=\"").Append(Url).AppendLine("\" />");
            if (SiteName.IsNotNullOrWhiteSpace())
                builder.Append("<meta property=\"og:site_name\" content=\"").Append(SiteName.HtmlEncode()).AppendLine("\" />");
            if (Locale.IsNotNullOrWhiteSpace())
                builder.Append("<meta property=\"og:locale\" content=\"").Append(Locale.HtmlEncode()).AppendLine("\" />");
            if (LocaleAlternatives?.Count > 0)
            {
                foreach (var item in LocaleAlternatives)
                {
                    item.EnsureNotNullOrWhiteSpace(nameof(LocaleAlternatives), "Has null or whitespace item.");
                    builder.Append("<meta property=\"og:locale:alternate\" content=\"").Append(item.HtmlEncode()).AppendLine("\" />");
                }
            }

            if (ImageUrl.IsNotNullOrWhiteSpace())
            {
                builder.Append("<meta property=\"og:image\" content=\"").Append(ImageUrl).AppendLine("\" />");
                if (ImageUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase) is true)
                    builder.Append("<meta property=\"og:image:secure_url\" content=\"").Append(ImageUrl).AppendLine("\" />");
                if (ImageMimeType.IsNotNullOrWhiteSpace())
                    builder.Append("<meta property=\"og:image:type\" content=\"").Append(ImageMimeType.HtmlEncode()).AppendLine("\" />");
                else if (Utilities.TryGetContentType(ImageUrl, out var type))
                    builder.Append("<meta property=\"og:image:type\" content=\"").Append(type).AppendLine("\" />");
                if (ImageWidth is not null)
                    builder.Append("<meta property=\"og:image:width\" content=\"").Append(ImageWidth).AppendLine("\" />");
                if (ImageHeight is not null)
                    builder.Append("<meta property=\"og:image:height\" content=\"").Append(ImageHeight).AppendLine("\" />");
                if (ImageAlt.IsNotNullOrWhiteSpace())
                    builder.Append("<meta property=\"og:image:alt\" content=\"").Append(ImageAlt.HtmlEncode()).AppendLine("\" />");
            }

            switch (Type)
            {
                case OpenGraphType.Article:
                    if (ArticlePublisher.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"article:publisher\" content=\"").Append(ArticlePublisher).AppendLine("\" />");
                    if (ArticleAuthor.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"article:author\" content=\"").Append(ArticleAuthor).AppendLine("\" />");
                    if (ArticlePublishTime is not null)
                        builder.Append("<meta property=\"article:published_time\" content=\"").Append(ArticlePublishTime.Value.ToStringIso8601(RenderDateTimeAsUTC)).AppendLine("\" />");
                    if (ArticleModifiedTime is not null)
                        builder.Append("<meta property=\"article:modified_time\" content=\"").Append(ArticleModifiedTime.Value.ToStringIso8601(RenderDateTimeAsUTC)).AppendLine("\" />");
                    if (ArticleExpirationTime is not null)
                        builder.Append("<meta property=\"article:expiration_time\" content=\"").Append(ArticleExpirationTime.Value.ToStringIso8601(RenderDateTimeAsUTC)).AppendLine("\" />");
                    if (ArticleSection.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"article:section\" content=\"").Append(ArticleSection.HtmlEncode()).AppendLine("\" />");
                    if (ArticleTags?.Count > 0)
                    {
                        foreach (var item in ArticleTags)
                        {
                            item.EnsureNotNullOrWhiteSpace(nameof(ArticleTags), "Has null or whitespace item.");
                            builder.Append("<meta property=\"article:tag\" content=\"").Append(item.HtmlEncode()).AppendLine("\" />");
                        }
                    }
                    break;

                case OpenGraphType.Video when VideoUrl.IsNotNullOrWhiteSpace():
                    builder.Append("<meta property=\"og:video\" content=\"").Append(VideoUrl).AppendLine("\" />");
                    if (VideoUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase) is true)
                        builder.Append("<meta property=\"og:video:secure_url\" content=\"").Append(VideoUrl).AppendLine("\" />");
                    if (VideoWidth is not null)
                        builder.Append("<meta property=\"og:video:width\" content=\"").Append(VideoWidth).AppendLine("\" />");
                    if (VideoHeight is not null)
                        builder.Append("<meta property=\"og:video:height\" content=\"").Append(VideoHeight).AppendLine("\" />");
                    if (VideoMimeType.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"og:video:type\" content=\"").Append(VideoMimeType.HtmlEncode()).AppendLine("\" />");
                    else if (Utilities.TryGetContentType(VideoUrl, out var type))
                        builder.Append("<meta property=\"og:video:type\" content=\"").Append(type).AppendLine("\" />");
                    else
                        builder.Append("<meta property=\"og:video:type\" content=\"text/html\" />"); //Default mime type of iframe url of video player
                    if (VideoDurationSeconds is not null)
                        builder.Append("<meta property=\"og:video:duration\" content=\"").Append(VideoDurationSeconds).AppendLine("\" />");
                    if (VideoReleaseDate is not null)
                        builder.Append("<meta property=\"og:video:release_date\" content=\"").Append(VideoReleaseDate.Value.ToStringIso8601(RenderDateTimeAsUTC)).AppendLine("\" />");
                    if (VideoTags?.Count > 0)
                    {
                        foreach (var item in VideoTags)
                        {
                            item.EnsureNotNullOrWhiteSpace(nameof(VideoTags), "Has null or whitespace item.");
                            builder.Append("<meta property=\"og:video:tag\" content=\"").Append(item.HtmlEncode()).AppendLine("\" />");
                        }
                    }
                    break;

                case OpenGraphType.Audio when AudioUrl.IsNotNullOrWhiteSpace():
                    builder.Append("<meta property=\"og:audio\" content=\"").Append(AudioUrl).AppendLine("\" />");
                    if (AudioUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase) is true)
                        builder.Append("<meta property=\"og:audio:secure_url\" content=\"").Append(AudioUrl).AppendLine("\" />");
                    if (AudioMimeType.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"og:audio:type\" content=\"").Append(AudioMimeType.HtmlEncode()).AppendLine("\" />");
                    else if (Utilities.TryGetContentType(AudioUrl, out var type))
                        builder.Append("<meta property=\"og:audio:type\" content=\"").Append(type).AppendLine("\" />");
                    if (MusicCreator.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"music:creator\" content=\"").Append(MusicCreator).AppendLine("\" />");
                    break;

                case OpenGraphType.Book:
                    if (BookAuthor.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"book:author\" content=\"").Append(BookAuthor).AppendLine("\" />");
                    if (BookISBN.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"book:isbn\" content=\"").Append(BookISBN.HtmlEncode()).AppendLine("\" />");
                    if (BookReleasDate is not null)
                        builder.Append("<meta property=\"book:release_date\" content=\"").Append(BookReleasDate.Value.ToStringIso8601(RenderDateTimeAsUTC)).AppendLine("\" />");
                    if (BookTags?.Count > 0)
                    {
                        foreach (var item in BookTags)
                        {
                            item.EnsureNotNullOrWhiteSpace(nameof(BookTags), "Has null or whitespace item.");
                            builder.Append("<meta property=\"book:tag\" content=\"").Append(item.HtmlEncode()).AppendLine("\" />");
                        }
                    }
                    break;

                case OpenGraphType.Product:
                    var priceCurrency = ProductPriceCurrency?.HtmlEncode();
                    if (ProductPriceAmount is not null)
                        builder.Append("<meta property=\"product:price:amount\" content=\"").Append(ProductPriceAmount).AppendLine("\" />");
                    if (priceCurrency.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"product:price:currency\" content=\"").Append(priceCurrency).AppendLine("\" />");
                    //<meta property="product:condition" content="new">
                    //<meta property="product:availability" content="in stock">
                    if (ProductPriceAmount is not null)
                        builder.Append("<meta property=\"og:price:amount\" content=\"").Append(ProductPriceAmount).AppendLine("\" />");
                    if (priceCurrency.IsNotNullOrWhiteSpace())
                        builder.Append("<meta property=\"og:price:currency\" content=\"").Append(priceCurrency).AppendLine("\" />");
                    break;
            }

            if (SeeAlsoUrls?.Count > 0)
            {
                foreach (var item in SeeAlsoUrls)
                {
                    item.EnsureNotNullOrWhiteSpace(nameof(SeeAlsoUrls), "Has null or whitespace item.");
                    builder.Append("<meta property=\"og:see_also\" content=\"").Append(item).AppendLine("\" />");
                }
            }

            builder.AppendLine();
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            Type?.EnsureIsValid(nameof(Type));

            if (ImageUrl is null)
            {
                if (ImageWidth is not null)
                    throw new ArgumentException("Image width is set but image url is not set.");
                if (ImageHeight is not null)
                    throw new ArgumentException("Image height is set but image url is not set.");
                if (ImageMimeType.IsNotNullOrWhiteSpace())
                    throw new ArgumentException("Image mime type is set but image url is not set.");
                if (ImageAlt.IsNotNullOrWhiteSpace())
                    throw new ArgumentException("Image alt is set but image url is not set.");
            }

            if (VideoUrl is null)
            {
                if (VideoWidth is not null)
                    throw new ArgumentException("Video width is set but video url is not set.");
                if (VideoHeight is not null)
                    throw new ArgumentException("Video height is set but video url is not set.");
                if (VideoMimeType.IsNotNullOrWhiteSpace())
                    throw new ArgumentException("Video mime type is set but video url is not set.");
                if (VideoDurationSeconds is not null)
                    throw new ArgumentException("Video duration seconds is set but video url is not set.");
                if (VideoReleaseDate is not null)
                    throw new ArgumentException("Video release date is set but video url is not set.");
                if (VideoTags?.Count > 0)
                    throw new ArgumentException("Video tags is set but video url is not set.");
            }

            if (AudioUrl is null && AudioMimeType.IsNotNullOrWhiteSpace())
                throw new ArgumentException("Audio mime type is set but audio url is not set.");
        }

        /// <summary>
        /// Sets the common information.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="url">The URL.</param>
        /// <param name="seeAlsoUrls">The see also urls.</param>
        public OpenGraph SetCommonInfo(string title, string description, string url, IEnumerable<string> seeAlsoUrls = null)
        {
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            description.EnsureNotNullOrWhiteSpace(nameof(description));
            url.EnsureNotNullOrWhiteSpace(nameof(url));
            seeAlsoUrls?.EnsureNotNullItem(nameof(seeAlsoUrls));

            Type ??= OpenGraphType.Website; //null-coalescing assign to prevent overwrite value
            Title = title;
            Description = description;
            Url = url;
            SeeAlsoUrls = seeAlsoUrls?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the image information.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alt">The alt.</param>
        /// <param name="mimeType">Type of the MIME.(Default: if not set, try to detect by image url file extension)</param>
        public OpenGraph SetImageInfo(string url, int? width = null, int? height = null, string alt = null, string mimeType = null)
        {
            url.EnsureNotNullOrWhiteSpace(nameof(url));

            ImageUrl = url;
            ImageWidth = width;
            ImageHeight = height;
            ImageAlt = alt;
            ImageMimeType = mimeType;
            return this;
        }

        /// <summary>
        /// Sets the article information.
        /// </summary>
        /// <param name="publishTime">The publish time of artile.</param>
        /// <param name="author">The author of artile (usually author facebook id).</param>
        /// <param name="section">The section of artile (a high-level topic).</param>
        /// <param name="tags">The tags of artile.</param>
        /// <param name="modifiedTime">The modified time of artile.</param>
        /// <param name="expirationTime">The expiration time of artile.</param>
        public OpenGraph SetArticleInfo(DateTimeOffset publishTime, string author = null, string section = null, IEnumerable<string> tags = null,
            DateTimeOffset? modifiedTime = null, DateTimeOffset? expirationTime = null)
        {
            tags?.EnsureNotNullItem(nameof(tags));

            Type = OpenGraphType.Article;
            ArticleAuthor = author;
            ArticlePublishTime = publishTime;
            ArticleModifiedTime = modifiedTime;
            ArticleExpirationTime = expirationTime;
            ArticleSection = section;
            ArticleTags = tags?.ToList();
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
        /// <param name="mimeType">The mime type of video. (Default: if not set, try to detect by video url file extension)</param>
        /// <param name="tags">The video tags.</param>
        public OpenGraph SetVideoInfo(string url, int? width = null, int? height = null, DateTimeOffset? releaseDate = null, int? durationSeconds = null,
            string mimeType = null, IEnumerable<string> tags = null)
        {
            url.EnsureNotNullOrWhiteSpace(nameof(url));
            tags?.EnsureNotNullItem(nameof(tags));

            Type = OpenGraphType.Video;
            VideoUrl = url;
            VideoMimeType = mimeType;
            VideoWidth = width;
            VideoHeight = height;
            VideoDurationSeconds = durationSeconds;
            VideoReleaseDate = releaseDate;
            VideoTags = tags?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the audio information.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="mimeType">The mime type of audio. (Default: if not set, try to detect by audio url file extension)</param>
        /// <param name="creator">The creator (usually creator facebook id).</param>
        public OpenGraph SetAudioInfo(string url, string mimeType = null, string creator = null)
        {
            url.EnsureNotNullOrWhiteSpace(nameof(url));

            Type = OpenGraphType.Audio;
            AudioUrl = url;
            AudioMimeType = mimeType;
            MusicCreator = creator;
            return this;
        }

        /// <summary>
        /// Sets the book information.
        /// </summary>
        /// <param name="author">The author (usually author facebook id).</param>
        /// <param name="isbn">The isbn of book.</param>
        /// <param name="bookReleaseDate">The book release date.</param>
        /// <param name="tags">The books tags.</param>
        public OpenGraph SetBookInfo(string author, string isbn = null, DateTimeOffset? bookReleaseDate = null, IEnumerable<string> tags = null)
        {
            author.EnsureNotNullOrWhiteSpace(author);
            tags?.EnsureNotNullItem(nameof(tags));

            Type = OpenGraphType.Book;
            BookAuthor = author;
            BookISBN = isbn;
            BookReleasDate = bookReleaseDate;
            BookTags = tags?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the produc information.
        /// </summary>
        /// <param name="priceAmount">The price amount.</param>
        /// <param name="priceCurrency">The price currency.</param>
        public OpenGraph SetProducInfo(int priceAmount, string priceCurrency)
        {
            priceCurrency.EnsureNotNullOrWhiteSpace(nameof(priceCurrency));

            ProductPriceAmount = priceAmount;
            ProductPriceCurrency = priceCurrency;
            return this;
        }

        /// <summary>
        /// Sets the locales.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="localeAlternatives">The locale alternatives.</param>
        public OpenGraph SetLocales(string locale, string[] localeAlternatives = null)
        {
            locale.EnsureNotNullOrWhiteSpace(nameof(locale));
            localeAlternatives?.EnsureNotNullItem(nameof(localeAlternatives));

            Locale = locale;
            LocaleAlternatives = localeAlternatives?.ToList();
            return this;
        }

        /// <summary>
        /// Adds the see also urls.
        /// </summary>
        /// <param name="seeAlsoUrls">The see also urls.</param>
        public OpenGraph AddSeeAlsoUrls(params string[] seeAlsoUrls)
        {
            seeAlsoUrls.EnsureNotNullAndNotNullItem(nameof(seeAlsoUrls));

            SeeAlsoUrls ??= [];
            SeeAlsoUrls.AddRange(seeAlsoUrls);
            return this;
        }

        /// <summary>
        /// Adds the see also urls.
        /// </summary>
        /// <param name="seeAlsoUrls">The see also urls.</param>
        public OpenGraph AddSeeAlsoUrls(IEnumerable<string> seeAlsoUrls)
        {
            seeAlsoUrls.EnsureNotNullAndNotNullItem(nameof(seeAlsoUrls));

            SeeAlsoUrls ??= [];
            SeeAlsoUrls.AddRange(seeAlsoUrls);
            return this;
        }
        #endregion

        #region Examples
        /*
        <!-- Open Graph data -->
        <meta property="og:site_name" content="@Model.SiteName" />
        <meta property="og:title" content="@Model.PageTitle" />
        <meta property="og:description" content="@Model.Description" />
        <meta property="og:url" content="@Model.PageUrl" />
        <meta property="og:locale" content="fa_IR" />
        <meta property="og:locale:alternate" content="en_US" />
        <meta property="og:type" content="@Model.OGType.ToString().ToLower()" /> (website - article - product - video.movie - book)

        <meta property="og:cover" content="@Model.ImageUrl">
        <meta property="og:image" content="@Model.ImageUrl" />
        <meta property="og:image:secure_url" content="@Model.ImageUrl" />
        <meta property="og:image:type" content="@Model.ImageType()" />
        <meta property="og:image:width" content="@Model.ImageWidth()" />
        <meta property="og:image:height" content="@Model.ImageHeight()" />
        <meta property="og:image:alt" content="@Model.PageTitle" />

        @if (Model.OGType == OGTypes.Article)
        {
            https://www.freecodecamp.org/news/what-is-open-graph-and-how-can-i-use-it-for-my-website/
            https://www.storybench.org/add-meta-tags-optimize-news-article-social-media/
            <meta property="article:publisher" content="https://www.facebook.com/mizfacom/" />
            <meta property="article:author" content="https://jber595.medium.com" />

            <meta property="article:publisher" content="@Model.SiteFacebookId" />
            <meta property="article:author" content="@Model.AuthorFacebookId" />

            <!-- time can be in UTC (2021-06-23T19:42:25+00:00) -->
            <meta property="article:published_time" content="@Model.PublishedTime.Value.ToString("yyyy-MM-ddTHH:mm:ss+03:30")" />
            <meta property="article:modified_time" content="@Model.ModifiedTime.Value.ToString("yyyy-MM-ddTHH:mm:ss+03:30")" />
            <meta property="article:section" content="@Model.Section" /> (A high-level section name. E.g. Technology)
            foreach (var item in Model.Tag.Split(','))
            {
                <meta property="article:tag" content="@item" />
            }
        }

        <meta property="og:video" content="@Model.VideoUrl" />
        <meta property="og:video:secure_url" content="@Model.VideoUrl" />
        <meta property="og:video:width" content="@Model.VideoWidth" />
        <meta property="og:video:height" content="@Model.VideoHeight" />
        <meta property="og:video:type" content="@Model.VideoMimeType" />
        <meta property="og:video:duration" content="@Model.VideoDurationSeconds" /> <!-- integer >=1 - The movie's length in seconds. -->
        <meta property="og:video:release_date" content="@Model.VideoReleaseDate" />
        @foreach (var item in Model.Tag.Split(','))
        {
            <meta property="video:tag" content="@item" />
        }

        <meta property="og:audio" content="@Model.AudioUrl" />
        <meta property="og:audio:secure_url" content="@Model.AudioUrl" />
        <meta property="og:audio:type" content="@Model.AudioMimeType" />

        <meta property="book:author" content="@Model.BookAuthor" />
        <meta property="book:isbn" content="@Model.BookISBN" />
        <meta property="book:release_date" content="@Model.BookReleaseDate" />
        @foreach (var item in Model.Tag.Split(','))
        {
            <meta property="book:tag" content="@item" />
        }

        <!-- for product -->
        https://developers.facebook.com/docs/payments/product/
        <meta property="og:type" content="og:product">
        <meta property="product:price:amount" content="@Model.ProductPriceIRR">
        <meta property="product:price:currency" content="IRR">
        <meta property="product:condition" content="new">
        <meta property="product:availability" content="in stock">
        https://raptor-dmt.com/seo/open-graph-and-social-tags/
        <meta property="og:price:amount" content="@Model.ProductPriceIRR" />
        <meta property="og:price:currency" content="IRR" />

        <meta property="og:see_also" content="https://www.instagram.com/moz_hq/">
        <meta property="og:see_also" content="https://www.youtube.com/channel/UCs26XZBwrSZLiTEH8wcoVXw">
        <meta property="og:see_also" content="https://www.linkedin.com/company/moz/">
        <meta property="og:see_also" content="https://www.facebook.com/moz/">
        <meta property="og:see_also" content="https://twitter.com/Moz">
        */
        #endregion
    }
}
