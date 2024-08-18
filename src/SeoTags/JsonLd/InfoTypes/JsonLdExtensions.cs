using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    internal static class JsonLdExtensions
    {
        /// <summary>
        /// Converts <see cref="IEnumerable{OfferInfo}"/> to <see cref="AggregateOffer"/> or <see cref="Offer"/> (if has one).
        /// </summary>
        /// <param name="offers">The offers.</param>
        /// <param name="returnOfferIfHasOne">If has one offer, returns <see cref="Offer"/> instead of <see cref="AggregateOffer"/>.</param>
        /// <returns>An Aggregate<see cref="Offer"/> instance</returns>
        internal static Offer ToAggregateOffer(this IEnumerable<OfferInfo> offers, bool returnOfferIfHasOne = false)
        {
            var offerCount = offers?.Count();

            if (offerCount is null || offerCount == 0)
                return null;

            if (offerCount == 1 && returnOfferIfHasOne)
                return offers.ElementAt(0).ConvertTo();

            var currencies = offers.Select(p => p.PriceCurrency).Distinct().ToList();
            if (currencies.Count > 1)
                throw new InvalidOperationException("There are different currencies.");

            return new AggregateOffer
            {
                PriceCurrency = currencies[0],
                LowPrice = offers.Min(p => p.Price),
                HighPrice = offers.Max(p => p.Price),
                OfferCount = offerCount,
                Offers = new(offers.Select(p => p.ConvertTo())),

                //ItemOffered = default
                //Seller = default
                //OfferedBy = default
            };
        }

        /// <summary>
        /// Converts to <see cref="IEnumerable{ReviewInfo}"/> to <see cref="AggregateRating"/>.
        /// </summary>
        /// <param name="reviews">The reviews.</param>
        /// <returns>An <see cref="AggregateRating"/> instance</returns>
        public static AggregateRating ToAggregateRating(this IEnumerable<ReviewInfo> reviews)
        {
            var offerCount = reviews?.Count();

            if (offerCount is null || offerCount == 0)
                return null;

            var bestRatingList = reviews.Select(p => p.BestRating).Distinct().ToList();
            if (bestRatingList.Count > 1)
                throw new InvalidOperationException("The best rating values are not the same.");

            var worstRatingList = reviews.Select(p => p.WorstRating).Distinct().ToList();
            if (worstRatingList.Count > 1)
                throw new InvalidOperationException("The worst rating values are not the same.");

            var bestRating = bestRatingList[0];
            var worstRating = worstRatingList[0];
            var ratingValue = reviews.DefaultIfEmpty().Average(p => p.RatingValue);
            var ratingCount = reviews.Count();

            if (ratingValue > bestRating)
                throw new ArgumentException("Rating value can not be grater than best rating value.");
            if (ratingValue < worstRating)
                throw new ArgumentException("Rating value can not be less than worst rating value.");

            return new()
            {
                //Id = Id?.ToUri(), //id
                RatingValue = ratingValue,
                BestRating = bestRating,
                WorstRating = worstRating,
                RatingCount = ratingCount,
                ReviewCount = ratingCount,
            };
        }
    }
}
