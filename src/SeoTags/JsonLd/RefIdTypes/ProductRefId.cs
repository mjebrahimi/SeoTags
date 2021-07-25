using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// Reference to an Product
    /// </summary>
    public class ProductRefId : Product
    {
        /// <inheritdoc/>
        public override string Type => null;

        /// <inheritdoc/>
        public override JsonLdContext Context => null;

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public ProductRefId(Uri id)
        {
            id.EnsureNotNull(nameof(id));
            Id = id;
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public ProductRefId(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            Id = id.ToUri();
        }
    }
}
