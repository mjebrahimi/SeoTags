using System.ComponentModel.DataAnnotations;

namespace SeoTags
{
    /// <summary>
    /// Types of twitter:card
    /// </summary>
    public enum TwitterCardType
    {
        /// <summary>
        /// summary
        /// </summary>
        [Display(Name = "summary")]
        Summary,

        /// <summary>
        /// summary_large_image
        /// </summary>
        [Display(Name = "summary_large_image")]
        SummaryLargeImage,

        /// <summary>
        /// player
        /// </summary>
        [Display(Name = "player")]
        Player,

        /// <summary>
        /// product
        /// </summary>
        [Display(Name = "product")]
        Product,
    }
}
