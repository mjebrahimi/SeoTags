using System.ComponentModel.DataAnnotations;

namespace SeoTags
{
    /// <summary>
    /// Type of preload (as attribute)
    /// </summary>
    public enum PreloadType
    {
        /// <summary>
        /// Audio file, as typically used in &lt;audio&gt;.
        /// </summary>
        [Display(Name = "audio")]
        Audio,

        /// <summary>
        /// An HTML document intended to be embedded by a &lt;frame&gt; or &lt;iframe&gt;.
        /// </summary>
        [Display(Name = "document")]
        Document,

        /// <summary>
        /// A resource to be embedded inside an &lt;embed&gt; element.
        /// </summary>
        [Display(Name = "embed")]
        Embed,

        /// <summary>
        /// Resource to be accessed by a fetch or XHR request, such as an ArrayBuffer or JSON file.
        /// </summary>
        [Display(Name = "fetch")]
        Fetch,

        /// <summary>
        /// Font file.
        /// </summary>
        [Display(Name = "font")]
        Font,

        /// <summary>
        /// Image file.
        /// </summary>
        [Display(Name = "image")]
        Image,

        /// <summary>
        /// A resource to be embedded inside an &lt;object&gt; element.
        /// </summary>
        [Display(Name = "object")]
        Object,

        /// <summary>
        /// JavaScript file.
        /// </summary>
        [Display(Name = "script")]
        Script,

        /// <summary>
        /// CSS stylesheet.
        /// </summary>
        [Display(Name = "style")]
        Style,

        /// <summary>
        /// WebVTT file.
        /// </summary>
        [Display(Name = "track")]
        Track,

        /// <summary>
        /// A JavaScript web worker or shared worker.
        /// </summary>
        [Display(Name = "worker")]
        Worker,

        /// <summary>
        /// Video file, as typically used in &lt;video&gt;.
        /// </summary>
        [Display(Name = "video")]
        Video
    }
}
