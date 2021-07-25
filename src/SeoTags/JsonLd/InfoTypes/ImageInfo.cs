using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// An image file
    /// </summary>
    /// <seealso cref="ThingInfo{ImageObject}" />
    public class ImageInfo : ThingInfo<ImageObject>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/image.jpg#image")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url. (and ContentUrl property)
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#image"); }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the language of the content. (e.g "en-US")
        /// </summary>
        public string InLanguage { get; set; }

        /// <summary>
        /// Converts to <see cref="ImageObject"/>.
        /// </summary>
        /// <returns>An <see cref="ImageObject"/> instance</returns>
        public override ImageObject ConvertTo()
        {
            if (referId is not null)
                return new ImageObjectRefId(referId);

            url.EnsureNotNull(nameof(Url));

            return new ImageObject
            {
                Id = id,
                Url = url,
                ContentUrl = url,
                Width = Width?.ToString(),
                Height = Height?.ToString(),
                Caption = Caption,
                InLanguage = InLanguage,
            };
        }

        /// <summary>
        /// Refers to an image.
        /// </summary>
        /// <param name="imageInfo">The ImageInfo instance.</param>
        /// <returns>ImageInfo</returns>
        public static ImageInfo ReferTo(ImageInfo imageInfo)
        {
            imageInfo.EnsureNotNull(nameof(imageInfo));
            imageInfo.id.EnsureNotNull(nameof(imageInfo.Id));
            return new() { referId = imageInfo.id };
        }

        /// <summary>
        /// Refers to an image.
        /// </summary>
        /// <param name="id">The identifier. (ImageInfo.Id)</param>
        /// <returns>ImageInfo</returns>
        public static ImageInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="ImageInfo"/> to refers to the image.
        /// </summary>
        /// <param name="id">The identifier. (ImageInfo.Id)</param>
        /// <returns>ImageInfo</returns>
        public static implicit operator ImageInfo(string id) => ReferTo(id);
    }
}
