using Schema.NET;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SeoTags
{
    /// <summary>
    /// A JSON-LD graph.
    /// </summary>
    public class JsonLdGraph : Thing
    {
        /// <inheritdoc/>
        public override string Type => null;

        /// <summary>
        /// Gets or sets the things.
        /// </summary>
        [DataMember(Name = "@graph", Order = 1)]
        public IEnumerable<IThing> Things { get; set; }
    }
}
