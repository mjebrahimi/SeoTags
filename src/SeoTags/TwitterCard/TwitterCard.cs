using System;
using System.Collections.Generic;
using System.Text;

namespace SeoTags
{
    /// <summary>
    /// Create meta tags for twitter: card.
    /// </summary>
    public class TwitterCard
    {
        #region Properties
        /// <summary>
        /// Type of your card. &lt;meta name="twitter:card" content="[for example summary or summary_large_image or product]" /&gt;
        /// </summary>
        public TwitterCardType? CardType { get; set; }

        /// <summary>
        /// Title of your page. &lt;meta name="twitter:title" content="[Page title]" /&gt;
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of your page. &lt;meta name="twitter:description" content="[Page description]" /&gt;
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Twitter id of your site. &lt;meta name="twitter:site" content="[for example: @SiteTwitter]" /&gt;
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Twitter id of creator/author. &lt;meta name="twitter:creator" content="[for example: @AuthorTwitter]" /&gt;
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Url of image. &lt;meta name="twitter:image" content="[for example https://site.com/uploads/image.jpg]" /&gt;
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Width of image. &lt;meta name="twitter:image:width" content="[for example: 500]" /&gt;
        /// </summary>
        public int? ImageWidth { get; set; }

        /// <summary>
        /// Height of image. &lt;meta name="twitter:image:height" content="[for example: 500]" /&gt;
        /// </summary>
        public int? ImageHeight { get; set; }

        /// <summary>
        /// Alt description for image. &lt;meta name="twitter:image:alt" content="[Alt]" /&gt;
        /// </summary>
        public string ImageAlt { get; set; }

        /// <summary>
        /// URL of iframe player. &lt;meta name="twitter:player" content="[for example: https://site.com/videos/test]" /&gt;
        /// </summary>
        public string PlayerUrl { get; set; }

        /// <summary>
        /// Width of video. &lt;meta name="twitter:player:width" content="[for example: 1280]" /&gt;
        /// </summary>
        public int? PlayerWidth { get; set; }

        /// <summary>
        /// Height of video. &lt;meta name="twitter:player:height" content="[for example: 720]" /&gt;
        /// </summary>
        public int? PlayerHeight { get; set; }

        /// <summary>
        /// Gets or sets the additional info.
        /// &lt;meta name="twitter:label1" content="Price in USD" /&gt;
        /// &lt;meta name="twitter:data1" content="1000" /&gt;
        /// &lt;meta name="twitter:label2" content="Color" /&gt;
        /// &lt;meta name="twitter:data2" content="Black" /&gt;
        /// </summary>
        public Dictionary<string, string> AdditionalInfo { get; set; } = [];

        ///// <summary>
        ///// Url of your page. &lt;meta name="twitter:url" content="[for example: https://site.com/page-url]" /gt;
        ///// </summary>
        //public string Url { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Renders tags to specified string builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public virtual void Render(StringBuilder builder)
        {
            Validate();

            if (CardType is not null)
                builder.Append("<meta name=\"twitter:card\" content=\"").Append(CardType.Value.ToDisplay()).AppendLine("\" />");
            if (Title.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"twitter:title\" content=\"").Append(Title.HtmlEncode()).AppendLine("\" />");
            if (Description.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"twitter:description\" content=\"").Append(Description.HtmlEncode()).AppendLine("\" />");
            if (Site.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"twitter:site\" content=\"").Append(Site.HtmlEncode()).AppendLine("\" />");
            if (Creator.IsNotNullOrWhiteSpace())
                builder.Append("<meta name=\"twitter:creator\" content=\"").Append(Creator.HtmlEncode()).AppendLine("\" />");

            if (ImageUrl.IsNotNullOrWhiteSpace())
            {
                builder.Append("<meta name=\"twitter:image\" content=\"").Append(ImageUrl).AppendLine("\" />");
                if (ImageWidth is not null)
                    builder.Append("<meta name=\"twitter:image:width\" content=\"").Append(ImageWidth).AppendLine("\" />");
                if (ImageHeight is not null)
                    builder.Append("<meta name=\"twitter:image:height\" content=\"").Append(ImageHeight).AppendLine("\" />");
                if (ImageAlt.IsNotNullOrWhiteSpace())
                    builder.Append("<meta name=\"twitter:image:alt\" content=\"").Append(ImageAlt.HtmlEncode()).AppendLine("\" />");
            }

            if (PlayerUrl.IsNotNullOrWhiteSpace() && CardType == TwitterCardType.Player)
            {
                if (PlayerUrl.IsNotNullOrWhiteSpace())
                    builder.Append("<meta name=\"twitter:player\" content=\"").Append(PlayerUrl).AppendLine("\" />");
                if (PlayerWidth is not null)
                    builder.Append("<meta name=\"twitter:player:width\" content=\"").Append(PlayerWidth).AppendLine("\" />");
                if (PlayerHeight is not null)
                    builder.Append("<meta name=\"twitter:player:height\" content=\"").Append(PlayerHeight).AppendLine("\" />");
            }

            if (AdditionalInfo is not null)
            {
                var counter = 1;
                foreach (var item in AdditionalInfo)
                {
                    item.Value.EnsureNotNullOrWhiteSpace(nameof(AdditionalInfo), "Has null value");

                    builder.Append("<meta name=\"twitter:label").Append(counter).Append("\" content=\"").Append(item.Key.HtmlEncode()).AppendLine("\" />");
                    builder.Append("<meta name=\"twitter:data").Append(counter).Append("\" content=\"").Append(item.Value.HtmlEncode()).AppendLine("\" />");
                    counter++;
                }
            }

            builder.AppendLine();

            //["نویسنده"] = LabelAuthorName,
            //["مدت زمان مطالعه"] = LabelReadTimeMinutes?.ToString(),
            //["قیمت به ریال"] = LabelProductPriceIRR?.ToString()
            //if (Url is not null)
            //    builder.Append("<meta name=\"twitter:url\" content=\"").Append(Url).AppendLine("\" />");
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            if (ImageUrl is null)
            {
                if (ImageWidth is not null)
                    throw new ArgumentException("Image width is set but image url is not set.");
                if (ImageHeight is not null)
                    throw new ArgumentException("Image height is set but image url is not set.");
                if (ImageAlt is not null)
                    throw new ArgumentException("Image alt is set but image url is not set.");
            }

            if (PlayerUrl is not null && CardType is not TwitterCardType.Player)
                throw new ArgumentException("Player url is set but card type is not player");

            if (CardType is TwitterCardType.Player)
            {
                if (Site is null)
                    throw new ArgumentNullException(nameof(Site), "Card type player is set but site not set.");
                if (PlayerUrl is null)
                    throw new ArgumentNullException(nameof(PlayerUrl), "Card type player is set but player url not set.");
                if (PlayerWidth is null)
                    throw new ArgumentNullException(nameof(PlayerWidth), "Card type player is set but player width not set.");
                if (PlayerHeight is null)
                    throw new ArgumentNullException(nameof(PlayerHeight), "Card type player is set but player height not set.");
                if (ImageUrl is null)
                    throw new ArgumentNullException(nameof(ImageUrl), "Card type player is set but player image url not set.");
            }
        }

        /// <summary>
        /// Sets the common information.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        public TwitterCard SetCommonInfo(string title, string description)
        {
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            description.EnsureNotNullOrWhiteSpace(nameof(description));

            CardType ??= TwitterCardType.Summary; //null-coalescing assign to prevent overwrite value
            Title = title;
            Description = description;
            return this;
        }

        /// <summary>
        /// Sets the image information.
        /// </summary>
        /// <param name="url">The URL of image.</param>
        /// <param name="width">Width of the image.</param>
        /// <param name="height">Height of the image.</param>
        /// <param name="alt">Alt of the image.</param>
        /// <param name="cardType">Type of the card.</param>
        public TwitterCard SetImageInfo(string url, int? width = null, int? height = null, string alt = null, TwitterCardType cardType = TwitterCardType.SummaryLargeImage)
        {
            url.EnsureNotNullOrWhiteSpace(nameof(url));
            cardType.EnsureIsValid(nameof(cardType));

            CardType = cardType;
            ImageUrl = url;
            ImageWidth = width;
            ImageHeight = height;
            ImageAlt = alt;
            return this;
        }

        /// <summary>
        /// Sets the player information.
        /// </summary>
        /// <param name="url">The URL of iframe player.</param>
        /// <param name="width">The width of video.</param>
        /// <param name="height">The height of video.</param>
        public TwitterCard SetPlayerInfo(string url, int width, int height)
        {
            url.EnsureNotNullOrWhiteSpace(nameof(url));

            CardType = TwitterCardType.Player;
            PlayerUrl = url;
            PlayerWidth = width;
            PlayerHeight = height;
            return this;
        }

        /// <summary>
        /// Sets the player information.
        /// </summary>
        /// <param name="playerUrl">The URL of iframe player.</param>
        /// <param name="playerWidth">Width of the video.</param>
        /// <param name="playerHeight">Height of the video.</param>
        /// <param name="site">The site twitter id.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="imageAlt">Alt of the image.</param>
        public TwitterCard SetPlayerInfo(string playerUrl, int playerWidth, int playerHeight, string site, string title,
            string description = null, string imageUrl = null, int? imageWidth = null, int? imageHeight = null, string imageAlt = null)
        {
            site.EnsureNotNullOrWhiteSpace(nameof(site));
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            playerUrl.EnsureNotNullOrWhiteSpace(nameof(playerUrl));

            CardType = TwitterCardType.Player;
            Site = site;
            Title = title;
            PlayerUrl = playerUrl;
            PlayerWidth = playerWidth;
            PlayerHeight = playerHeight;
            Description = description;
            ImageUrl = imageUrl;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            ImageAlt = imageAlt;
            return this;
        }

        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="data">The data.</param>
        public TwitterCard AddAdditionalInfo(string label, string data)
        {
            label.EnsureNotNullOrWhiteSpace(nameof(label));
            data.EnsureNotNullOrWhiteSpace(nameof(data));

            AdditionalInfo ??= [];
            AdditionalInfo[label] = data;
            return this;
        }

        /// <summary>
        /// Adds the additional information.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        public TwitterCard AddAdditionalInfo(Dictionary<string, string> additionalInfo)
        {
            additionalInfo.EnsureNotNull(nameof(additionalInfo));

            AdditionalInfo ??= [];
            foreach (var item in additionalInfo)
            {
                item.Value.EnsureNotNullOrWhiteSpace(nameof(item.Value));
                AdditionalInfo[item.Key] = item.Value;
            }
            return this;
        }

        /// <summary>
        /// Creates an instance for summary card.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="site">The site.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="imageAlt">Alt of the image.</param>
        public static TwitterCard CreateSummary(string title, string description, string site = null,
            string imageUrl = null, int? imageWidth = null, int? imageHeight = null, string imageAlt = null)
        {
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            description.EnsureNotNullOrWhiteSpace(nameof(description));

            return new TwitterCard
            {
                CardType = TwitterCardType.Summary,
                Site = site,
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                ImageWidth = imageWidth,
                ImageHeight = imageHeight,
                ImageAlt = imageAlt
            };
        }

        /// <summary>
        /// Creates an instance for summary_large_image card.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="site">The site.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="imageAlt">Alt of the image.</param>
        public static TwitterCard CreateSummaryLargeImage(string title, string description, string site = null,
            string imageUrl = null, int? imageWidth = null, int? imageHeight = null, string imageAlt = null)
        {
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            description.EnsureNotNullOrWhiteSpace(nameof(description));

            return new TwitterCard
            {
                CardType = TwitterCardType.SummaryLargeImage,
                Site = site,
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                ImageWidth = imageWidth,
                ImageHeight = imageHeight,
                ImageAlt = imageAlt
            };
        }

        /// <summary>
        /// Creates an instance for player card.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="title">The title.</param>
        /// <param name="playerUrl">The player URL.</param>
        /// <param name="playerWidth">Width of the video.</param>
        /// <param name="playerHeight">Height of the video.</param>
        /// <param name="description">The description of video.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="imageAlt">Alt of the image.</param>
        public static TwitterCard CreatePlayer(string site, string title, string playerUrl, int playerWidth, int playerHeight,
            string description = null, string imageUrl = null, int? imageWidth = null, int? imageHeight = null, string imageAlt = null)
        {
            site.EnsureNotNullOrWhiteSpace(nameof(site));
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            playerUrl.EnsureNotNullOrWhiteSpace(nameof(playerUrl));

            return new TwitterCard
            {
                CardType = TwitterCardType.Player,
                Site = site,
                Title = title,
                PlayerUrl = playerUrl,
                PlayerWidth = playerWidth,
                PlayerHeight = playerHeight,
                Description = description,
                ImageUrl = imageUrl,
                ImageWidth = imageWidth,
                ImageHeight = imageHeight,
                ImageAlt = imageAlt,
            };
        }
        #endregion

        #region Examples
        /*
        <meta name="twitter:title" content="@Model.PageTitle" />
        <meta name="twitter:description" content="@Model.Description" />
        <meta name="twitter:url" content="@Model.PageUrl" />
        <meta name="twitter:site" content="@Model.SiteTwitterId" />
        <meta name="twitter:card" content="@Model.TwitterCard.Value.ToString().ToLower()" /> (summary - summary_large_image - product - photo & gallery(deprecated))

        <meta name="twitter:image" content="@Model.ImageUrl" />
        <meta name="twitter:image:width" content="@Model.ImageWidth().Value" />
        <meta name="twitter:image:height" content="@Model.ImageHeight().Value" />
        <meta name="twitter:image:alt" content="@Model.PageTitle">

        <!-- for product -->
        <meta name="twitter:card" content="product">
        <meta name="twitter:label1" content="قیمت به ریال">
        <meta name="twitter:data1" content="19500">
        <meta name="twitter:data2" content="مشکی">
        <meta name="twitter:label2" content="رنگ">

        <!-- for player -->
        <meta name="twitter:player" content="@Model.VideoUrl"> <!-- HTTPS URL to iFrame player -->
        <meta name="twitter:player:width" content="@Model.VideoWidth"> <!-- Width of video pixels -->
        <meta name="twitter:player:height" content="@Model.VideoHeight"> <!-- Height of video pixels -->
        <meta name="twitter:player:stream" content="@Model.VideoStreamUrl"> <!-- URL to raw video or audio stream -->
        <meta name="twitter:image" content="@Model.VideoImageUrl"> <!-- Image to be displayed in place of the player on platforms that don’t support iFrames or inline players -->
        <meta name="twitter:image:alt" content="@Model.VideoImageAlt">

        <meta name="twitter:creator" content="@Model.AuthorTwitterId" /> @* (link or twitter id or name) *@
        <meta name="twitter:label1" content="زمان تقریبی برای خواندن">
        <meta name="twitter:data1" content="16 دقیقه">
        <meta name="twitter:label1" content="نویسنده" />
        <meta name="twitter:data1" content="محمدجواد ابراهیمی" />

        <meta name="twitter:card" content="app">
        <meta name="twitter:app:id:googleplay" content="com.digikala">
        https://developer.twitter.com/en/docs/twitter-for-websites/cards/overview/app-card
        <meta name="twitter:widgets:new-embed-design" content="on">
        */
        #endregion
    }
}
