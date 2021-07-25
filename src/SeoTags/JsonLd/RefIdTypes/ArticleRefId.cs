using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// Reference to an Article
    /// </summary>
    public class ArticleRefId : Article
    {
        /// <inheritdoc/>
        public override string Type => null;

        /// <inheritdoc/>
        public override JsonLdContext Context => null;

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public ArticleRefId(Uri id)
        {
            id.EnsureNotNull(nameof(id));
            Id = id;
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public ArticleRefId(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            Id = id.ToUri();
        }
    }
}
