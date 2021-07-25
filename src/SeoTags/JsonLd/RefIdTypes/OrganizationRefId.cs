using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// Reference to an Organization
    /// </summary>
    public class OrganizationRefId : Organization
    {
        /// <inheritdoc/>
        public override string Type => null;

        /// <inheritdoc/>
        public override JsonLdContext Context => null;

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public OrganizationRefId(Uri id)
        {
            id.EnsureNotNull(nameof(id));
            Id = id;
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="id">The id reference</param>
        public OrganizationRefId(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            Id = id.ToUri();
        }
    }
}
