using System.ComponentModel.DataAnnotations;

namespace SeoTags
{
    /// <summary>
    /// Type of open graph
    /// </summary>
    public enum OpenGraphType
    {
        /// <summary>
        /// og:type => website
        /// </summary>
        [Display(Name = "website")]
        Website,

        /// <summary>
        /// og:type => article
        /// </summary>
        [Display(Name = "article")]
        Article,

        /// <summary>
        /// og:type => product
        /// </summary>
        [Display(Name = "product")]
        Product,

        /// <summary>
        /// og:type => video
        /// </summary>
        [Display(Name = "video.other")] //video.movie, video.tv_show, video.other (these are identical to video.movie) - video.episode
        Video,

        /// <summary>
        /// og:type => audio
        /// </summary>
        [Display(Name = "music.radio_station")] //music.song, music.album, music.playlist, music.radio_station
        Audio,

        /// <summary>
        /// og:type => book
        /// </summary>
        [Display(Name = "book")]
        Book
    }
}
