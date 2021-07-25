using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// The average rating based on multiple ratings or reviews.
    /// </summary>
    /// <seealso cref="ThingInfo{AggregateRating}" />
    public class AggregateRatingInfo : ThingInfo<AggregateRating>
    {
        /// <summary>
        /// Gets or sets the rating value.
        /// </summary>
        public double RatingValue { get; set; }

        /// <summary>
        /// The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.
        /// </summary>
        public double? BestRating { get; set; }

        /// <summary>
        /// The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.
        /// </summary>
        public double? WorstRating { get; set; }

        /// <summary>
        /// The count of total number of ratings/reviews.
        /// </summary>
        public int RatingCount { get; set; }

        /// <summary>
        /// The item that is being reviewed/rated.
        /// </summary>
        public ProductInfo ItemReviewed { get; set; }

        /// <summary>
        /// Converts to <see cref="AggregateRating"/>.
        /// </summary>
        /// <returns>An <see cref="AggregateRating"/> instance</returns>
        public override AggregateRating ConvertTo()
        {
            if (RatingValue > BestRating)
                throw new ArgumentException("Rating value can not be grater than best rating value.");
            if (RatingValue < WorstRating)
                throw new ArgumentException("Rating value can not be less than worst rating value.");

            return new()
            {
                RatingValue = RatingValue,
                BestRating = BestRating,
                WorstRating = WorstRating,
                RatingCount = RatingCount,
                ReviewCount = RatingCount,
                ItemReviewed = ItemReviewed?.ConvertTo()
            };
        }
    }
}
