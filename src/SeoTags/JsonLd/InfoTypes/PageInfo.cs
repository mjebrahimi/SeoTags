using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// A web page.
    /// </summary>
    /// <seealso cref="ThingInfo{WebPage}" />
    public class PageInfo : ThingInfo<WebPage>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/page/#webpage")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url. (and MainEntityOfPage property)
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#webpage"); }

        /// <summary>
        /// Gets or sets the title. (Name and Headline property)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        public IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the language of the content. (e.g "en-US")
        /// </summary>
        public string InLanguage { get; set; }

        /// <summary>
        /// Gets or sets the date published. (and DateCreated property)
        /// </summary>
        public DateTimeOffset? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTimeOffset? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the images (first image specified as PrimaryImageOfPage property). (instances of ImageInfo or ImageInfo.ReferTo method)
        /// </summary>
        public IEnumerable<ImageInfo> Images { get; set; }

        /// <summary>
        /// Gets or sets the video. (an instance of VideoInfo or VideoInfo.ReferTo method)
        /// </summary>
        public VideoInfo Video { get; set; }

        /// <summary>
        /// Gets or sets the breadcrumb. (an instance of BreadcrumbInfo or BreadcrumbInfo.ReferTo method)
        /// </summary>
        public BreadcrumbInfo Breadcrumb { get; set; }

        /// <summary>
        /// Gets or sets the web site (IsPartOf property). (an instance of WebSiteInfo or WebSiteInfo.ReferTo method)
        /// </summary>
        public WebSiteInfo WebSite { get; set; }

        /// <summary>
        /// Gets or sets the author (and Creator property). (an instance of PersonInfo or PersonInfo.ReferTo method)
        /// </summary>
        public PersonInfo Author { get; set; }

        /// <summary>
        /// Gets or sets the type of the page. (default: <see cref="PageType.WebPage"/>)
        /// </summary>
        public PageType PageType { get; set; } = PageType.WebPage;

        //public IEnumerable<string> RelatedLinks { get; set; }

        /// <summary>
        /// Converts to <see cref="WebPage"/>.
        /// </summary>
        /// <returns>A <see cref="WebPage"/> instance</returns>
        public override WebPage ConvertTo()
        {
            if (referId is not null)
                return new WebPageRefId(referId);

            url.EnsureNotNull(nameof(Url));
            Title.EnsureNotNullOrWhiteSpace(nameof(Title));
            Description.EnsureNotNullOrWhiteSpace(nameof(Description));

            var page = CreatePage(PageType);
            page.Id = id;
            page.Url = url;
            page.MainEntityOfPage = url;
            page.Name = Title;
            page.Headline = Title;
            page.Description = Description;
            page.Keywords = Keywords?.ToArray();
            page.InLanguage = InLanguage;
            page.DatePublished = DatePublished;
            page.DateCreated = DatePublished;
            page.DateModified = DateModified;
            page.Image = new(Images?.Select(p => p.ConvertTo()));
            page.Video = Video?.ConvertTo();
            page.PrimaryImageOfPage = Images?.ElementAtOrDefault(0)?.ConvertTo();
            page.Breadcrumb = Breadcrumb?.ConvertTo();
            page.IsPartOf = WebSite?.ConvertTo();
            page.Author = Author?.ConvertTo();
            page.Creator = Author?.ConvertTo();
            page.PotentialAction = new ReadAction //CommentAction
            {
                //Url = url
                Target = url
            };

            //page.RelatedLink = default;
            //page.Identifier = default;
            //page.CopyrightHolder = default;
            //page.CopyrightYear = default;
            //page.Comment = default;
            //page.CommentCount = default;

            return page;
        }

        /// <summary>
        /// Create a page based on specified page type.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>WebPage</returns>
        public static WebPage CreatePage(PageType pageType)
        {
            return pageType switch
            {
                PageType.WebPage => new WebPage(),
                PageType.AboutPage => new AboutPage(),
                PageType.ContactPage => new ContactPage(),
                PageType.FAQPage => new FAQPage(),
                PageType.QAPage => new QAPage(),
                PageType.ProfilePage => new ProfilePage(),
                PageType.CheckoutPage => new CheckoutPage(),
                PageType.SearchResultsPage => new SearchResultsPage(),
                PageType.ItemPage => new ItemPage(),
                PageType.CollectionPage => new CollectionPage(),
                _ => throw new ArgumentOutOfRangeException(nameof(pageType))
            };
        }

        /// <summary>
        /// Refers to a web page.
        /// </summary>
        /// <param name="pageInfo">The PageInfo instance.</param>
        /// <returns>PageInfo</returns>
        public static PageInfo ReferTo(PageInfo pageInfo)
        {
            pageInfo.EnsureNotNull(nameof(pageInfo));
            pageInfo.id.EnsureNotNull(nameof(pageInfo.Id));
            return new() { referId = pageInfo.id };
        }

        /// <summary>
        /// Refers to a web page.
        /// </summary>
        /// <param name="id">The identifier. (PageInfo.Id)</param>
        /// <returns>PageInfo</returns>
        public static PageInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="PageInfo"/> to refers to the web page.
        /// </summary>
        /// <param name="id">The identifier. (PageInfo.Id)</param>
        /// <returns>PageInfo</returns>
        public static implicit operator PageInfo(string id) => ReferTo(id);
    }

    /// <summary>
    /// Type of web page
    /// </summary>
    public enum PageType
    {
        /// <summary>
        /// A web page. Every web page is implicitly assumed to be declared to be of type
        /// WebPage, so the various properties about that webpage, such as <c>breadcrumb</c>
        /// may be used. We recommend explicit declaration if these properties are specified,
        /// but if they are found outside of an itemscope, they will be assumed to be about
        /// the page.
        /// </summary>
        WebPage,

        /// <summary>
        /// Web page type: About page.
        /// </summary>
        AboutPage,

        /// <summary>
        /// Web page type: Contact page.
        /// </summary>
        ContactPage,

        /// <summary>
        /// A [[FAQPage]] is a [[WebPage]] presenting one or more "[Frequently asked questions](https://en.wikipedia.org/wiki/FAQ)" (see also [[QAPage]]).
        /// </summary>
        FAQPage,

        /// <summary>
        /// A QAPage is a WebPage focussed on a specific Question and its Answer(s), e.g. in a question answering site or documenting Frequently Asked Questions (FAQs).
        /// </summary>
        QAPage,

        /// <summary>
        /// Web page type: Profile page.
        /// </summary>
        ProfilePage,

        /// <summary>
        /// Web page type: Checkout page.
        /// </summary>
        CheckoutPage,

        /// <summary>
        /// Web page type: Search results page.
        /// </summary>
        SearchResultsPage,

        /// <summary>
        /// A page devoted to a single item, such as a particular product or hotel.
        /// </summary>
        ItemPage,

        /// <summary>
        /// Web page type: Collection page.
        /// </summary>
        CollectionPage
    }
}
