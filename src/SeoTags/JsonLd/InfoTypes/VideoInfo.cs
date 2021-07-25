using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// An video file.
    /// </summary>
    /// <seealso cref="ThingInfo{VideoObject}" />
    public class VideoInfo : ThingInfo<VideoObject>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/video-url/#video")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#video"); }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail image URL.
        /// </summary>
        public string ThumbnailImageUrl { get; set; }

        /// <summary>
        /// A URL pointing to a player for a specific video. In general, this is the information in the `src` element of an `embed` tag and should not be the same as the content of the `loc` tag.
        /// </summary>
        public string EmbedUrl { get; set; }

        /// <summary>
        /// Gets or sets the duration seconds.
        /// </summary>
        public int? DurationSeconds { get; set; }

        /// <summary>
        /// Gets or sets the upload date.
        /// </summary>
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the number of times the video has been watched.
        /// </summary>
        public int? WatchedCount { get; set; }

        /// <summary>
        /// Gets or sets the author. (an instance of PersonInfo or PersonInfo.ReferTo method)
        /// </summary>
        public PersonInfo Author { get; set; }

        /// <summary>
        /// Gets or sets the publisher. (an instance of OrganizationInfo or OrganizationInfo.ReferTo method)
        /// </summary>
        public OrganizationInfo Publisher { get; set; }

        /// <summary>
        /// Converts to <see cref="VideoObject"/>.
        /// </summary>
        /// <returns>A <see cref="VideoObject"/> instance</returns>
        public override VideoObject ConvertTo()
        {
            if (referId is not null)
                return new VideoObjectRefId(referId);

            url.EnsureNotNull(nameof(Url));
            Title.EnsureNotNullOrWhiteSpace(nameof(Title));
            Description.EnsureNotNullOrWhiteSpace(nameof(Description));
            //UploadDate.EnsureNotNull(nameof(UploadDate));

            var video = new VideoObject()
            {
                Id = id,
                Url = url,
                ContentUrl = url,
                Name = Title,
                Description = Description,
                ThumbnailUrl = ThumbnailImageUrl?.ToUri(),
                EmbedUrl = EmbedUrl?.ToUri(),
                Duration = TimeSpan.FromSeconds(DurationSeconds ?? 0),
                UploadDate = UploadDate,
                Author = Author?.ConvertTo(),
                Publisher = Publisher?.ConvertTo(),
                Genre = Genre,

                //https://developers.google.com/search/docs/data-types/video
                //RegionsAllowed = default,
                //Expires = default,
                //Publication = default,
                //HasPart = default,
                //PotentialAction = default,
            };
            if (WatchedCount is not null)
            {
                video.InteractionStatistic = new InteractionCounter
                {
                    InteractionType = new WatchAction(),
                    UserInteractionCount = WatchedCount
                };
            }
            return video;
        }

        /// <summary>
        /// Refers to an video.
        /// </summary>
        /// <param name="videoInfo">The VideoInfo instance.</param>
        /// <returns>VideoInfo</returns>
        public static VideoInfo ReferTo(VideoInfo videoInfo)
        {
            videoInfo.EnsureNotNull(nameof(videoInfo));
            videoInfo.id.EnsureNotNull(nameof(videoInfo.Id));
            return new() { referId = videoInfo.id };
        }

        /// <summary>
        /// Refers to an video.
        /// </summary>
        /// <param name="id">The identifier. (VideoInfo.Id)</param>
        /// <returns>VideoInfo</returns>
        public static VideoInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="VideoInfo"/> to refers to the video.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>VideoInfo</returns>
        public static implicit operator VideoInfo(string id) => ReferTo(id);
    }
}
