[![NuGet](https://img.shields.io/nuget/v/SeoTags.svg)](https://www.nuget.org/packages/SeoTags)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://github.com/mjebrahimi/SeoTags/workflows/.NET%20Core/badge.svg)](https://github.com/mjebrahimi/SeoTags)

# SeoTags
SeoTags Create **all SEO tags** you need such as **meta**, **link**, **twitter card** (twitter:), **open graph** (og:), and **JSON-LD** schema (structred data).

## How to use

See https://mjebrahimi.github.io/SeoTags/ for more info.

### 1. Install Package

```ini
PM> Install-Package SeoTags
```

### 2. Add Services and Configure

Everything you need to do is configuring `SeoInfo` object  and render this in your _Layout.cshtml.

This configuring can be achived by set properties of SeoInfo object in three ways:

1. When register services using `services.AddSeoTags(seoInfo => { ... })` method in Startup.cs
2. `Html.SetSeoInfo(seoInfo => { ... })`method in your views .cshtml (Mvc or RazorPages)
3. `HttpContext.SetSeoInfo(seoInfo => { ... })` method anywhere you access to HttpContext object (for example in your Controller or PageModel)

There is common options which is not page spacific like site title, site twitter id, site facebook id, open search url, feeds (Rss or Atom), and etc...

Usually this values set when register services using `services.AddSeoTags(seoInfo => { ... })` method in Startup.cs.

```cs
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddSeoTags(seoInfo =>
    {
        seoInfo.SetSiteInfo(
            siteTitle: "My Site Title", 
            siteTwitterId: "@MySiteTwitter",  //optional
            siteFacebookId: "https://facebook.com/MySite",  //optional
            openSearchUrl: "https://site.com/open-search.xml",  //optional
            robots: "index, follow"  //optional
        );

        //optional
        seoInfo.AddFeed(
            title: "Post Feeds",
            url: "https://site.com/rss/",
            feedType: FeedType.Rss);

        //optional
        seoInfo.AddDnsPrefetch("https://fonts.gstatic.com/", "https://www.google-analytics.com");

        //optional
        seoInfo.AddPreload(new Preload("https://site.com/site.css"),
            new Preload("https://site.com/app.js"),
            new Preload("https://site.com/fonts/Font.woff2"),
            new Preload("https://site.com/fonts/Font_Light.woff2"),
            new Preload("https://site.com/fonts/Font_Medium.woff2"),
            new Preload("https://site.com/fonts/Font_Bold.woff2"));

        //optional
        seoInfo.SetLocales("en_US");
    });
    //...
}
```

### 3. Place it in your _Layout.cshtml

Call `Html.SeoTags()` to render seo tags.

This method has two overload, one with SeoInfo argument (if you need to pass custom instance of SeoInfo object), and one without argument which retrive configured SeoInfo object from services.

```html
<!DOCTYPE html>
<html lang="en">
<head>

    <!-- You don't need this anymore -->
    @*<meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] -  Site Title</title>*@
    <!-- SeoTags generates all of these for you. -->

    @Html.SeoTags()
```

### 4. Set SEO info in your view

Set your SEO options with calling `Html.SetSeoInfo(seoInfo => { ... })` method in your view .cshtml.

You can do the same with calling `HttpContext.SetSeoInfo(seoInfo => { ... })` anywhere you access to HttpContext object (for example in your Controller or PageModel)

```csharp
@{
    //ViewData["Title"] = "Page Title";

    Html.SetSeoInfo(seoInfo =>
    {
        seoInfo.SetCommonInfo(
            pageTitle: "SEO Tags for ASP.NET Core",
            description: "Create all SEO tags you need such as meta, link, twitter card (twitter:), open graph (og:), and ...",
            url: "https://site.com/url/",
            keywordTags: new[] { "SEO", "AspNetCore", "MVC", "RazorPages" }, //optional
            seeAlsoUrls: new[] { "https://site.com/see-also-1", "https://site.com/see-also-2" }  //optional
        );

        seoInfo.SetImageInfo(
            url: "https://site.com/uploads/image.jpg",
            width: 1280,  //optional
            height: 720,  //optional
            alt: "Image alt",  //optional
            //mimeType: "image/jpeg", //optional (detects from url file extension if not set.)
            cardType: SeoTags.TwitterCardType.SummaryLargeImage   //optional
        );

        seoInfo.SetArticleInfo(
            authorName: "Author Name",
            publishDate: DateTimeOffset.Now,
            modifiedDate: DateTimeOffset.Now,  //optional
            authorTwitterId: "@MyTwitterId",  //optional
            authorFacebookId: "https://facebook.com/MyUserId",  //optional
            authorUrl: "https://github.com/author-profile",  //optional
            section: "Article Topic"  //optional
        );

        //Add another rss feed. (only for this page) (optional)
        seoInfo.AddFeed("Post Comments", "https://site.com/post/comment/rss", SeoTags.FeedType.Rss);
    });
}
```

### 5. Renderd Output

The following code shows the rendered output.

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
<meta name="description" content="Create all SEO tags you need such as meta, link, twitter card (twitter:), open graph (og:), and ..." />
<meta name="keywords" content="SEO, AspNetCore, MVC, RazorPages" />
<meta name="author" content="Author Name" />
<link rel="author" href="https://github.com/author-profile" />
<link rel="canonical" href="https://site.com/url/" />
<link rel="application/opensearchdescription+xml" title="My Site Title" href="https://site.com/open-search.xml" />
<link rel="alternate" type="application/rss+xml" title="Post Feeds" href="https://site.com/rss/" />
<link rel="alternate" type="application/rss+xml" title="Post Comments" href="https://site.com/post/comment/rss" />

<meta name="twitter:card" content="summary_large_image" />
<meta name="twitter:title" content="SEO Tags for ASP.NET Core" />
<meta name="twitter:description" content="Create all SEO tags you need such as meta, link, twitter card (twitter:), open graph (og:), and ..." />
<meta name="twitter:site" content="@MySiteTwitter" />
<meta name="twitter:creator" content="@MyTwitterId" />
<meta name="twitter:image" content="https://site.com/uploads/image.jpg" />
<meta name="twitter:image:width" content="1280" />
<meta name="twitter:image:height" content="720" />
<meta name="twitter:image:alt" content="Image alt" />

<meta property="og:type" content="article" />
<meta property="og:title" content="SEO Tags for ASP.NET Core" />
<meta property="og:description" content="Create all SEO tags you need such as meta, link, twitter card (twitter:), open graph (og:), and ..." />
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

## JSON-LD

SeoTags now supports popular JSON-LD types such as **Article**, **Product**, **Book**, **Organization**, **WebSite**, **WebPage**, and etc...

See our docs for [Nested example](https://mjebrahimi.github.io/SeoTags/jsonld1.html) and [Referenced example](https://mjebrahimi.github.io/SeoTags/jsonld2.html).

## Note
- This package does not generate **favicon** tags. Use [realfavicongenerator.net](https://realfavicongenerator.net/) to generate favicon tags. 
- Generation of **JSON-LD** is in progress and not yet available .

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

SeoTags is Copyright © 2021 [Mohammd Javad Ebrahimi](https://github.com/mjebrahimi) under the [MIT License](https://github.com/mjebrahimi/SeoTags/LICENSE).