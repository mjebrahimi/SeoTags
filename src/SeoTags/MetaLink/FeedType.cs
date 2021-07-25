using System.ComponentModel.DataAnnotations;

namespace SeoTags
{
    /// <summary>
    /// Type of feed
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// The RSS xml
        /// </summary>
        [Display(Name = "application/rss+xml")]
        Rss,

        /// <summary>
        /// The atom xml
        /// </summary>
        [Display(Name = "application/atom+xml")]
        Atom
    }
}
