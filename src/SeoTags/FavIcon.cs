using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetZoom.Web.AppCode.SeoHelpers
{
    /// <summary>
    /// http://realfavicongenerator.net/
    /// <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png">
    /// <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png">
    /// <link rel="icon" type="image/png" sizes="192x192" href="/android-chrome-192x192.png">
    /// <link rel="apple-touch-icon" sizes="57x57" href="/apple-touch-icon-57x57.png">
    /// <link rel="apple-touch-icon" sizes="60x60" href="/apple-touch-icon-60x60.png">
    /// <link rel="apple-touch-icon" sizes="72x72" href="/apple-touch-icon-72x72.png">
    /// <link rel="apple-touch-icon" sizes="76x76" href="/apple-touch-icon-76x76.png">
    /// <link rel="apple-touch-icon" sizes="114x114" href="/apple-touch-icon-114x114.png">
    /// <link rel="apple-touch-icon" sizes="120x120" href="/apple-touch-icon-120x120.png">
    /// <link rel="apple-touch-icon" sizes="144x144" href="/apple-touch-icon-144x144.png">
    /// <link rel="apple-touch-icon" sizes="152x152" href="/apple-touch-icon-152x152.png">
    /// <link rel="apple-touch-icon" sizes="180x180" href="/apple-touch-icon-180x180.png">
    /// <link rel="manifest" href="/site.webmanifest">
    /// <link rel="mask-icon" href="/safari-pinned-tab.svg" color="#ffffff">
    /// <meta name="msapplication-TileColor" content="#ffffff">
    /// <meta name="msapplication-TileImage" content="/mstile-144x144.png">
    /// <meta name="theme-color" content="#ffffff">
    /// </summary>
    public class FavIcon
    {
        public string ShortcutIconUrl { get; set; }
        public string Icon16x16Url { get; set; }
        public string Icon32x32Url { get; set; }
        public string Icon96x96Url { get; set; }
        public string Icon192x192Url { get; set; }

        public string AppleTouchIcon57x57Url { get; set; }
        public string AppleTouchIcon60x60Url { get; set; }
        public string AppleTouchIcon72x72Url { get; set; }
        public string AppleTouchIcon76x76Url { get; set; }
        public string AppleTouchIcon114x114Url { get; set; }
        public string AppleTouchIcon120x120Url { get; set; }
        public string AppleTouchIcon144x144Url { get; set; }
        public string AppleTouchIcon152x152Url { get; set; }
        public string AppleTouchIcon167x167Url { get; set; }
        public string AppleTouchIcon180x180Url { get; set; }
        public string MaskIconUrl { get; set; } // /safari-pinned-tab.svg
        public string MaskIconColor { get; set; }
        public string ManifestJsonUrl { get; set; }

        public string ThemeColor { get; set; }
        public AppleMobileWebAppStatusBarStyle? AppleMobileWebAppStatusBarStyle { get; set; }
        public bool AppleMobileWebAppCapable { get; set; }
        public bool MobileWebAppCapable { get; set; }
        public string AppleMobileWebAppTitle { get; set; }
        public string ApplicationName { get; set; }

        public string MsApplicationNavbuttonColor { get; set; }
        public string MsApplicationTileColor { get; set; }
        public string MsApplicationTileImageUrl { get; set; }
        public string MsApplicationSquare70x70LogoUrl { get; set; }
        public string MsApplicationSquare150x150LogoUrl { get; set; }
        public string MsApplicationSquare310x310LogoUrl { get; set; }
        public string MsApplicationWide310x150LogoUrl { get; set; }
        public string MsApplicationConfigUrl { get; set; } // /browserconfig.xml

        public void Render(StringBuilder builder)
        {
            if (ShortcutIconUrl is not null)
                builder.Append("<link rel=\"shortcut icon\" type=\"").Append(Utils.GetContentType(ShortcutIconUrl)).Append("\" href=\"").Append(ShortcutIconUrl).AppendLine("\" />");

            if (Icon16x16Url is not null)
                builder.Append("<link rel=\"icon\" sizes=\"16x16\" type=\"").Append(Utils.GetContentType(Icon16x16Url)).Append("\" href=\"").Append(Icon16x16Url).AppendLine("\" />");
            if (Icon32x32Url is not null)
                builder.Append("<link rel=\"icon\" sizes=\"32x32\" type=\"").Append(Utils.GetContentType(Icon32x32Url)).Append("\" href=\"").Append(Icon32x32Url).AppendLine("\" />");
            if (Icon96x96Url is not null)
                builder.Append("<link rel=\"icon\" sizes=\"96x96\" type=\"").Append(Utils.GetContentType(Icon96x96Url)).Append("\" href=\"").Append(Icon96x96Url).AppendLine("\" />");
            if (Icon192x192Url is not null)
                builder.Append("<link rel=\"icon\" sizes=\"192x192\" type=\"").Append(Utils.GetContentType(Icon192x192Url)).Append("\" href=\"").Append(Icon192x192Url).AppendLine("\" />");

            if (AppleTouchIcon57x57Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"57x57\" href=\"").Append(AppleTouchIcon57x57Url).AppendLine("\" />");
            if (AppleTouchIcon60x60Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"60x60\" href=\"").Append(AppleTouchIcon60x60Url).AppendLine("\" />");
            if (AppleTouchIcon72x72Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"72x72\" href=\"").Append(AppleTouchIcon72x72Url).AppendLine("\" />");
            if (AppleTouchIcon76x76Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"76x76\" href=\"").Append(AppleTouchIcon76x76Url).AppendLine("\" />");
            if (AppleTouchIcon114x114Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"114x114\" href=\"").Append(AppleTouchIcon114x114Url).AppendLine("\" />");
            if (AppleTouchIcon120x120Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"120x120\" href=\"").Append(AppleTouchIcon120x120Url).AppendLine("\" />");
            if (AppleTouchIcon144x144Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"144x144\" href=\"").Append(AppleTouchIcon144x144Url).AppendLine("\" />");
            if (AppleTouchIcon152x152Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"152x152\" href=\"").Append(AppleTouchIcon152x152Url).AppendLine("\" />");
            if (AppleTouchIcon167x167Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"167x167\" href=\"").Append(AppleTouchIcon167x167Url).AppendLine("\" />");
            if (AppleTouchIcon180x180Url is not null)
                builder.Append("<link rel=\"apple-touch-icon\" sizes=\"180x180\" href=\"").Append(AppleTouchIcon180x180Url).AppendLine("\" />");
            if (MaskIconUrl is not null)
            {
                builder.Append("<link rel=\"mask-icon\" href=\"").Append(MaskIconUrl);
                if (MaskIconColor is not null)
                    builder.Append("\" color=\"").Append(MaskIconColor);
                builder.AppendLine("\" />");
            }
            if (ManifestJsonUrl is not null)
                builder.Append("<link rel=\"manifest\" href=\"").Append(ManifestJsonUrl).AppendLine("\" />");

            if (ThemeColor is not null)
                builder.Append("<meta name=\"theme-color\" content=\"").Append(ThemeColor).AppendLine("\" />");
            if (AppleMobileWebAppStatusBarStyle is not null)
                builder.Append("<meta name=\"apple-mobile-web-app-status-bar-style\" content=\"").Append(AppleMobileWebAppStatusBarStyle.Value.ToDisplay()).AppendLine("\" />");
            if (AppleMobileWebAppCapable is true)
                builder.Append("<meta name=\"apple-mobile-web-app-capable\" content=\"yes\" />");
            if (MobileWebAppCapable is true)
                builder.Append("<meta name=\"mobile-web-app-capable\" content=\"yes\" />");
            if (AppleMobileWebAppTitle is not null)
                builder.Append("<meta name=\"apple-mobile-web-app-title\" content=\"").Append(AppleMobileWebAppTitle).AppendLine("\" />");
            if (ApplicationName is not null)
                builder.Append("<meta name=\"application-name\" content=\"").Append(ApplicationName).AppendLine("\" />");

            if (MsApplicationNavbuttonColor is not null)
                builder.Append("<meta name=\"msapplication-navbutton-color\" content=\"").Append(MsApplicationNavbuttonColor).AppendLine("\" />");
            if (MsApplicationTileColor is not null)
                builder.Append("<meta name=\"msapplication-TileColor\" content=\"").Append(MsApplicationTileColor).AppendLine("\" />");
            if (MsApplicationTileImageUrl is not null)
                builder.Append("<meta name=\"msapplication-TileImage\" content=\"").Append(MsApplicationTileImageUrl).AppendLine("\" />");
            if (MsApplicationSquare70x70LogoUrl is not null)
                builder.Append("<meta name=\"msapplication-square70x70logo\" content=\"").Append(MsApplicationSquare70x70LogoUrl).AppendLine("\" />");
            if (MsApplicationSquare150x150LogoUrl is not null)
                builder.Append("<meta name=\"msapplication-square150x150logo\" content=\"").Append(MsApplicationSquare150x150LogoUrl).AppendLine("\" />");
            if (MsApplicationWide310x150LogoUrl is not null)
                builder.Append("<meta name=\"msapplication-wide310x150logo\" content=\"").Append(MsApplicationWide310x150LogoUrl).AppendLine("\" />");
            if (MsApplicationSquare310x310LogoUrl is not null)
                builder.Append("<meta name=\"msapplication-square310x310logo\" content=\"").Append(MsApplicationSquare310x310LogoUrl).AppendLine("\" />");
            if (MsApplicationConfigUrl is not null)
                builder.Append("<meta name=\"msapplication-config\" content=\"").Append(MsApplicationConfigUrl).AppendLine("\" />");
        }

        /*
        <link rel="shortcut icon" type="@Model.IconType" href="@Model.IconUrl" /> (Recommanded: image/x-icon)
        <link rel="shortcut icon" type="image/x-icon" href="@Model.IconUrl" />
        <link rel="icon" type="image/png" sizes="16x16" href="@Model.Icon16x16Url">
        <link rel="icon" type="image/png" sizes="32x32" href="@Model.Icon32x32Url">
        <link rel="icon" type="image/png" sizes="96x96" href="@Model.Icon96x96Url">
        <link rel="icon" type="image/png" sizes="192x192" href="@Model.Icon192x192Url">

        <link rel="apple-touch-icon" href="https://www.daneshjooyar.com/apple-icon.png"> <!-- max size -->
        <link rel="apple-touch-icon" sizes="57x57" href="@Model.Icon57x57Url">
        <link rel="apple-touch-icon" sizes="60x60" href="@Model.Icon60x60Url">
        <link rel="apple-touch-icon" sizes="72x72" href="@Model.Icon72x72Url">
        <link rel="apple-touch-icon" sizes="76x76" href="@Model.Icon76x76Url">
        <link rel="apple-touch-icon" sizes="114x114" href="@Model.Icon114x114Url">
        <link rel="apple-touch-icon" sizes="120x120" href="@Model.Icon120x120Url">
        <link rel="apple-touch-icon" sizes="144x144" href="@Model.Icon144x144Url">
        <link rel="apple-touch-icon" sizes="152x152" href="@Model.Icon152x152Url">
        <link rel="apple-touch-icon" sizes="167x167" href="@Model.Icon167x167Url">
        <link rel="apple-touch-icon" sizes="180x180" href="@Model.Icon180x180Url">
        <link rel="mask-icon" href="@Model.MaskIconUrl" color="@Model.MaskIconColorHex"> /safari-pinned-tab.svg color="#9f00a7"

        <link rel="manifest" href="@Model.ManifestJsonUrl">
        <link rel="manifest" href="https://www.daneshjooyar.com/manifest.json">
        <link rel="manifest" href="https://www.digikala.com/manifest.json?v=1.4">
        <link rel="manifest" href="https://ahrefs.com/blog/wp-content/themes/Ahrefs-4/images/favicons/site.webmanifest">

        https://stackoverflow.com/questions/26960703/how-to-change-the-color-of-header-bar-and-address-bar-in-newest-chrome-version-o 
        <meta name="theme-color" content="@Model.ThemeColorHex" /> (Chrome, Firefox OS and Opera) 
        https://medium.com/appscope/changing-the-ios-status-bar-of-your-progressive-web-app-9fc8fbe8e6ab
        https://codeburst.io/progressive-web-apps-customize-status-bar-23c4b2de590f
        <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" /> (iOS Safari - supported values seems color is not valid: black | black-translucent | default)
        https://stackoverflow.com/questions/4617073/meta-tag-apple-mobile-web-app-capable-for-android
        <meta name="apple-mobile-web-app-capable" content="yes">
        <meta name="mobile-web-app-capable" content="yes"> 
        <meta name="apple-mobile-web-app-title" content="@Model.SiteTitle">
        <meta name="application-name" content="@Model.SiteName">

        <meta name="msapplication-navbutton-color" content="@Model.ThemeColor" /> (Windows Phone - jaee nadidam estefade kone, faghat digikala) 
        <meta name="msapplication-TileColor" content="@Model.MsTileColorHex"> #9f00a7
        <meta name="msapplication-TileImage" content="@Model.IconUrl" />
        <meta name="msapplication-config" content="@Model.BrowserConfigXml" /> /browserconfig.xml

        https://webdesign.tutsplus.com/tutorials/how-to-add-windows-tiles-to-your-website--cms-23099
        <meta name="msapplication-square70x70logo" content="images/tiles/acme-tile-small.png" />
        <meta name="msapplication-square150x150logo" content="images/tiles/acme-tile-medium.png" />
        <meta name="msapplication-wide310x150logo" content="images/tiles/acme-tile-wide.png" />
        <meta name="msapplication-square310x310logo" content="images/tiles/acme-tile-large.png" />

        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-640x1136.png?v=v2.5.4" media="(device-width: 320px) and (device-height: 568px) and (-webkit-device-pixel-ratio: 2) and (orientation: portrait)">
        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-750x1294.png?v=v2.5.4" media="(device-width: 375px) and (device-height: 667px) and (-webkit-device-pixel-ratio: 2) and (orientation: portrait)">
        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-1242x2148.png?v=v2.5.4" media="(device-width: 414px) and (device-height: 736px) and (-webkit-device-pixel-ratio: 3) and (orientation: portrait)">
        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-1125x2436.png?v=v2.5.4" media="(device-width: 375px) and (device-height: 812px) and (-webkit-device-pixel-ratio: 3) and (orientation: portrait)">
        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-1536x2048.png?v=v2.5.4" media="(min-device-width: 768px) and (max-device-width: 1024px) and (-webkit-min-device-pixel-ratio: 2) and (orientation: portrait)">
        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-1668x2224.png?v=v2.5.4" media="(min-device-width: 834px) and (max-device-width: 834px) and (-webkit-min-device-pixel-ratio: 2) and (orientation: portrait)">
        <link rel="apple-touch-startup-image" href="https://virgool.io/images/pwa/ios/launch-2048x2732.png?v=v2.5.4" media="(min-device-width: 1024px) and (max-device-width: 1024px) and (-webkit-min-device-pixel-ratio: 2) and (orientation: portrait)">
        */
    }

    public enum AppleMobileWebAppStatusBarStyle
    {
        [Display(Name = "default")]
        Default,

        [Display(Name = "black")]
        Black,

        [Display(Name = "black-translucent")]
        BlackTranslucent
    }
}
