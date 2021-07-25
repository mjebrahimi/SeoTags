using Schema.NET;

namespace SeoTags
{
    /// <summary>
    /// A search action
    /// </summary>
    /// <seealso cref="ThingInfo{SearchAction}" />
    public class SearchActionInfo : ThingInfo<SearchAction>
    {
        /// <summary>
        /// Gets or sets the target url. (e.g "https://site.com/?s={search_term_string}")
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the query input search parameter. (e.g "required name=search_term_string")
        /// </summary>
        public string QueryInput { get; set; }

        /// <summary>
        /// Converts to <see cref="SearchAction"/>.
        /// </summary>
        /// <returns>A <see cref="SearchAction"/> instance</returns>
        public override SearchAction ConvertTo()
        {
            Target.EnsureNotNullOrWhiteSpace(nameof(Target));
            QueryInput.EnsureNotNullOrWhiteSpace(nameof(QueryInput));

            return new()
            {
                Target = Target.ToUri(),
                QueryInput = QueryInput
            };
        }
    }
}
