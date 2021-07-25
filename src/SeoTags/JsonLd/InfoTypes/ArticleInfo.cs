using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// An article, such as a news article or piece of investigative report.
    /// </summary>
    /// <seealso cref="ThingInfo{Article}" />
    public class ArticleInfo : ThingInfo<Article>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. ("https://site.com/article-url#article")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url. (and MainEntityOfPage property)
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#article"); }

        /// <summary>
        /// Gets or sets the title. (Name and Headline property)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the keywords. (and ArticleSection property)
        /// </summary>
        public IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the date published.
        /// </summary>
        public DateTimeOffset? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTimeOffset? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the images. (instances of ImageInfo or ImageInfo.ReferTo method)
        /// </summary>
        public IEnumerable<ImageInfo> Images { get; set; }

        /// <summary>
        /// Gets or sets the video. (an instance of VideoInfo or VideoInfo.ReferTo method)
        /// </summary>
        public VideoInfo Video { get; set; }

        /// <summary>
        /// Gets or sets the author. (an instance of PersonInfo or PersonInfo.ReferTo method)
        /// </summary>
        public PersonInfo Author { get; set; }

        /// <summary>
        /// Gets or sets the publisher. (an instance of OrganizationInfo or OrganizationInfo.ReferTo method)
        /// </summary>
        public OrganizationInfo Publisher { get; set; }

        /// <summary>
        /// Gets or sets the web site (IsPartOf property). (an instance of WebSiteInfo or WebSiteInfo.ReferTo method)
        /// </summary>
        public WebSiteInfo WebSite { get; set; }

        /// <summary>
        /// Gets or sets the number of comments.
        /// </summary>
        public int? CommentCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is NewsArticle or Article.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is NewsArticle; otherwise, <c>false</c>.
        /// </value>
        public bool IsNewsArticle { get; set; } = true;

        /// <summary>
        /// Converts to <see cref="Article"/>.
        /// </summary>
        /// <returns>An <see cref="Article"/> instance</returns>
        public override Article ConvertTo()
        {
            if (referId is not null)
                return new ArticleRefId(referId);

            url.EnsureNotNull(nameof(Url));
            Title.EnsureNotNullOrWhiteSpace(nameof(Title));
            Description.EnsureNotNullOrWhiteSpace(nameof(Description));

            var article = IsNewsArticle ? new NewsArticle() : new Article();
            article.Id = id;
            article.Url = url;
            article.MainEntityOfPage = url;
            article.Name = Title;
            article.Headline = Title;
            article.Description = Description;
            article.Keywords = Keywords?.ToArray();
            article.ArticleSection = Keywords?.ToArray();
            article.DateCreated = DatePublished;
            article.DatePublished = DatePublished;
            article.DateModified = DateModified;
            article.Image = new(Images?.Select(p => p.ConvertTo()));
            article.Video = Video?.ConvertTo();
            article.Author = Author?.ConvertTo();
            article.Creator = Author?.ConvertTo();
            article.Publisher = Publisher?.ConvertTo();
            article.IsPartOf = WebSite?.ConvertTo();
            article.CommentCount = CommentCount;
            article.PotentialAction = new ReadAction //CommentAction
            {
                //Url = url
                Target = url
            };

            //article.Genre = default;
            //article.WordCount = default;
            //article.Identifier = default;
            //article.Comment = default;
            //article.Author =
            //    Author.HasValue1 ? Author.Value1?.ToSchemaNET() :
            //    Author.HasValue2 ? Author.Value2?.ToSchemaNET() :
            //    Author.HasValue3 ? Author.Value3 :
            //    Author.HasValue4 ? Author.Value4 : null,
            //article.Publisher =
            //    Publisher.HasValue1 ? Publisher.Value1?.ToSchemaNET() :
            //    Publisher.HasValue2 ? Publisher.Value2 : null

            return article;
        }

        /// <summary>
        /// Refers to an article.
        /// </summary>
        /// <param name="articleInfo">The ArticleInfo instance.</param>
        /// <returns>ArticleInfo</returns>
        public static ArticleInfo ReferTo(ArticleInfo articleInfo)
        {
            articleInfo.EnsureNotNull(nameof(articleInfo));
            articleInfo.id.EnsureNotNull(nameof(articleInfo.Id));
            return new() { referId = articleInfo.id };
        }

        /// <summary>
        /// Refers to an article.
        /// </summary>
        /// <param name="id">The identifier. (ArticleInfo.Id)</param>
        /// <returns>ArticleInfo</returns>
        public static ArticleInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="ArticleInfo"/> to refers to the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ArticleInfo</returns>
        public static implicit operator ArticleInfo(string id) => ReferTo(id);
    }
}
