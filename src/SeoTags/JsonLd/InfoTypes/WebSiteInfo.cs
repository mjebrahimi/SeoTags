using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// A website.
    /// </summary>
    /// <seealso cref="ThingInfo{WebSite}" />
    public class WebSiteInfo : ThingInfo<WebSite>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/#website")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#website"); }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alias of the item.
        /// </summary>
        public string AlternateName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the language of the content. (e.g "en-US")
        /// </summary>
        public string InLanguage { get; set; }

        /// <summary>
        /// Gets or sets the search action. (PotentialAction property)
        /// </summary>
        public SearchActionInfo SearchAction { get; set; }

        /// <summary>
        /// Gets or sets the publisher organization. (an instance of OrganizationInfo or OrganizationInfo.ReferTo method)
        /// </summary>
        public OrganizationInfo Publisher { get; set; }

        /// <summary>
        /// Converts to <see cref="WebSite"/>.
        /// </summary>
        /// <returns>A <see cref="WebSite"/> instance</returns>
        public override WebSite ConvertTo()
        {
            if (referId is not null)
                return new WebSiteRefId(referId);

            url.EnsureNotNull(nameof(Url));
            Name.EnsureNotNullOrWhiteSpace(nameof(Name));
            Description.EnsureNotNullOrWhiteSpace(nameof(Description));

            return new()
            {
                Id = id,
                Url = url,
                Name = Name,
                AlternateName = AlternateName,
                Description = Description,
                InLanguage = InLanguage,
                PotentialAction = SearchAction?.ConvertTo(),
                Publisher = Publisher?.ConvertTo()
            };
        }

        /// <summary>
        /// Refers to a website.
        /// </summary>
        /// <param name="webSiteInfo">The WebSiteInfo instance.</param>
        /// <returns>WebSiteInfo</returns>
        public static WebSiteInfo ReferTo(WebSiteInfo webSiteInfo)
        {
            webSiteInfo.EnsureNotNull(nameof(webSiteInfo));
            webSiteInfo.id.EnsureNotNull(nameof(webSiteInfo.Id));
            return new() { referId = webSiteInfo.id };
        }

        /// <summary>
        /// Refers to a website.
        /// </summary>
        /// <param name="id">The identifier. (WebSiteInfo.Id)</param>
        /// <returns>WebSiteInfo</returns>
        public static WebSiteInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="WebSiteInfo"/> to refers to the website.
        /// </summary>
        /// <param name="id">The identifier. (WebSiteInfo.Id)</param>
        /// <returns>WebSiteInfo</returns>
        public static implicit operator WebSiteInfo(string id) => ReferTo(id);
    }
}
