using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// A review.
    /// </summary>
    /// <seealso cref="ThingInfo{Review}" />
    public class ReviewInfo : ThingInfo<Review>
    {
        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the author URL.
        /// </summary>
        public string AuthorUrl { get; set; }

        /// <summary>
        /// Gets or sets the description. (and ReviewBody property)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date published. (and DateCreated property)
        /// </summary>
        public DateTimeOffset? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the rating value.
        /// </summary>
        public double? RatingValue { get; set; }

        /// <summary>
        /// The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.
        /// </summary>
        public double? BestRating { get; set; }

        /// <summary>
        /// The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.
        /// </summary>
        public double? WorstRating { get; set; }

        /// <summary>
        /// The item that is being reviewed/rated. (an instance of ProductInfo or ProductInfo.ReferTo method)
        /// </summary>
        public ProductInfo ItemReviewed { get; set; }

        /// <summary>
        /// Converts to <see cref="Review"/>.
        /// </summary>
        /// <returns>A <see cref="Review"/> instance</returns>
        public override Review ConvertTo()
        {
            AuthorName.EnsureNotNullOrWhiteSpace(nameof(AuthorName));
            Description.EnsureNotNullOrWhiteSpace(nameof(Description));

            var review = new Review
            {
                Description = Description,
                ReviewBody = Description,
                DatePublished = DatePublished,
                DateCreated = DatePublished,
                //DateModified = DateTimeOffset?, 
                ItemReviewed = ItemReviewed?.ConvertTo(),
                Author = new Person
                {
                    Name = AuthorName,
                    Url = AuthorUrl?.ToUri(),
                },
            };

            if (RatingValue is not null)
            {
                review.ReviewRating = new Rating
                {
                    RatingValue = RatingValue,
                    BestRating = BestRating,
                    WorstRating = WorstRating,
                };
            }

            return review;
        }
    }
}