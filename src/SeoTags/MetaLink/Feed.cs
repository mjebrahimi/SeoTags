namespace SeoTags
{
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
        public Feed(string title, string url, FeedType feedType = FeedType.Rss)
        {
            title.EnsureNotNullOrWhiteSpace(nameof(title));
            url.EnsureNotNullOrWhiteSpace(nameof(url));
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
        /// Gets or sets the type of the feed. (default: RSS)
        /// </summary>
        public FeedType FeedType { get; set; }
    }
}
