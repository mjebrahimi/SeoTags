namespace SeoTags
{
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
            url.EnsureNotNullOrWhiteSpace(nameof(url));
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
}
