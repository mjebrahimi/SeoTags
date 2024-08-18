using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// A BreadcrumbList is an ItemList consisting of a chain of linked Web pages, typically
    /// described using at least their URL and their name, and typically ending with the current page.
    /// </summary>
    /// <seealso cref="ThingInfo{BreadcrumbList}" />
    public class BreadcrumbInfo : ThingInfo<BreadcrumbList>
    {
        //public BreadcrumbInfo(params (string Url, string Name)[] items)
        //{
        //    items.EnsureNotNull(nameof(items));
        //    Items = items;
        //}

        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/#breadcrumb")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#breadcrumb"); }

        /// <summary>
        /// Gets or sets the items. (ItemListElement property and NumberOfItems based on its count)
        /// </summary>
        public IEnumerable<(string Url, string Name)> Items { get; set; }

        /// <summary>
        /// Converts to <see cref="BreadcrumbList"/>.
        /// </summary>
        /// <returns>A <see cref="BreadcrumbList"/> instance</returns>
        public override BreadcrumbList ConvertTo()
        {
            if (referId is not null)
                return new BreadcrumbListRefId(referId);

            Items.EnsureNotNull(nameof(Items));

            var itemListElement = Items?.Select((item, @index) =>
            {
                item.Name.EnsureNotNullOrWhiteSpace(nameof(item.Name));
                item.Url.EnsureNotNullOrWhiteSpace(nameof(item.Url));

                var uri = item.Url.ToUri();
                return new ListItem
                {
                    Position = @index + 1,
                    //Id = url.Relative("#webpage"),
                    //Url = url,
                    //Name = item.Name,
                    Item = new WebPage
                    {
                        Id = uri.Relative("#webpage"),
                        Url = uri,
                        Name = item.Name,
                    }
                };
            });

            return new()
            {
                Id = id,
                //Url = url,
                NumberOfItems = Items?.Count(),
                ItemListElement = new(itemListElement),
            };
        }

        /// <summary>
        /// Refers to a breadcrumb.
        /// </summary>
        /// <param name="breadcrumbInfo">The BreadcrumbInfo instance.</param>
        /// <returns>BreadcrumbInfo</returns>
        public static BreadcrumbInfo ReferTo(BreadcrumbInfo breadcrumbInfo)
        {
            breadcrumbInfo.EnsureNotNull(nameof(breadcrumbInfo));
            breadcrumbInfo.id.EnsureNotNull(nameof(breadcrumbInfo.Id));
            return new() { referId = breadcrumbInfo.id };
        }

        /// <summary>
        /// Refers to a breadcrumb.
        /// </summary>
        /// <param name="id">The identifier. (BreadcrumbInfo.Id)</param>
        /// <returns>BreadcrumbInfo</returns>
        public static BreadcrumbInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="BreadcrumbInfo"/> to refers to the breadcrumb.
        /// </summary>
        /// <param name="id">The identifier. (BreadcrumbInfo.Id)</param>
        /// <returns>BreadcrumbInfo</returns>
        public static implicit operator BreadcrumbInfo(string id) => ReferTo(id);
    }
}
