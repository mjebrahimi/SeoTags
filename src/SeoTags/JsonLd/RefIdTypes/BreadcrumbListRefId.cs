using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// Reference to a BreadcrumbListRefId
    /// </summary>
    public class BreadcrumbListRefId : BreadcrumbList
    {
        /// <inheritdoc/>
        public override string Type => null;

        /// <inheritdoc/>
        public override JsonLdContext Context => null;

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public BreadcrumbListRefId(Uri id)
        {
            id.EnsureNotNull(nameof(id));
            Id = id;
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public BreadcrumbListRefId(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            Id = id.ToUri();
        }
    }
}
