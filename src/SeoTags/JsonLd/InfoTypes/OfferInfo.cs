using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// An offer for a product.
    /// </summary>
    /// <seealso cref="ThingInfo{Offer}" />
    public class OfferInfo : ThingInfo<Offer>
    {
        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the currency symbol of the price. (e.g "USD")
        /// </summary>
        public string PriceCurrency { get; set; }

        /// <summary>
        /// Gets or sets the availability of this item. (e.g InStock)
        /// </summary>
        public ItemAvailability? Availability { get; set; }

        /// <summary>
        /// Gets or sets the condition of this item. (e.g UsedCondition)
        /// </summary>
        public OfferItemCondition? ItemCondition { get; set; }

        /// <summary>
        /// Gets or sets the date after which the price is no longer available.
        /// </summary>
        public DateTime? PriceValidUntil { get; set; }

        //public ? Seller { get; set; }
        //public ? OfferedBy { get; set; }
        //public DateTimeOffset? ValidFrom { get; set; }
        //public DateTimeOffset? ValidThrough { get; set; }

        /// <summary>
        /// Converts to <see cref="Offer"/>.
        /// </summary>
        /// <returns>A <see cref="Offer"/> instance</returns>
        public override Offer ConvertTo()
        {
            if (Price < 0)
                throw new ArgumentException("Price can not be less than zero.");
            PriceCurrency.EnsureNotNullOrWhiteSpace(nameof(PriceCurrency));

            return new()
            {
                Url = Url.ToUri(), //?? Product.Url,
                Price = Price,
                PriceCurrency = PriceCurrency,
                Availability = Availability,
                ItemCondition = ItemCondition,
                PriceValidUntil = PriceValidUntil,
                //ValidFrom = ValidFrom,
                //ValidThrough = ValidThrough,
                ////AvailabilityStarts = ValidFrom,
                ////AvailabilityEnds = ValidThrough

                //Seller = organizationRefId, //new Organization { Name = organizationName }, //Organization //Person
                //OfferedBy = organizationRefId, //new Organization { Name = organizationName }, //Organization //Person
            };
        }
    }
}
