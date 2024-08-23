[![NuGet](https://img.shields.io/nuget/dt/SeoTags?style=flat&logo=nuget&cacheSeconds=1&label=Downloads)](https://www.nuget.org/packages/SeoTags)
[![NuGet](https://img.shields.io/nuget/v/SeoTags.svg)](https://www.nuget.org/packages/SeoTags)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://github.com/mjebrahimi/SeoTags/workflows/.NET/badge.svg)](https://github.com/mjebrahimi/SeoTags)

# SeoTags
SeoTags generates **All SEO Tags** you need such as **meta**, **link**, **Twitter card** (twitter:), **Open Graph (for Facebook)** (og:), and **JSON-LD** schema (structured data).

## How to use

See https://mjebrahimi.github.io/SeoTags/ for more info.

### 1. Install Package

```ini
PM> Install-Package SeoTags
```

### 2. Register/Configure to your Services

Everything you need to do is to configure the `SeoInfo` object and render it in your `_Layout.cshtml`.

This configuring can be done by setting the properties of the `SeoInfo` object in **three ways**:

1. When **registering your services** using `services.AddSeoTags(seoInfo => { ... })` method.
2. `Html.SetSeoInfo(seoInfo => { ... })` method in your `.cshtml` **views (Mvc or RazorPages)**
3. `HttpContext.SetSeoInfo(seoInfo => { ... })` method anywhere you access the `HttpContext` object (for example in your mvc **Controller**/**Action** or razor-pages **PageModel**)

There are general options that are constant for your entire website (not specific to a certain page), such as **Website Title**, **Twitter ID**, **Facebook ID**, **OpenSearch URL**, **feeds (RSS or Atom)**, etc...

Usually, these values are set when registering services using `services.AddSeoTags(seoInfo => { ... })` method.

```cs
//Register your services
app.Services.AddSeoTags(seoInfo =>
{
    seoInfo.SetSiteInfo(
        siteTitle: "My Site Title", 
        siteTwitterId: "@MySiteTwitter",                    //Optional
        siteFacebookId: "https://facebook.com/MySite",      //Optional
        openSearchUrl: "https://site.com/open-search.xml",  //Optional
        robots: "index, follow"                             //Optional
    );

    //Optional
    seoInfo.AddFeed(
        title: "Post Feeds",
        url: "https://site.com/rss/",
        feedType: FeedType.Rss);

    //Optional
    seoInfo.AddDnsPrefetch("https://fonts.gstatic.com/", "https://www.google-analytics.com");

    //Optional
    seoInfo.AddPreload(new Preload("https://site.com/site.css"),
        new Preload("https://site.com/app.js"),
        new Preload("https://site.com/fonts/Font.woff2"),
        new Preload("https://site.com/fonts/Font_Light.woff2"),
        new Preload("https://site.com/fonts/Font_Medium.woff2"),
        new Preload("https://site.com/fonts/Font_Bold.woff2"));

    //Optional
    seoInfo.SetLocales("en_US");
});
//...
```

### 3. Render SEO Tags in your _Layout.cshtml

To render the output SEO Tags call `Html.SeoTags()` method in your `_Layout.cshtml`.

This method has two overloads, one with a `SeoInfo` argument (if you need to pass a new arbitrary instance of the `SeoInfo` object), and one without an argument that retrieves the configured `SeoInfo` object from your previous registered services.

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Remove these tags from your _Layout.cshtml
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] -  Site Title</title>
    -->

    <!-- SeoTags generates all of these for you -->
    @Html.SeoTags() <!-- 👈 Add this line -->
```

### 4. Set Specific SEO info in your Views/Pages

There are some specific SEO info that you may want to set for a certain page, such as such as **Page Title**, **Page Description**, **Page Keywords**, **Page URL**, **Publish Date**, **Modified Date**, **Image Info**, **Page Type** etc...

To do this, call `Html.SetSeoInfo(seoInfo => { ... })` method in your `.cshtml` views to set specific desired SEO info for that page.

You can do the same by calling `HttpContext.SetSeoInfo(seoInfo => { ... })` anywhere you access to `HttpContext` object (for example in your mvc **Controller**/**Action** or razor-pages **PageModel**)

```csharp
@{
    // Remove these line from your views
    // ViewData["Title"] = "Page Title";

    Html.SetSeoInfo(seoInfo =>
    {
        seoInfo.SetCommonInfo(
            pageTitle: "SEO Tags for ASP.NET Core",
            description: "SetoTags creates all SEO tags you need such as meta, link, Twitter card (twitter:), open graph (og:), and ...",
            url: "https://site.com/url/",
            keywordTags: new[] { "SEO", "AspNetCore", "MVC", "RazorPages" }, //Optional
            seeAlsoUrls: new[] { "https://site.com/see-also-1", "https://site.com/see-also-2" }  //Optional
        );

        seoInfo.SetImageInfo(
            url: "https://site.com/uploads/image.jpg",
            width: 1280,                                        //Optional
            height: 720,                                        //Optional
            alt: "Image alt",                                   //Optional
            //mimeType: "image/jpeg",                           //Optional (detects from URL file extension if not set.)
            cardType: SeoTags.TwitterCardType.SummaryLargeImage //Optional
        );

        seoInfo.SetArticleInfo(
            authorName: "Author Name",
            publishDate: DateTimeOffset.Now,
            modifiedDate: DateTimeOffset.Now,                   //Optional
            authorTwitterId: "@MyTwitterId",                    //Optional
            authorFacebookId: "https://facebook.com/MyUserId",  //Optional
            authorUrl: "https://github.com/author-profile",     //Optional
            section: "Article Topic"                            //Optional
        );

        //Add another RSS feed. (only for this page) (Optional)
        seoInfo.AddFeed("Post Comments", "https://site.com/post/comment/rss", SeoTags.FeedType.Rss);
    });
}
```

### 5. Done! Enjoy the Renderd Output

Open your page in a browser and view the source code.

The following code shows the rendered output for this example.

```html
<!DOCTYPE html>
<html lang="en">
<head>

<meta charset="utf-8" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1" />
<meta name="viewport" content="width=device-width, initial-scale=1" />

<link rel="preconnect" href="https://fonts.gstatic.com/" crossorigin />
<link rel="preconnect" href="https://www.google-analytics.com" crossorigin />
<link rel="dns-prefetch" href="https://fonts.gstatic.com/" />
<link rel="dns-prefetch" href="https://www.google-analytics.com" />
<link rel="preload" as="style" href="https://site.com/site.css" />
<link rel="preload" as="script" href="https://site.com/app.js" />
<link rel="preload" as="font" type="font/woff2" href="https://site.com/fonts/Font.woff2" crossorigin />
<link rel="preload" as="font" type="font/woff2" href="https://site.com/fonts/Font_Light.woff2" crossorigin />
<link rel="preload" as="font" type="font/woff2" href="https://site.com/fonts/Font_Medium.woff2" crossorigin />
<link rel="preload" as="font" type="font/woff2" href="https://site.com/fonts/Font_Bold.woff2" crossorigin />
<link rel="preload" as="image" type="image/jpeg" href="https://site.com/uploads/image.jpg" />

<title>SEO Tags for ASP.NET Core - My Site Title</title>
<meta name="title" content="SEO Tags for ASP.NET Core - My Site Title" />
<meta name="description" content="SetoTags Creates all SEO tags you need such as meta, link, Twitter card (twitter:), open graph (og:), and ..." />
<meta name="keywords" content="SEO, AspNetCore, MVC, RazorPages" />
<meta name="author" content="Author Name" />
<link rel="author" href="https://github.com/author-profile" />
<link rel="canonical" href="https://site.com/url/" />
<link rel="application/opensearchdescription+xml" title="My Site Title" href="https://site.com/open-search.xml" />
<link rel="alternate" type="application/rss+xml" title="Post Feeds" href="https://site.com/rss/" />
<link rel="alternate" type="application/rss+xml" title="Post Comments" href="https://site.com/post/comment/rss" />

<meta name="twitter:card" content="summary_large_image" />
<meta name="twitter:title" content="SEO Tags for ASP.NET Core" />
<meta name="twitter:description" content="SetoTags creates all SEO tags you need such as meta, link, Twitter card (twitter:), open graph (og:), and ..." />
<meta name="twitter:site" content="@MySiteTwitter" />
<meta name="twitter:creator" content="@MyTwitterId" />
<meta name="twitter:image" content="https://site.com/uploads/image.jpg" />
<meta name="twitter:image:width" content="1280" />
<meta name="twitter:image:height" content="720" />
<meta name="twitter:image:alt" content="Image alt" />

<meta property="og:type" content="article" />
<meta property="og:title" content="SEO Tags for ASP.NET Core" />
<meta property="og:description" content="SetoTags creates all SEO tags you need such as meta, link, Twitter card (twitter:), open graph (og:), and ..." />
<meta property="og:url" content="https://site.com/url/" />
<meta property="og:site_name" content="My Site Title" />
<meta property="og:locale" content="en_US" />
<meta property="og:image" content="https://site.com/uploads/image.jpg" />
<meta property="og:image:secure_url" content="https://site.com/uploads/image.jpg" />
<meta property="og:image:type" content="image/jpeg" />
<meta property="og:image:width" content="1280" />
<meta property="og:image:height" content="720" />
<meta property="og:image:alt" content="Image alt" />
<meta property="article:publisher" content="https://facebook.com/MySite" />
<meta property="article:author" content="https://facebook.com/MyUserId" />
<meta property="article:published_time" content="2021-07-03T13:34:41+00:00" />
<meta property="article:modified_time" content="2021-07-03T13:34:41+00:00" />
<meta property="article:section" content="Article Topic" />
<meta property="article:tag" content="SEO" />
<meta property="article:tag" content="AspNetCore" />
<meta property="article:tag" content="MVC" />
<meta property="article:tag" content="RazorPages" />
<meta property="og:see_also" content="https://site.com/see-also-1" />
<meta property="og:see_also" content="https://site.com/see-also-2" />

...
```

## JSON-LD Support

SeoTags now supports popular JSON-LD types such as **Article**, **Product**, **Book**, **Organization**, **WebSite**, **WebPage**, and etc...

See our docs for [Nested example](https://mjebrahimi.github.io/SeoTags/jsonld1.html) and [Referenced example](https://mjebrahimi.github.io/SeoTags/jsonld2.html).

## Note
- This package does not generate **favicon** tags. Use [realfavicongenerator.net](https://realfavicongenerator.net/) to generate favicon tags. 
- **Only ASP.NET Core is supported** (not the legacy ASP.NET Framework)

## Contributing

Create an [issue](https://github.com/mjebrahimi/SeoTags/issues/new) if you find a BUG or have a Suggestion or Question. If you want to develop this project :

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## Give a Star! ⭐️

If you find this repository useful, please give it a star. Thanks!

## License

Copyright © 2021 [Mohammd Javad Ebrahimi](https://github.com/mjebrahimi) under the [MIT License](https://github.com/mjebrahimi/SeoTags/LICENSE).
