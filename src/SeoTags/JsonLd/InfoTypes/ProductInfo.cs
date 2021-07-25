using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// A product
    /// </summary>
    /// <seealso cref="ThingInfo{Product}" />
    public class ProductInfo : ThingInfo<Product>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/product-url/#product")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#product"); }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alias for the item.
        /// </summary>
        public string AlternateName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the identifier. (Sku and Mpn property)
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the images. (instances of ImageInfo or ImageInfo.ReferTo method)
        /// </summary>
        public IEnumerable<ImageInfo> Images { get; set; }

        /// <summary>
        /// Gets or sets the brand. (BrandInfo or refer to OrganizationInfo)
        /// </summary>
        public BrandInfo Brand { get; set; }

        /// <summary>
        /// Gets or sets the offers.
        /// </summary>
        public IEnumerable<OfferInfo> Offers { get; set; }

        /// <summary>
        /// Gets or sets the reviews.
        /// </summary>
        public IEnumerable<ReviewInfo> Reviews { get; set; }

        /// <summary>
        /// Gets or sets the overall rating, based on a collection of reviews or ratings.
        /// </summary>
        public AggregateRatingInfo AggregateRating { get; set; }

        /// <summary>
        /// Converts to <see cref="Product"/>.
        /// </summary>
        /// <returns>A <see cref="Product"/> instance</returns>
        public override Product ConvertTo()
        {
            if (referId is not null)
                return new ProductRefId(referId);

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
                Category = Category,
                Sku = Identifier,
                Mpn = Identifier,
                Image = new(Images?.Select(p => p.ConvertTo())),
                Brand = new(Brand?.ConvertTo()),
                Offers = Offers?.ToAggregateOffer(true),
                AggregateRating = AggregateRating?.ConvertTo() ?? Reviews?.ToAggregateRating(),
                Review = new(Reviews?.Select(p => p.ConvertTo())),

                //Identifier = Identifier,
                //Audience = default,
                //IsRelatedTo = default,
                //IsSimilarTo = default,
                //Width = default,
                //Height = default,
                //Weight = default,
                //Color = default
                //ProductID = default,
                //Manufacturer = default,
            };
        }

        /// <summary>
        /// Refers to a product.
        /// </summary>
        /// <param name="productInfo">The ProductInfo instance.</param>
        /// <returns>ProductInfo</returns>
        public static ProductInfo ReferTo(ProductInfo productInfo)
        {
            productInfo.EnsureNotNull(nameof(productInfo));
            productInfo.id.EnsureNotNull(nameof(productInfo.Id));
            return new() { referId = productInfo.id };
        }

        /// <summary>
        /// Refers to a product.
        /// </summary>
        /// <param name="id">The identifier. (ProductInfo.Id)</param>
        /// <returns>ProductInfo</returns>
        public static ProductInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="ProductInfo"/> to refers to the product.
        /// </summary>
        /// <param name="id">The identifier. (ProductInfo.Id)</param>
        /// <returns>ProductInfo</returns>
        public static implicit operator ProductInfo(string id) => ReferTo(id);
    }
}
