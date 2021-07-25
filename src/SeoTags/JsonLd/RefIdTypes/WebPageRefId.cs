using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// Reference to a WebPage
    /// </summary>
    public class WebPageRefId : WebPage
    {
        /// <inheritdoc/>
        public override string Type => null;

        /// <inheritdoc/>
        public override JsonLdContext Context => null;

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public WebPageRefId(Uri id)
        {
            id.EnsureNotNull(nameof(id));
            Id = id;
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public WebPageRefId(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            Id = id.ToUri();
        }
    }
}
