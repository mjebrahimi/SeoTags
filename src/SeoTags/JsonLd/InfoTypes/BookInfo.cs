using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// An book.
    /// </summary>
    /// <seealso cref="ThingInfo{Book}" />
    public class BookInfo : ThingInfo<Book>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/book-url/#book")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#book"); }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL of a reference page that identifies the work. For example, a Wikipedia, Wikidata, VIAF, or Library of Congress page for the book.
        /// </summary>
        public IEnumerable<string> SameAsUrls { get; set; }

        /// <summary>
        /// Gets or sets the authors. (instances of PersonInfo or PersonInfo.ReferTo method)
        /// </summary>
        public IEnumerable<PersonInfo> Authors { get; set; }

        /// <summary>
        /// Gets or sets the book editions. (WorkExample property)
        /// </summary>
        public IEnumerable<BookEditionInfo> BookEditions { get; set; }

        /// <summary>
        /// Converts to <see cref="Book"/>.
        /// </summary>
        /// <returns>A <see cref="Book"/> instance</returns>
        public override Book ConvertTo()
        {
            if (referId is not null)
                return new BookRefId(referId);

            url.EnsureNotNull(nameof(Url));
            Name.EnsureNotNullOrWhiteSpace(nameof(Name));
            Authors.EnsureNotNullAndNotNullItem(nameof(Authors));
            BookEditions.EnsureNotNullAndNotNullItem(nameof(BookEditions));
            if (BookEditions.Any() is false)
                throw new("There must be at least one BookEdition");

            //More info: https://developers.google.com/search/docs/data-types/book
            return new()
            {
                Id = id,
                Url = url,
                Name = Name,
                Author = new(Authors.Select(p => p.ConvertTo())),
                SameAs = SameAsUrls?.Select(p => p.ToUri()).ToArray(),
                WorkExample = new(BookEditions.Select(p => p.ConvertTo()))
            };
        }

        /// <summary>
        /// Refers to an book.
        /// </summary>
        /// <param name="bookInfo">The BookInfo instance.</param>
        /// <returns>BookInfo</returns>
        public static BookInfo ReferTo(BookInfo bookInfo)
        {
            bookInfo.EnsureNotNull(nameof(bookInfo));
            bookInfo.id.EnsureNotNull(nameof(bookInfo.Id));
            return new() { referId = bookInfo.id };
        }

        /// <summary>
        /// Refers to an book.
        /// </summary>
        /// <param name="id">The identifier. (BookInfo.Id)</param>
        /// <returns>BookInfo</returns>
        public static BookInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="BookInfo"/> to refers to the book.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>BookInfo</returns>
        public static implicit operator BookInfo(string id) => ReferTo(id);
    }

    /// <summary>
    /// Book (Edition) is the "lower level" Book entity.
    /// </summary>
    /// <seealso cref="ThingInfo{Book}" />
    public class BookEditionInfo : ThingInfo<Book>
    {
        private Uri url;

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => url = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language of the content. (e.g "en-US")
        /// </summary>
        public string InLanguage { get; set; }

        /// <summary>
        /// Gets or sets the isbn.
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// Gets or sets the date published.
        /// </summary>
        public DateTime? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the book format. (default: <see cref="BookFormatType.Hardcover"/>)
        /// </summary>
        public BookFormatType BookFormat { get; set; } = BookFormatType.Hardcover;

        /// <summary>
        /// Gets or sets the authors. (instances of PersonInfo or PersonInfo.ReferTo method)
        /// </summary>
        public IEnumerable<PersonInfo> Authors { get; set; }

        /// <summary>
        /// Gets or sets the URL of a reference page that identifies the work. For example, a Wikipedia, Wikidata, VIAF, or Library of Congress page for the book.
        /// </summary>
        public IEnumerable<string> SameAsUrls { get; set; }

        /// <summary>
        /// ReadAction defines your deep links to access the book, the retailer that stocks the book, and the criteria that the users must meet.
        /// </summary>
        public static ReadAction ReadAction { get; set; }

        /// <summary>
        /// Converts to <see cref="Book"/>.
        /// </summary>
        /// <returns>A <see cref="Book"/> instance</returns>
        public override Book ConvertTo()
        {
            BookFormat.EnsureIsValid(nameof(BookFormat));
            Isbn.EnsureNotNullOrWhiteSpace(nameof(Isbn));

            return new Book
            {
                Url = url,
                Name = Name,
                BookEdition = Name,
                InLanguage = InLanguage,
                Isbn = Isbn,
                BookFormat = BookFormat,
                DatePublished = DatePublished,
                Author = new(Authors.Select(p => p.ConvertTo())),
                PotentialAction = ReadAction
            };
        }
    }
}
