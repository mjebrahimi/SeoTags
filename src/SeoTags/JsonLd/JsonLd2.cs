//using Schema.NET;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SeoTags
//{
//    public class JsonLd2
//    {
//        public JsonLd2()
//        {
//            //TODO: Preventing from XSS
//            //- html encode strings or utf-16 encode (for json-ld) 
//            //- url encoding?

//            //SchemaNET examples:
//            //https://github.com/RehanSaeed/Schema.NET/tree/main/Tests/Test
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/JsonLdContextTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/ContextJsonConverterTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/MixedTypesTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/MusicVenueTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/MusicGroupTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/MusicEventTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/MusicAlbumTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/JobPostingTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/CarTest.cs
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/EventTest.cs

//            //Most used types:
//            //Organization
//            //WebSite //WebPage
//            //Blog //BlogPosting
//            //SoftwareApplication //SoftwareSourceCode //WebApplication
//            //Collection
//            //Person
//            //Product
//            //Brand
//            //Offer
//            //Article //NewsArticle
//            //CreativeWork
//            //Rating //AggregateRating 
//            //Comment //CommentAction
//            //Airline //Airport //Accommodation //Place
//            //AccountingService //BankAccount
//            //LocalBusiness
//            //Thing
//            //Recipe
//            //Amount //Currency
//            //Url
//            //Address
//            //Series
//            //Book //BookSeries
//            //Movie //MovieSeries
//            //MusicRelease //MusicAlbum //AlbumRelease //MusicEvent //MusicGroup //MusicPlaylist //MusicRecording //MusicComposition
//            //AudioObject //Audiobook //AudioObjectAndBook
//            //VideoObject //VideoGallery
//            //Event //EventReservation //EventVenue //BusinessEvent //ChildrensEvent //ComedyEvent //DanceEvent //DeliveryEvent //EducationEvent //ExhibitionEvent //FoodEvent //LiteraryEvent //MusicEvent //OnDemandEvent //PublicationEvent //SaleEvent //ScreeningEvent //SocialEvent //SportsEvent //TheaterEvent //TheaterEvent //VisualArtsEvent;
//            //MobileApplication
//            //Sport //SportsTeam //SportsOrganization //Athlete
//            //Game //VideoGame //VideoGameSeries
//            //SearchAction //CommentAction //Action  //ActionStatusType //ControlAction //FailedActionStatus
//            //Periodical
//            //RoleName
//            //Coach
//            //DatedMoneySpecification
//            //HomeTeam
//            //AwayTeam
//            //IneligibleRegion
//            //RsvpResponse
//            //Episode //RadioEpisode //TVEpisode
//        }

//        #region Properties        
//        /// <summary>
//        /// Gets or sets the organization.
//        /// </summary>
//        public Organization Organization { get; set; }

//        /// <summary>
//        /// Gets or sets the web site.
//        /// </summary>
//        public WebSite WebSite { get; set; }

//        /// <summary>
//        /// Gets or sets the images.
//        /// </summary>
//        public List<ImageObject> Images { get; set; } = new();

//        /// <summary>
//        /// Gets or sets the product.
//        /// </summary>
//        public Product Product { get; set; }

//        /// <summary>
//        /// Gets or sets the article.
//        /// </summary>
//        public Article Article { get; set; }

//        /// <summary>
//        /// Gets or sets the page.
//        /// </summary>
//        public WebPage Page { get; set; } //TODO: Blog, BlogPosting

//        /// <summary>
//        /// Gets or sets the author.
//        /// </summary>
//        public Person Person { get; set; }

//        /// <summary>
//        /// Gets or sets the rating.
//        /// </summary>
//        public AggregateRating Rating { get; set; }

//        /// <summary>
//        /// Gets or sets the reviews.
//        /// </summary>
//        public List<Review> Reviews { get; set; } = new();

//        /// <summary>
//        /// Gets or sets the breadcrumb.
//        /// </summary>
//        public BreadcrumbList Breadcrumb { get; set; }

//        /// <summary>
//        /// Gets or sets the additional information.
//        /// </summary>
//        public List<Thing> AdditionalInfo { get; set; } = new();
//        #endregion

//        #region Methods
//        public void Render(StringBuilder builder)
//        {
//            #region Wireup
//            //Set publisher of website to organization
//            if (WebSite?.Publisher.HasValue is false && Organization?.Id is not null)
//                WebSite.Publisher = new OrganizationRefId(Organization.Id);

//            //Set images to product/article/page
//            if (Images?.Count > 0)
//            {
//                if (Page?.Image.HasValue is false)
//                    Page.Image = new(Images);
//                if (Product?.Image.HasValue is false)
//                    Product.Image = new(Images);
//                if (Article?.Image.HasValue is false)
//                    Article.Image = new(Images);
//            }

//            if (Product?.Offers.HasValue2 is true)
//            {
//                foreach (var offer in Product.Offers.OfType<IOffer>())
//                {
//                    //Set item offered to current product id
//                    if (offer.ItemOffered.HasValue is false && Product.Id is not null)
//                        offer.ItemOffered = new ProductRefId(Product.Id);

//                    //Set offer url to current product url
//                    if (offer.Url.Count == 0)
//                        offer.Url = Product.Url;

//                    //Set seller/offeredby to ogranization
//                    if (Organization?.Id is not null)
//                    {
//                        if (offer.Seller.HasValue is false)
//                            offer.Seller = new OrganizationRefId(Organization.Id);
//                        if (offer.OfferedBy.HasValue is false)
//                            offer.OfferedBy = new OrganizationRefId(Organization.Id);
//                    }
//                }
//            }

//            if (Rating is not null)
//            {
//                //Wire up product and rating to each other
//                if (Product is not null)
//                {
//                    if (Product.Id is not null && Rating.ItemReviewed.Count == 0)
//                        Rating.ItemReviewed = new ProductRefId(Product.Id);
//                    if (Product.AggregateRating.Count == 0)
//                        Product.AggregateRating = Rating;
//                }

//                //Wire up article and rating to each other
//                else if (Article is not null)
//                {
//                    if (Article.Id is not null && Rating.ItemReviewed.Count == 0)
//                        Rating.ItemReviewed = new ArticleRefId(Article.Id);
//                    if (Article.AggregateRating.Count == 0)
//                        Article.AggregateRating = Rating;
//                }
//            }

//            if (Reviews?.Count > 0)
//            {
//                //Wire up product and reveiws to each other 
//                if (Product is not null)
//                {
//                    if (Product.Id is not null)
//                    {
//                        foreach (var review in Reviews.Where(p => p.ItemReviewed.Count == 0))
//                        {
//                            review.ItemReviewed = new ProductRefId(Product.Id);
//                        }
//                    }
//                    Product.Review = new(Reviews);
//                }

//                //Wire up article and reveiws to each other 
//                else if (Article is not null)
//                {
//                    if (Article.Id is not null)
//                    {
//                        foreach (var review in Reviews.Where(p => p.ItemReviewed.Count == 0))
//                        {
//                            review.ItemReviewed = new ArticleRefId(Article.Id);
//                        }
//                    }
//                    Article.Review = new(Reviews);
//                }
//            }

//            if (Breadcrumb is not null && Breadcrumb.Id is null)
//            {
//                //Set breadcrumb id to page's breadcrumb id
//                if (Page?.Breadcrumb.HasValue1 is true && Page?.Breadcrumb.Count == 1)
//                {
//                    var pageBreadcrumb = Page.Breadcrumb.Cast<BreadcrumbList>().Single();
//                    Breadcrumb.Id = pageBreadcrumb.Id;
//                }

//                //Set breadcrumb id to page's url#breadcrumb
//                else if (Page?.Url.HasOne is true)
//                {
//                    var pageUrl = Page.Url.Single();
//                    Breadcrumb.Id = pageUrl.Relative("#breadcrumb");
//                }

//                //Set breadcrumb id to product's url#breadcrumb
//                else if (Product?.Url.HasOne is true)
//                {
//                    var productUrl = Product.Url.Single();
//                    Breadcrumb.Id = productUrl.Relative("#breadcrumb");
//                }

//                //Set breadcrumb id to Article's url#breadcrumb
//                else if (Article?.Url.HasOne is true)
//                {
//                    var articleUrl = Article.Url.Single();
//                    Breadcrumb.Id = articleUrl.Relative("#breadcrumb");
//                }
//            }

//            //Set page breadcrumb to current breadcrumb
//            if (Page?.Breadcrumb.HasValue is false && Breadcrumb?.Id is not null)
//                Page.Breadcrumb = new BreadcrumbListRefId(Breadcrumb.Id);

//            if (Person?.Id is not null && Article is not null && Article.Author.HasValue is false)
//                Article.Author = new PersonRefId(Person.Id);
//            if (Person?.Id is not null && Page is not null && Page.Author.HasValue is false)
//                Page.Author = new PersonRefId(Person.Id);

//            #endregion

//            //TODO: validation

//            if (Organization is not null)
//                builder.AppendLine(Organization.ToHtmlEscapedString());
//            if (WebSite is not null)
//                builder.AppendLine(WebSite.ToHtmlEscapedString());
//            //if (Images?.Count > 0)
//            //    builder.AppendLine(Images.ToHtmlEscapedString());
//            if (Person is not null)
//                builder.AppendLine(Person.ToHtmlEscapedString());
//            if (Product is not null)
//                builder.AppendLine(Product.ToHtmlEscapedString());
//            if (Article is not null)
//                builder.AppendLine(Article.ToHtmlEscapedString());
//            if (Rating is not null)
//                builder.AppendLine(Rating.ToHtmlEscapedString());
//            //if (Reviews?.Count > 0)
//            //    builder.AppendLine(Reviews.ToHtmlEscapedString());
//            if (Breadcrumb is not null)
//                builder.AppendLine(Breadcrumb.ToHtmlEscapedString());
//            //if (AdditionalInfo?.Count > 0)
//            //    builder.AppendLine(AdditionalInfo.ToHtmlEscapedString());
//        }

//        /// <summary>
//        /// Sets the organization.
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <param name="altName">The alt name.</param>
//        /// <param name="url">The URL.</param>
//        /// <param name="sameAs">The sameAs urls (usually social media urls).</param>
//        /// <param name="logoUrl">The logo URL.</param>
//        /// <param name="logoWidth">Width of the logo.</param>
//        /// <param name="logoHeight">Height of the logo.</param>
//        /// <param name="id">The id to reference later (e.g "https://site.com/#organization").</param>
//        public void SetOrganization(string name, string altName, string url, string[] sameAs = null,
//            string logoUrl = null, int? logoWidth = null, int? logoHeight = null, string id = null)
//        {
//            name.EnsureNotNullOrWhiteSpace(nameof(name));
//            url.EnsureNotNullOrWhiteSpace(nameof(url));
//            if (logoUrl is null && logoWidth is not null)
//                throw new ArgumentException("Logo width is set but logo url not set.");
//            if (logoUrl is null && logoHeight is not null)
//                throw new ArgumentException("Logo height is set but logo url not set.");

//            var uri = url.ToUri();
//            Organization ??= new();
//            Organization.Id = id?.ToUri() ?? uri.Relative("#organization");
//            Organization.Url = uri;
//            Organization.Name = name;
//            Organization.AlternateName = altName;
//            Organization.SameAs = sameAs?.Select(p => p.ToUri()).ToArray();

//            if (logoUrl is not null)
//            {
//                var logoUri = logoUrl.ToUri();
//                var logoId = logoUri.Relative("#logo");

//                var image = new ImageObject
//                {

//                    Id = logoId,
//                    Url = logoUri,
//                    ContentUrl = logoUri,
//                    Caption = altName,
//                    Width = logoWidth?.ToString(),
//                    Height = logoHeight?.ToString()
//                };

//                Organization.Logo = image;
//                //Organization.Image = image;

//                Organization.Image = new ImageRefId(logoId);
//            }

//            if (WebSite?.Publisher.HasValue is false)
//                WebSite.Publisher = new OrganizationRefId(Organization.Id);

//            #region Examples
//            /*
//             {
//                "@context": "http://schema.org",
//                "@type": "Organization",
//                "name": "Alibaba Travels Co.",
//                "alternateName": "سفرهای علی بابا",
//                "url": "https://www.alibaba.ir",
//                "logo": "https://www.alibaba.ir/icon.png",
//                "contactPoint": [
//                    {
//                        "@type": "ContactPoint",
//                        "telephone": "+982143900000",
//                        "contactType": "customer service",
//                        "areaServed": "IR",
//                        "availableLanguage": "Persian"
//                    },
//                    {
//                        "@type": "ContactPoint",
//                        "telephone": "+982124580000",
//                        "contactType": "sales",
//                        "areaServed": "IR",
//                        "availableLanguage": "Persian"
//                    }
//                ],
//                "sameAs": [
//                    "https://twitter.com/AlibabaIR",
//                    "https://www.instagram.com/alibabaticket/",
//                    "https://www.linkedin.com/company/alibaba-ir/"
//                ]
//            }

//            {
//                "@id": "https://moz.com#identity",
//                "@type": "Organization",
//                "url": "https://moz.com"
//                "name": "Moz",
//                "logo": {
//                    "@type": "ImageObject",
//                    "height": "60",
//                    "url": "https://moz.com/cms/_600x60_fit_center-center_82_none/5147/Moz-logo-blue.png?mtime=20170419135148&focal=none&tmtime=20210628145909",
//                    "width": "205"
//                },
//                "image": {
//                    "@type": "ImageObject",
//                    "height": "252",
//                    "url": "https://moz.com/cms/Moz-logo-blue.jpg?mtime=20170419135148&focal=none",
//                    "width": "862"
//                },
//                "sameAs": [
//                    "https://twitter.com/Moz",
//                    "https://www.facebook.com/moz/",
//                    "https://www.linkedin.com/company/moz/",
//                    "https://www.youtube.com/channel/UCs26XZBwrSZLiTEH8wcoVXw",
//                    "https://www.instagram.com/moz_hq/"
//                ],
//            }




//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/OrganizationTest.cs
//            var organization = new Organization
//            {
//                AreaServed = "GB", // Recommended. Omit for global.
//                ContactPoint = new ContactPoint() // Required
//                {
//                    AvailableLanguage = "English", // Recommended
//                    ContactOption = ContactPointOption.TollFree, // Recommended
//                    ContactType = "customer service", // Required
//                    Telephone = "+1-401-555-1212", // Required
//                },
//                Url = new Uri("https://example.com"), // Required
//                Logo = new Uri("https://example.com/logo.png"),
//            };
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Adds the contact telephone.
//        /// </summary>
//        /// <param name="telephone">The telephone number.</param>
//        /// <param name="type">A person or organization can have different contact points, for different purposes.
//        /// For example, a sales contact point, a PR contact point and so on.
//        /// This property is used to specify the kind of contact point.</param>
//        /// <param name="inLanguage">The language someone may use with or at the item, service or place. (e.g "English")</param>
//        /// <param name="areaServed">The geographic area where a service or offered item is provided. (e.g "US")</param>
//        public void AddContactTelephone(string telephone, string type = null, string inLanguage = null, string areaServed = null
//            /*, TimeSpan?[] openHours = null, TimeSpan?[] closeHours = null, Schema.NET.DayOfWeek?[] dayOfWeeks = null*/)
//        {
//            telephone.EnsureNotNullOrWhiteSpace(nameof(telephone));

//            var contact = new ContactPoint()
//            {
//                Telephone = telephone,
//                ContactType = type,
//                AvailableLanguage = inLanguage,
//                AreaServed = areaServed
//            };

//            //if (openHours is not null || closeHours is not null || dayOfWeeks is not null)
//            //{
//            //    contact.HoursAvailable = new OpeningHoursSpecification
//            //    {
//            //        Opens = openHours,
//            //        Closes = closeHours,
//            //        DayOfWeek = dayOfWeeks
//            //    };
//            //}

//            Organization ??= new();
//            var contacts = Organization.ContactPoint.ToList();
//            contacts.Add(contact);
//            Organization.ContactPoint = contacts;
//        }

//        /// <summary>
//        /// Adds the contact email.
//        /// </summary>
//        /// <param name="email">The email address.</param>
//        /// <param name="type">A person or organization can have different contact points, for different purposes.
//        /// For example, a sales contact point, a PR contact point and so on.
//        /// This property is used to specify the kind of contact point.</param>
//        /// <param name="inLanguage">The language someone may use with or at the item, service or place. (e.g "English")</param>
//        /// <param name="areaServed">The geographic area where a service or offered item is provided. (e.g "US")</param>
//        public void AddOrganizationContactEmail(string email, string type = null, string inLanguage = null, string areaServed = null)
//        {
//            email.EnsureNotNullOrWhiteSpace(nameof(email));

//            var contact = new ContactPoint()
//            {
//                Email = email,
//                ContactType = type,
//                AvailableLanguage = inLanguage,
//                AreaServed = areaServed
//            };

//            Organization ??= new();
//            var contacts = Organization.ContactPoint.ToList();
//            contacts.Add(contact);
//            Organization.ContactPoint = contacts;
//        }

//        /// <summary>
//        /// Sets the web site.
//        /// </summary>
//        /// <param name="url">The URL (e.g "https://site.com/").</param>
//        /// <param name="name">The name.</param>
//        /// <param name="description">The description.</param>
//        /// <param name="altName">The Alt name.</param>
//        /// <param name="inLanguage">The in language. (e.g "en-US")</param>
//        /// <param name="searchUrl">The search action URL. (e.g "https://site.com/?s={search_term_string}")</param>
//        /// <param name="searchQuery">The search query. (e.g "required name=search_term_string")</param>
//        /// <param name="id">The id to reference later. (e.g "https://site.com/#website")</param>
//        /// <param name="organizationId">The search query. (e.g "https://site.com/#organization")</param>
//        public void SetWebSite(string url, string name, string description, string altName = null, string inLanguage = null,
//            string searchUrl = null, string searchQuery = null, string id = null, string organizationId = null)
//        {
//            url.EnsureNotNullOrWhiteSpace(nameof(url));
//            name.EnsureNotNullOrWhiteSpace(nameof(name));
//            description.EnsureNotNullOrWhiteSpace(nameof(description));
//            if (searchUrl is not null && searchQuery is null)
//                throw new ArgumentException("Search url is set but search query not set.");
//            if (searchUrl is null && searchQuery is not null)
//                throw new ArgumentException("Search query is set but search url not set.");

//            var uri = new Uri(url);
//            WebSite ??= new WebSite();
//            WebSite.Id = id?.ToUri() ?? uri.Relative("#website");
//            WebSite.Url = uri;
//            WebSite.Name = name;
//            WebSite.Description = description;
//            WebSite.AlternateName = altName;
//            WebSite.InLanguage = inLanguage;

//            if (organizationId is not null)
//                WebSite.Publisher = new OrganizationRefId(organizationId.ToUri());
//            else if (Organization?.Id is not null)
//                WebSite.Publisher = new OrganizationRefId(Organization.Id);

//            if (searchUrl is not null && searchQuery is not null)
//            {
//                WebSite.PotentialAction = new SearchAction
//                {
//                    Target = new Uri(searchUrl),
//                    QueryInput = searchQuery
//                };
//            }

//            #region Examples
//            /*
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Schema.NET.Test/Examples/WebsiteTest.cs

//            {
//                "@type": "WebSite",
//                "@id": "https://www.daneshjooyar.com/#website",
//                "url": "https://www.daneshjooyar.com/",
//                "name": "\u062f\u0627\u0646\u0634\u062c\u0648\u06cc\u0627\u0631",
//                "description": "\u0622\u0645\u0648\u0632\u0634 \u0628\u0631\u0646\u0627\u0645\u0647 \u0646\u0648\u06cc\u0633\u06cc",
//                "inLanguage": "fa-IR",
//                "publisher": {
//                    "@id": "https://www.daneshjooyar.com/#organization"
//                },
//                "potentialAction": [
//                    {
//                        "@type": "SearchAction",
//                        "target": "https://www.daneshjooyar.com/?s={search_term_string}",
//                        "query-input": "required name=search_term_string"
//                    }
//                ]
//             */
//            #endregion
//        }

//        /// <summary>
//        /// Adds an image.
//        /// </summary>
//        /// <param name="url">The URL.</param>
//        /// <param name="caption">The caption.</param>
//        /// <param name="width">The width.</param>
//        /// <param name="height">The height.</param>
//        /// <param name="inLanguage">The in language. (e.g "en-US")</param>
//        /// <param name="pageUrl">The current page url (to create id automatically)</param>
//        /// <param name="id">The id to reference later. (e.g "https://site.com/#image")</param>
//        public void AddImage(string url, string caption = null, int? width = null, int? height = null, string inLanguage = null, string pageUrl = null, string id = null)
//        {
//            url.EnsureNotNullOrWhiteSpace(nameof(url));

//            var uri = url.ToUri();
//            var idUri = id?.ToUri() ??
//                pageUrl?.ToUri().Relative(Images.Count == 0 ? "#primaryimage" : "#image" + Images.Count.ToString());

//            var image = new ImageObject
//            {
//                Id = idUri,
//                Url = uri,
//                ContentUrl = uri,
//                Caption = caption,
//                InLanguage = inLanguage,
//                Width = width?.ToString(),
//                Height = height?.ToString()
//            };

//            Images ??= new();
//            Images.Add(image);

//            if (Page is not null)
//                Page.Image = new(Images);
//            if (Product is not null)
//                Product.Image = new(Images);
//            else if (Article is not null)
//                Article.Image = new(Images);
//        }

//        /// <summary>
//        /// Sets the product.
//        /// </summary>
//        /// <param name="url">The URL.</param>
//        /// <param name="name">The name.</param>
//        /// <param name="description">The description.</param>
//        /// <param name="altName">The alt name.</param>
//        /// <param name="category">The category.</param>
//        /// <param name="brandName">The brand name.</param>
//        /// <param name="productId">The product id.</param>
//        public void SetProduct(string url, string name, string description, string altName = null, string category = null, string brandName = null, string productId = null)
//        {
//            url.EnsureNotNullOrWhiteSpace(nameof(url));
//            name.EnsureNotNullOrWhiteSpace(nameof(name));
//            description.EnsureNotNullOrWhiteSpace(nameof(description));

//            var uri = new Uri(url);
//            Product ??= new();
//            Product.Url = uri;
//            Product.Name = name;
//            Product.AlternateName = altName;
//            Product.Description = description;
//            Product.Category = category;
//            Product.Sku = productId;
//            Product.Mpn = productId;
//            if (brandName is not null)
//                Product.Brand = new Brand { Name = brandName }; //Brand, Org, BrandRefId, OrgRefId

//            //Set images to product image
//            if (Images?.Count > 0)
//            {
//                if (Images.All(p => p.Id is not null))
//                    Product.Image = Images.Select(p => new ImageRefId(p.Id)).Cast<IImageObject>().ToList();
//                else
//                    Product.Image = new(Images);
//            }

//            #region Examples
//            /*
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/ProductTest.cs
//            var product = new Product()
//            {
//                Name = "Executive Anvil", // Required
//                Description = "Sleeker than ACME's Classic Anvil, the Executive Anvil is perfect for the business traveller looking for something to drop from a height.", // Recommended
//                Image = new Uri("https://www.example.com/anvil_executive.jpg"), // Required
//                Mpn = "925872", // Recommended
//                Brand = new Brand() // Recommended
//                {
//                    Name = "ACME",
//                },
//                AggregateRating = new AggregateRating() // Recommended
//                {
//                    ReviewCount = 89,
//                    RatingValue = 4.4D,
//                },
//                Review = new OneOrMany<IReview>((IReview)null!), // Recommended
//                Offers = new Offer() // Recommended
//                {
//                    Url = (Uri)null!, // Recommended
//                    ItemOffered = (Product)null!, // Recommended
//                    PriceCurrency = "USD", // Required
//                    Price = 119.99M, // Required
//                    PriceValidUntil = new DateTime(2020, 11, 5), // Recommended
//                    ItemCondition = OfferItemCondition.UsedCondition,
//                    Availability = ItemAvailability.InStock, // Recommended
//                    Seller = new Organization()
//                    {
//                        Name = "Executive Objects",
//                    },
//                },
//            };



//            {
//                "@context": "https:\/\/schema.org",
//                "@type": "Product",
//                "name": "\u0622\u0645\u0648\u0632\u0634 ASP.NET Core 5 \u062f\u0631 \u0642\u0627\u0644\u0628 \u067e\u0631\u0648\u0698\u0647 \u0628\u0632\u0631\u06af \u062a\u0627\u06a9\u0633\u06cc \u0622\u0646\u0644\u0627\u06cc\u0646 \u0645\u0634\u0627\u0628\u0647 \u0627\u0633\u0646\u067e",
//                "image": [
//                    "https:\/\/daneshjooyar.com\/wp-content\/uploads\/2020\/08\/snap.png"
//                ],
//                "description": "\u062a\u0627\u06a9\u0633\u06cc \u0622\u0646\u0644\u0627\u06cc\u0646 \u060c \u06cc\u06a9 \u0633\u0631\u0641\u0635\u0644 \u06a9\u0627\u0645\u0644 \u0628\u0631\u0627\u06cc \u0639\u0632\u06cc\u0632\u0627\u0646\u06cc \u0627\u0633\u062a \u06a9\u0647 \u0645\u06cc\u062e\u0648\u0627\u0647\u0646\u062f \u0635\u0641\u0631 \u062a\u0627 \u0635\u062f \u0637\u0631\u0627\u062d\u06cc \u0648\u0628\u0633\u0627\u06cc\u062a \u0631\u0627 \u0641\u0631\u0627 \u0628\u06af\u06cc\u0631\u06cc\u0646\u062f \u0648 \u0628\u0627 \u06cc\u06a9 \u067e\u0631\u0648\u0698\u0647\u060c \u0648\u0627\u0631\u062f \u0628\u0627\u0632\u0627\u0631 \u06a9\u0627\u0631 \u0634\u0648\u0646\u062f",
//                "brand": {
//                    "@type": "Brand",
//                    "name": "\u062f\u0627\u0646\u0634\u062c\u0648\u06cc\u0627\u0631"
//                },
//                "category": "\u0622\u0645\u0648\u0632\u0634 ASP.NET",
//                "aggregateRating": {
//                    "@type": "AggregateRating",
//                    "ratingValue": 4.3300000000000000710542735760100185871124267578125,
//                    "reviewCount": "6"
//                },
//                "review": {
//                    "@type": "Review",
//                    "reviewRating": {
//                        "@type": "Rating",
//                        "ratingValue": 4.3300000000000000710542735760100185871124267578125,
//                        "bestRating": 5
//                    },
//                    "author": {
//                        "@type": "Person",
//                        "name": "\u0645\u06cc\u0644\u0627\u062f \u0639\u0627\u0645\u0631\u06cc"
//                    }
//                },
//                "mpn": 2025,
//                "sku": "c2574048",
//                "offers": {
//                    "@type": "Offer",
//                    "url": "https:\/\/www.daneshjooyar.com\/%d8%a2%d9%85%d9%88%d8%b2%d8%b4-%d8%b7%d8%b1%d8%a7%d8%ad%db%8c-%d8%b3%d8%a7%db%8c%d8%aa-%d9%88-%d8%a7%d9%be%d9%84%db%8c%da%a9%db%8c%d8%b4%d9%86-%d8%aa%d8%a7%da%a9%d8%b3%db%8c-%d8%a2%d9%86%d9%84%d8%a7\/",
//                    "priceCurrency": "IRR",
//                    "price": 8800000,
//                    "availability": "https:\/\/schema.org\/InStock",
//                    "seller": {
//                        "@type": "Organization",
//                        "name": "\u062f\u0627\u0646\u0634\u062c\u0648\u06cc\u0627\u0631"
//                    },
//                    "priceValidUntil": "2021-12-31"
//                }
//            }

//            {
//                "@context": "https://www.schema.org",
//                "@type": "Product",
//                "name": "کروسان کاکائو پچ پچ  بسته 6 عددی",
//                "alternateName": "Pech Pech Cocoa Croissant Pack of 6",
//                "image": [
//                    "https://dkstatics-public.digikala.com/digikala-products/119588865.jpg?x-oss-process=image/resize,h_1600/quality,q_80/watermark,image_ZGstdy8xLnBuZw==,t_90,g_nw,x_15,y_15",
//                    "https://dkstatics-public.digikala.com/digikala-products/119588867.jpg?x-oss-process=image/resize,h_1600/quality,q_80/watermark,image_ZGstdy8xLnBuZw==,t_90,g_nw,x_15,y_15",
//                    "https://dkstatics-public.digikala.com/digikala-products/31f3beb7a8689bc74459db8e4f260221034e2bf9_1603785925.jpg?x-oss-process=image/resize,h_1600/quality,q_80"
//                ],
//                "description": "",
//                "sku": 2522021,
//                "mpn": 2522021,
//                "aggregateRating": {
//                    "@type": "AggregateRating",
//                    "ratingValue": 4.4,
//                    "reviewCount": 3719,
//                    "bestRating": 5,
//                    "worstRating": 0
//                },
//                "brand": {
//                    "@type": "Thing",
//                    "name": "پچ پچ"
//                },
//                "offers": {
//                    "@type": "AggregateOffer",
//                    "priceCurrency": "IRR",
//                    "lowPrice": 195000,
//                    "highPrice": 195000,
//                    "offerCount": 1,
//                    "offers": {
//                        "@type": "Offer",
//                        "priceCurrency": "IRR",
//                        "price": 195000,
//                        "itemCondition": "https://schema.org/UsedCondition",
//                        "availability": "https://schema.org/InStock",
//                        "seller": {
//                            "@type": "Organization",
//                            "name": "دیجی‌کالا"
//                        }
//                    }
//                },
//                "review": {
//                    "@type": "Review",
//                    "author": "حسین بریهی",
//                    "datePublished": "2021-01-10",
//                    "description": "شکلاتش هر دفعه داره کمتر میشه. کیفیتش هم کمتر شده",
//                    "name": "کروسان",
//                    "reviewRating": {
//                        "@type": "Rating",
//                        "bestRating": 5,
//                        "ratingValue": 2,
//                        "worstRating": 0
//                    }
//                }
//            }
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Sets the product offer.
//        /// </summary>
//        /// <param name="price">The price.</param>
//        /// <param name="currency">The currency (e.g "USD").</param>
//        /// <param name="url">The url of product.</param>
//        /// <param name="selerOrganizationId">The condition option.</param>
//        /// <param name="offeredProductId">The condition option.</param>
//        /// <param name="availability">The availability option.</param>
//        /// <param name="condition">The condition option.</param>
//        /// <param name="validFrom">The date when the item becomes valid.</param>
//        /// <param name="validThrough">The date after when the item is not valid. For example the end of an offer, salary period, or a period of opening hours.</param>
//        /// <param name="priceValidUntil">The date after which the price is no longer available.</param>
//        public void SetProductOffer(decimal price, string currency, string url = null, string selerOrganizationId = null, string offeredProductId = null,
//            ItemAvailability? availability = ItemAvailability.InStock, OfferItemCondition? condition = OfferItemCondition.UsedCondition,
//            DateTimeOffset? validFrom = null, DateTimeOffset? validThrough = null, DateTime? priceValidUntil = null)
//        {
//            if (price < 0)
//                throw new ArgumentException("Price can not be less than zero.");
//            currency.EnsureNotNullOrWhiteSpace(nameof(currency));

//            var organizationId = selerOrganizationId?.ToUri() ?? Organization?.Id;
//            var organizationRefId = organizationId is not null ? new OrganizationRefId(organizationId) : null;

//            var productId = offeredProductId?.ToUri() ?? Product?.Id;
//            var productRefId = productId is not null ? new ProductRefId(productId) : null;

//            Product ??= new();
//            Product.Offers = new Offer()
//            {
//                Url = url?.ToUri() ?? Product.Url,
//                Price = price,
//                PriceCurrency = currency,
//                Availability = availability,
//                ItemCondition = condition,
//                ItemOffered = productRefId,
//                Seller = organizationRefId,
//                OfferedBy = organizationRefId,
//                //Seller = new Organization { Name = organizationName }, //Organization //Person
//                //OfferedBy = new Organization { Name = organizationName }, //Organization //Person
//                ValidFrom = validFrom,
//                ValidThrough = validThrough,
//                PriceValidUntil = priceValidUntil,
//                //AvailabilityStarts = validFrom,
//                //AvailabilityEnds = validThrough,

//                //AggregateOffer for several offers
//                //LowPrice = decimal ?,
//                //HighPrice = decimal ?,
//                //OfferCount = 1,
//            };

//            #region #Examples
//            /*
//            "offers": {
//                "@type": "Offer",
//                "url": "https:\/\/www.daneshjooyar.com\/%d8%a2%d9%85%d9%88%d8%b2%d8%b4-%d8%b7%d8%b1%d8%a7%d8%ad%db%8c-%d8%b3%d8%a7%db%8c%d8%aa-%d9%88-%d8%a7%d9%be%d9%84%db%8c%da%a9%db%8c%d8%b4%d9%86-%d8%aa%d8%a7%da%a9%d8%b3%db%8c-%d8%a2%d9%86%d9%84%d8%a7\/",
//                "priceCurrency": "IRR",
//                "price": 8800000,
//                "availability": "https:\/\/schema.org\/InStock",
//                "seller": {
//                    "@type": "Organization",
//                    "name": "\u062f\u0627\u0646\u0634\u062c\u0648\u06cc\u0627\u0631"
//                },
//                "priceValidUntil": "2021-12-31"
//            }


//            "offers": {
//                "@type": "AggregateOffer",
//                "priceCurrency": "IRR",
//                "lowPrice": 195000,
//                "highPrice": 195000,
//                "offerCount": 1,
//                "offers": {
//                    "@type": "Offer",
//                    "priceCurrency": "IRR",
//                    "price": 195000,
//                    "itemCondition": "https://schema.org/UsedCondition",
//                    "availability": "https://schema.org/InStock",
//                    "seller": {
//                        "@type": "Organization",
//                        "name": "دیجی‌کالا"
//                    }
//                }
//            }, 
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Sets the product rating.
//        /// </summary>
//        /// <param name="ratingValue">The rating value.</param>
//        /// <param name="bestRating">The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.</param>
//        /// <param name="worstRating">The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.</param>
//        /// <param name="ratingCount">The count of total number of ratings/reviews.</param>
//        /// <param name="url">The current page url (to create id automatically)</param>
//        /// <param name="id">The id to reference later. (e.g "https://site.com/#breadcrumb")</param>
//        public void SetRating(double ratingValue, double? bestRating = null, double? worstRating = null, int? ratingCount = null, string url = null, string id = null)
//        {
//            if (ratingValue > bestRating)
//                throw new ArgumentException("Rating value can not be grater than best rating value.");
//            if (ratingValue < worstRating)
//                throw new ArgumentException("Rating value can not be less than worst rating value.");

//            var ratingId = id?.ToUri();
//            ratingId ??= url?.ToUri().Relative("#rating");
//            ratingId ??= Page?.Url.SingleOrDefault()?.Relative("#rating");
//            ratingId ??= Article?.Url.SingleOrDefault()?.Relative("#rating");
//            ratingId ??= Product?.Url.SingleOrDefault()?.Relative("#rating");

//            Rating ??= new();
//            Rating.Id = ratingId;
//            Rating.RatingValue = ratingValue;
//            Rating.BestRating = bestRating;
//            Rating.WorstRating = worstRating;
//            Rating.RatingCount = ratingCount;
//            Rating.ReviewCount = ratingCount;

//            if (Product?.Id is not null)
//                Rating.ItemReviewed = new ProductRefId(Product.Id);
//            else if (Article?.Id is not null)
//                Rating.ItemReviewed = new ArticleRefId(Article.Id);

//            if (Rating.Id is not null)
//            {
//                if (Product is not null)
//                    Product.AggregateRating = new AggregateRatingRefId(Rating.Id);
//                else if (Article is not null)
//                    Article.AggregateRating = new AggregateRatingRefId(Rating.Id);
//            }

//            #region Examples
//            /*
//             "aggregateRating": {
//                "@type": "AggregateRating",
//                "ratingValue": 4.4,
//                "reviewCount": 3719,
//                "bestRating": 5,
//                "worstRating": 0
//            }

//            "aggregateRating": {
//                "@type": "AggregateRating",
//                "ratingValue": 4.33,
//                "reviewCount": "6"
//            },
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Adds the product review.
//        /// </summary>
//        /// <param name="authorName">Name of the author.</param>
//        /// <param name="description">The description.</param>
//        /// <param name="datePublished">The date published.</param>
//        /// <param name="ratingValue">The rating value.</param>
//        /// <param name="bestRating">The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.</param>
//        /// <param name="worstRating">The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.</param>
//        /// <param name="authorUrl">The url of author profile.</param>
//        ///// <param name="reviewedId">The item that is being reviewed/rated.</param>
//        public void AddReview(string authorName, string description, DateTimeOffset datePublished,
//            double? ratingValue = null, double? bestRating = null, double? worstRating = null, string authorUrl = null /*, string reviewedId = null*/)
//        {
//            authorName.EnsureNotNullOrWhiteSpace(nameof(authorName));
//            description.EnsureNotNullOrWhiteSpace(nameof(description));

//            var review = new Review
//            {
//                Description = description,
//                ReviewBody = description, //I did not see these used anywhere 
//                DatePublished = datePublished,
//                DateCreated = datePublished, //I did not see these used anywhere 
//                                             //DateModified = DateTimeOffset?,
//                Author = new Person
//                {
//                    Name = authorName,
//                    Url = authorUrl?.ToUri(),
//                },
//            };

//            if (ratingValue is not null)
//            {
//                review.ReviewRating = new Rating
//                {
//                    RatingValue = ratingValue,
//                    BestRating = bestRating,
//                    WorstRating = worstRating,
//                };
//            }

//            if (Product?.Id is not null)
//                review.ItemReviewed = new ProductRefId(Product.Id);
//            else if (Article?.Id is not null)
//                review.ItemReviewed = new ArticleRefId(Article.Id);

//            Reviews ??= new();
//            Reviews.Add(review);

//            if (Product is not null)
//                Product.Review = new(Reviews);
//            else if (Article is not null)
//                Article.Review = new(Reviews);

//            #region Examples
//            /*
//            "review": {
//                "@type": "Review",
//                "reviewRating": {
//                    "@type": "Rating",
//                    "ratingValue": 4.3300000000000000710542735760100185871124267578125,
//                    "bestRating": 5
//                },
//                "author": {
//                    "@type": "Person",
//                    "name": "\u0645\u06cc\u0644\u0627\u062f \u0639\u0627\u0645\u0631\u06cc"
//                }
//            }, 

//            {
//                "@type": "Review",
//                "@id": "https:\/\/learnfiles.com\/course\/---\/comment-page-17\/#comment-85366",
//                "datePublished": "\u06f1\u06f3\u06f9\u06f8\/\u06f1\u06f2\/\u06f1 \u06f2\u06f3:\u06f4\u06f8:\u06f2\u06f8",
//                "description": "\u0633\u0644\u0627\u0645 \u0645\u0646 \u06cc\u0647 \u0641\u0627\u06cc\u0644 js \u0633\u0627\u062e\u062a\u0645 \u0648\u0644\u06cc \u0647\u0631\u06a9\u062f\u06cc \u062a\u0648\u0634 \u0645\u06cc\u0646\u0648\u06cc\u0633\u0645 \u062d\u062a\u06cc alert \u0627\u0631\u0648\u0631 used befor it was define \u0645\u06cc\u0632\u0646\u0647 \u0645\u0634\u06a9\u0644\u0634 \u0686\u06cc\u0647\u061f",
//                "itemReviewed": {
//                    "@type": "Product",
//                    "name": "\u062f\u0648\u0631\u0647 \u0622\u0645\u0648\u0632\u0634 \u062c\u0627\u0648\u0627 \u0627\u0633\u06a9\u0631\u06cc\u067e\u062a"
//                },
//                "author": {
//                    "@type": "Person",
//                    "name": "sara"
//                }
//            },
//            {
//                "@type": "Review",
//                "@id": "https:\/\/learnfiles.com\/course\/---\/comment-page-17\/#comment-85819",
//                "datePublished": "\u06f1\u06f3\u06f9\u06f8\/\u06f1\u06f2\/\u06f1\u06f1 \u06f9:\u06f5\u06f9:\u06f3\u06f2",
//                "description": "\u0633\u0644\u0627\u0645 \u0627\u06cc\u0646 \u0627\u0645\u0648\u0632\u0634 \u0686\u0646\u062f \u062f\u0631\u0635\u062f \u0627\u0632 \u062c\u0627\u0648\u0627\u0627\u0633\u06a9\u0631\u06cc\u067e\u062a \u0647\u0633\u062a\u061f",
//                "itemReviewed": {
//                    "@type": "Product",
//                    "name": "\u062f\u0648\u0631\u0647 \u0622\u0645\u0648\u0632\u0634 \u062c\u0627\u0648\u0627 \u0627\u0633\u06a9\u0631\u06cc\u067e\u062a"
//                },
//                "author": {
//                    "@type": "Person",
//                    "name": "HAMID"
//                }
//            },

//            ClaimReview
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/ClaimReviewTest.cs
//            var claimReview = new ClaimReview()
//            {
//                DatePublished = new DateTime(2016, 6, 22), // Required
//                Url = new Uri("https://example.com/news/science/worldisflat.html"), // Required
//                ItemReviewed = new CreativeWork() // Required
//                {
//                    Author = new Organization() // Required
//                    {
//                        Name = "Square World Society", // Required
//                        SameAs = new Uri("https://example.flatworlders.com/we-know-that-the-world-is-flat"), // Recommended
//                    },
//                    DatePublished = new DateTime(2016, 6, 20), // Optional
//                },
//                ClaimReviewed = "The world is flat", // Required
//                Author = new Organization() // Required
//                {
//                    Name = "Example.com science watch",
//                },
//                ReviewRating = new Rating() // Required
//                {
//                    RatingValue = 1D, // Required
//                    BestRating = 5D, // Required
//                    WorstRating = 1D, // Required
//                    AlternateName = "False", // Recommended
//                },
//            };
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Sets the breadcrumb
//        /// </summary>
//        /// <param name="items">The items</param>
//        public void SetBreadcrumb(params (string Url, string Name)[] items)
//        {
//            SetBreadcrumb(items.AsEnumerable());
//        }

//        /// <summary>
//        /// Sets the breadcrumb
//        /// </summary>
//        /// <param name="items">The items</param>
//        /// <param name="url">The current page url (to create id automatically)</param>
//        /// <param name="id">The id to reference later. (e.g "https://site.com/#breadcrumb")</param>
//        public void SetBreadcrumb(IEnumerable<(string Url, string Name)> items, string url = null, string id = null)
//        {
//            items.EnsureNotNull(nameof(items));

//            var list = Breadcrumb.ItemListElement.Cast<IListItem>().ToList();
//            foreach (var (Url, Name) in items)
//            {
//                Name.EnsureNotNullOrWhiteSpace(nameof(Name));
//                Url.EnsureNotNullOrWhiteSpace(nameof(Url));

//                list.Add(new ListItem
//                {
//                    Position = list.Count + 1,
//                    //Name = item.Name,
//                    //Url = new Uri(item.Url),
//                    Item = new WebPage
//                    {
//                        Name = Name,
//                        Url = new Uri(Url),
//                    }
//                });
//            }

//            //Create id of breadcrumb
//            var breadcrumbId = id?.ToUri();
//            breadcrumbId ??= url?.ToUri().Relative("#breadcrumb");
//            breadcrumbId ??= Page?.Breadcrumb.Value1.Cast<BreadcrumbList>().SingleOrDefault()?.Id;
//            breadcrumbId ??= Page?.Url.SingleOrDefault()?.Relative("#breadcrumb");
//            breadcrumbId ??= Product?.Url.SingleOrDefault()?.Relative("#breadcrumb");
//            breadcrumbId ??= Article?.Url.SingleOrDefault()?.Relative("#breadcrumb");

//            Breadcrumb ??= new();
//            Breadcrumb.Id = breadcrumbId;
//            Breadcrumb.ItemListElement = list;
//            Breadcrumb.NumberOfItems = list.Count;

//            //Set page's breadcrumb
//            if (Page?.Breadcrumb.HasValue is false && Breadcrumb.Id is not null)
//                Page.Breadcrumb = new BreadcrumbListRefId(Breadcrumb.Id);

//            #region Examples
//            /*
//            //ItemList
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/ItemListTest.cs
//            //BreadcrumbList
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/BreadcrumbListTest.cs
//            var breadcrumbList = new BreadcrumbList()
//            {
//                ItemListElement = new List<IListItem>()
//                {
//                    new ListItem()
//                    {
//                        Position = 1,
//                        Item = new Book()
//                        {
//                            Id = new Uri("https://example.com/books"),
//                            Name = "Books",
//                            Image = new Uri("https://example.com/images/icon-book.png"),
//                        },
//                    },
//                    new ListItem()
//                    {
//                        Position = 2,
//                        Item = new Person()
//                        {
//                            Id = new Uri("https://example.com/books/authors"),
//                            Name = "Authors",
//                            Image = new Uri("https://example.com/images/icon-author.png"),
//                        },
//                    },
//                },
//            };


//            {
//                "@type": "BreadcrumbList",
//                "@id": "https://www.daneshjooyar.com/%d8%a2%d9%85%d9%88%d8%b2%d8%b4-%d8%b7%d8%b1%d8%a7%d8%ad%db%8c-%d8%b3%d8%a7%db%8c%d8%aa-%d9%88-%d8%a7%d9%be%d9%84%db%8c%da%a9%db%8c%d8%b4%d9%86-%d8%aa%d8%a7%da%a9%d8%b3%db%8c-%d8%a2%d9%86%d9%84%d8%a7/#breadcrumb",
//                "itemListElement": [
//                    {
//                        "@type": "ListItem",
//                        "position": 1,
//                        "item": {
//                            "@type": "WebPage",
//                            "@id": "https://www.daneshjooyar.com/",
//                            "url": "https://www.daneshjooyar.com/",
//                            "name": "\u0635\u0641\u062d\u0647 \u0646\u062e\u0633\u062a"
//                        }
//                    },
//                    {
//                        "@type": "ListItem",
//                        "position": 2,
//                        "item": {
//                            "@type": "WebPage",
//                            "@id": "https://www.daneshjooyar.com/category/webdesign/",
//                            "url": "https://www.daneshjooyar.com/category/webdesign/",
//                            "name": "\u0637\u0631\u0627\u062d\u06cc \u0633\u0627\u06cc\u062a"
//                        }
//                    },
//                    {
//                        "@type": "ListItem",
//                        "position": 3,
//                        "item": {
//                            "@type": "WebPage",
//                            "@id": "https://www.daneshjooyar.com/category/webdesign/back-end/",
//                            "url": "https://www.daneshjooyar.com/category/webdesign/back-end/",
//                            "name": "\u0628\u0631\u0646\u0627\u0645\u0647 \u0646\u0648\u06cc\u0633\u06cc \u0648\u0628"
//                        }
//                    },
//                    {
//                        "@type": "ListItem",
//                        "position": 4,
//                        "item": {
//                            "@type": "WebPage",
//                            "@id": "https://www.daneshjooyar.com/category/webdesign/back-end/aspnet/",
//                            "url": "https://www.daneshjooyar.com/category/webdesign/back-end/aspnet/",
//                            "name": "\u0622\u0645\u0648\u0632\u0634 ASP.NET"
//                        }
//                    },
//                    {
//                        "@type": "ListItem",
//                        "position": 5,
//                        "item": {
//                            "@type": "WebPage",
//                            "@id": "https://www.daneshjooyar.com/category/webdesign/back-end/aspnet/aspcore/",
//                            "url": "https://www.daneshjooyar.com/category/webdesign/back-end/aspnet/aspcore/",
//                            "name": "\u0622\u0645\u0648\u0632\u0634 Core"
//                        }
//                    },
//                    {
//                        "@type": "ListItem",
//                        "position": 6,
//                        "item": {
//                            "@id": "https://www.daneshjooyar.com/%d8%a2%d9%85%d9%88%d8%b2%d8%b4-%d8%b7%d8%b1%d8%a7%d8%ad%db%8c-%d8%b3%d8%a7%db%8c%d8%aa-%d9%88-%d8%a7%d9%be%d9%84%db%8c%da%a9%db%8c%d8%b4%d9%86-%d8%aa%d8%a7%da%a9%d8%b3%db%8c-%d8%a2%d9%86%d9%84%d8%a7/#webpage"
//                        }
//                    }
//                ]
//            },
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Sets the person.
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <param name="description">The description (usually users bio).</param>
//        /// <param name="url">The profile URL of person.</param>
//        /// <param name="sameAs">The same as (usually social media urls).</param>
//        /// <param name="imageUrl">The image URL.</param>
//        /// <param name="imageWidth">Width of the image.</param>
//        /// <param name="imageHeight">Height of the image.</param>
//        /// <param name="inLanguage">The in language.</param>
//        /// <param name="id">The id to reference later. (e.g "https://site.com/#person")</param>
//        public void SetPerson(string name, string description = null, string url = null, string[] sameAs = null, string imageUrl = null,
//            int? imageWidth = null, int? imageHeight = null, string inLanguage = null, string id = null)
//        {
//            name.EnsureNotNullOrWhiteSpace(nameof(name));

//            Person ??= new();
//            var uri = url?.ToUri();
//            Person.Url = uri;
//            Person.Id = id?.ToUri() ?? uri.Relative("#person");
//            Person.Name = name;
//            Person.Description = description;
//            Person.SameAs = sameAs?.Select(p => p.ToUri()).ToArray();
//            //Person.FamilyName = "last name";
//            //Person.GivenName = "first name";
//            //Person.Email = "email";
//            //Person.Address = "address";
//            //Person.BirthDate = DateTime ?;
//            //Person.ContactPoint = new ContactPoint { };

//            if (imageUrl is not null)
//            {
//                var imageUri = imageUrl.ToUri();
//                Person.Image = new ImageObject
//                {
//                    Url = imageUri,
//                    ContentUrl = imageUri,
//                    Caption = name,
//                    InLanguage = inLanguage,
//                    Width = imageWidth?.ToString(),
//                    Height = imageHeight?.ToString(),
//                };
//            }

//            #region Examples
//            /*
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Schema.NET.Test/Examples/PersonTest.cs

//            {
//                "@type": "Person",
//                "@id": "https://www.daneshjooyar.com/#/schema/person/ed647341df1791e40a99568e907b0531",
//                "name": "\u0645\u06cc\u0644\u0627\u062f \u0639\u0627\u0645\u0631\u06cc",
//                "image": {
//                    "@type": "ImageObject",
//                    "@id": "https://www.daneshjooyar.com/#personlogo",
//                    "inLanguage": "fa-IR",
//                    "url": "https://www.daneshjooyar.com/wp-content/uploads/2019/09/421a3cf79e0de9d2ca1b9de4174c9e77_avatar.jpg",
//                    "contentUrl": "https://www.daneshjooyar.com/wp-content/uploads/2019/09/421a3cf79e0de9d2ca1b9de4174c9e77_avatar.jpg",
//                    "caption": "\u0645\u06cc\u0644\u0627\u062f \u0639\u0627\u0645\u0631\u06cc"
//                },
//                "sameAs": [
//                    "http://www.pumkin.ir"
//                ]
//            }
//            */
//            #endregion
//        }

//        /// <summary>
//        /// Sets the page.
//        /// </summary>
//        /// <param name="url">The URL.</param>
//        /// <param name="name">The name.</param>
//        /// <param name="description">The description.</param>
//        /// <param name="inLanguage">The in language.</param>
//        /// <param name="imageUrl">The image URL.</param>
//        /// <param name="imageWidth">Width of the image.</param>
//        /// <param name="imageHeight">Height of the image.</param>
//        /// <param name="breadcrumbId">The breadcrumb identifier.</param>
//        /// <param name="isPartOfId">The is part of identifier.</param>
//        /// <param name="datePublished">The date published.</param>
//        /// <param name="dateModified">The date modified.</param>
//        /// <param name="id">The identifier.</param>
//        /// <param name="pageType">Type of the page.</param>
//        /// <returns></returns>
//        public void SetPage(string url, string name, string description, string inLanguage = null, string imageUrl = null, int? imageWidth = null, int? imageHeight = null,
//            string breadcrumbId = null, string isPartOfId = null, DateTimeOffset? datePublished = null, DateTimeOffset? dateModified = null, string id = null,
//            PageType pageType = PageType.WebPage)
//        {
//            url.EnsureNotNullOrWhiteSpace(nameof(url));
//            name.EnsureNotNullOrWhiteSpace(nameof(name));
//            description.EnsureNotNullOrWhiteSpace(nameof(description));

//            Page ??= CreatePage(pageType);
//            var uri = url.ToUri();
//            Page.Id = id?.ToUri() ?? uri.Relative("#page");
//            Page.Url = uri;
//            Page.Name = name;
//            Page.Headline = name;
//            Page.Description = description;
//            Page.InLanguage = inLanguage;
//            Page.DatePublished = datePublished;
//            Page.DateCreated = datePublished;
//            Page.DateModified = dateModified;

//            if (imageUrl is not null)
//            {
//                var imageUri = imageUrl.ToUri();
//                Page.PrimaryImageOfPage = new ImageObject
//                {
//                    Url = imageUri,
//                    ContentUrl = imageUri,
//                    Width = imageWidth?.ToString(),
//                    Height = imageHeight?.ToString(),
//                    Name = name,
//                    Caption = name,
//                    InLanguage = inLanguage
//                };
//            }

//            //Set page's breadcrumb
//            var breadcrumbRefId = breadcrumbId?.ToUri() ?? Breadcrumb?.Id;
//            if (breadcrumbRefId is not null)
//                Page.Breadcrumb = new BreadcrumbListRefId(breadcrumbRefId);

//            //Set images to page image
//            if (Images?.Count > 0)
//            {
//                if (Images.All(p => p.Id is not null))
//                    Page.Image = Images.Select(p => new ImageRefId(p.Id)).Cast<IImageObject>().ToList();
//                else
//                    Page.Image = new(Images);
//            }

//            var isPartOfRefId = isPartOfId?.ToUri() ?? WebSite?.Id;
//            if (isPartOfRefId is not null)
//                Page.IsPartOf = new WebSiteRefId(isPartOfRefId);


//            /*
//            {
//                "@type": "WebPage",
//                "author": {
//                    "@id": "https://moz.com#identity"
//                },
//                "copyrightHolder": {
//                    "@id": "https://moz.com#identity"
//                },
//                "copyrightYear": "2019",
//                "creator": {
//                    "@id": "https://moz.com#creator"
//                },
//                "dateModified": "2021-06-24T15:51:48-07:00",
//                "datePublished": "2019-04-01T12:53:00-07:00",
//                "description": "New to SEO? Looking for higher rankings and traffic through Search Engine Optimization? The Beginner's Guide to SEO has been read over 10 million times.",
//                "headline": "Beginner's Guide to SEO [Search Engine Optimization]",
//                "image": {
//                    "@type": "ImageObject",
//                    "url": "https://moz.com/cms/_1200x630_crop_center-center_82_none/BGSEO_intro_V3-01.png?mtime=20190327131722&focal=none&tmtime=20210628145917"
//                },
//                "inLanguage": "en-us",
//                "mainEntityOfPage": "https://moz.com/beginners-guide-to-seo",
//                "name": "Beginner's Guide to SEO [Search Engine Optimization]",
//                "publisher": {
//                    "@id": "https://moz.com#creator"
//                },
//                "url": "https://moz.com/beginners-guide-to-seo"
//            }
//            */

//            static WebPage CreatePage(PageType pageType)
//            {
//                return pageType switch
//                {
//                    PageType.WebPage => new WebPage(),
//                    PageType.AboutPage => new AboutPage(),
//                    PageType.ContactPage => new ContactPage(),
//                    PageType.FAQPage => new FAQPage(),
//                    PageType.QAPage => new QAPage(),
//                    PageType.ProfilePage => new ProfilePage(),
//                    PageType.CheckoutPage => new CheckoutPage(),
//                    PageType.SearchResultsPage => new SearchResultsPage(),
//                    PageType.ItemPage => new ItemPage(),
//                    _ => throw new ArgumentOutOfRangeException(nameof(pageType))
//                };
//            }
//        }

//        /// <summary>
//        /// Sets the article.
//        /// </summary>
//        /// <param name="url">The URL.</param>
//        /// <param name="name">The name.</param>
//        /// <param name="description">The description.</param>
//        /// <param name="datePublished">The date published.</param>
//        /// <param name="dateModified">The date modified.</param>
//        /// <param name="authorName">Name of the author.</param>
//        /// <param name="imageUrl">The image URL.</param>
//        /// <param name="imageWidth">Width of the image.</param>
//        /// <param name="imageHeight">Height of the image.</param>
//        /// <param name="isNews">if set to <c>true</c> [is news].</param>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public void SetArticle(string url, string name, string description, DateTimeOffset? datePublished = null, DateTimeOffset? dateModified = null, string authorName = null,
//            string imageUrl = null, int? imageWidth = null, int? imageHeight = null, bool isNews = false, string id = null)
//        {
//            //NewsArticle
//            Article ??= isNews ? new NewsArticle() : new();

//            var uri = url.ToUri();
//            Article.Id = id?.ToUri() ?? uri.Relative("#article");
//            Article.Url = uri;
//            Article.Name = name;
//            Article.Headline = name;
//            Article.Description = description;
//            Article.Image = new(Images);
//            Article.DateCreated = datePublished;
//            Article.DatePublished = datePublished;
//            Article.DateModified = dateModified;
//            Article.Author = new Person
//            {
//                Name = authorName,
//            };
//            var imageUri = imageUrl?.ToUri();
//            Article.Image = new ImageObject
//            {
//                Url = imageUri,
//                ContentUrl = imageUri,
//                Caption = name,
//                Width = imageWidth?.ToString(),
//                Height = imageHeight?.ToString()
//            };
//            //Article.Publisher = new Organization
//            //{
//            //};

//            /* Examples
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/NewsArticleTest.cs
//            var article = new NewsArticle()
//            {
//                MainEntityOfPage = new Uri("https://google.com/article"), // Ignored
//                Headline = "Article headline", // Recommended
//                Image = new ImageObject() // Recommended
//                {
//                    Url = new Uri("https://google.com/thumbnail1.jpg"), // Recommended
//                    Height = 800, // Recommended
//                    Width = 800, // Recommended
//                },
//                DatePublished = new DateTime(2015, 2, 5), // Ignored
//                DateModified = new DateTimeOffset(2015, 2, 5, 9, 20, 0, TimeSpan.Zero), // Ignored
//                Author = new Person() // Ignored
//                {
//                    Name = "John Doe", // Ignored
//                },
//                Publisher = new Organization() // Ignored
//                {
//                    Name = "Google",
//                    Logo = new ImageObject() // Ignored
//                    {
//                        Url = new Uri("https://google.com/logo.jpg"), // Ignored
//                        Height = 60, // Ignored
//                        Width = 600, // Ignored
//                    },
//                },
//                Description = "A most wonderful article", // Ignored
//            };

//            {
//                "@context": "https://schema.org",
//                "@type": "NewsArticle",
//                "mainEntityOfPage": {
//                    "@type": "WebPage",
//                    "@id": "https://google.com/article"
//                },
//                "headline": "Article headline",
//                "image": [
//                    "https://example.com/photos/1x1/photo.jpg",
//                    "https://example.com/photos/4x3/photo.jpg",
//                    "https://example.com/photos/16x9/photo.jpg"
//                ],
//                "datePublished": "2015-02-05T08:00:00+08:00",
//                "dateModified": "2015-02-05T09:20:00+08:00",
//                "author": {
//                    "@type": "Person",
//                    "name": "John Doe"
//                },
//                "publisher": {
//                    "@type": "Organization",
//                    "name": "Google",
//                    "logo": {
//                        "@type": "ImageObject",
//                        "url": "https://google.com/logo.jpg"
//                    }
//                }
//            }
//            */
//            return default;
//        }

//        public void SetVideo()
//        {
//            /* Examples
//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/VideoObjectTest.cs
//            var videoObject = new VideoObject()
//            {
//                Name = "Title", // Required
//                Description = "Video description", // Required
//                ThumbnailUrl = new Uri("https://www.example.com/thumbnail.jpg"), // Required
//                Expires = new DateTime(2016, 2, 5), // Recommended
//                UploadDate = new DateTime(2015, 2, 5), // Required
//                Duration = new TimeSpan(0, 1, 33), // Recommended
//                Publisher = new Organization()
//                {
//                    Name = "Example Publisher", // Required
//                    Logo = new ImageObject() // Required
//                    {
//                        Height = 60, // Required
//                        Url = new Uri("https://example.com/logo.jpg"), // Required
//                        Width = 600, // Required
//                    },
//                },
//                ContentUrl = new Uri("https://www.example.com/video123.flv"), // Recommended
//                EmbedUrl = new Uri("https://www.example.com/videoplayer.swf?video=123"), // Recommended
//                InteractionStatistic = new InteractionCounter()
//                {
//                    UserInteractionCount = 2347, // Recommended
//                },
//            }; 
//            */
//        }

//        public void SetAudio()
//        {
//            /* Examples
//            Schema.NET.MusicRecording;
//            Schema.NET.AudioObject;

//            //https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/MusicRecordingTest.cs
//            var musicRecording = new MusicRecording()
//            {
//                Name = "2 + 2 = 5", // Required
//                Identifier = "37kUGdEJJ7NaMl5LFW4EA4", // Recommended
//                Image = new ImageObject() // Recommended
//                {
//                    ContentUrl = new Uri("https://is4-ssl.mzstatic.com/image/thumb/Music69/v4/cc/1c/90/cc1c9039-c3ba-4256-e251-1687df46cb0a/cover.jpg/1400x1400bb.jpeg"), // Required
//                },
//                SameAs = new Uri("https://music.apple.com/us/album/2-2-5/1097863576?i=1097863810"), // Recommended
//                Url = new Uri("https://open.spotify.com/track/37kUGdEJJ7NaMl5LFW4EA4"), // Recommended
//                DatePublished = new DateTime(2003, 5, 26), // Recommended
//                IsFamilyFriendly = true, // Recommended
//                Position = "1.1", // Recommended
//                ByArtist = new MusicGroup() // Recommended
//                {
//                    Name = "Radiohead", // Required
//                    Identifier = "4Z8W4fKeB5YxbusRsdQVPb", // Recommended
//                },
//                Duration = new TimeSpan(0, 0, 3, 19, 360), // Recommended
//                InAlbum = new MusicAlbum() // Recommended
//                {
//                    Name = "Hail To the Thief", // Required
//                    Identifier = "1oW3v5Har9mvXnGk0x4fHm", // Recommended
//                },
//                IsrcCode = "GBAYE0300801", // Recommended
//            }; 
//            */
//        }

//        public void SetBook()
//        {
//            /* Examples
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/BookTest.cs
//            var book = new Book()
//            {
//                Id = new Uri("https://example.com/book/1"),
//                Name = "The Catcher in the Rye",
//                Author = new Person()
//                {
//                    Name = "J.D. Salinger",
//                },
//                Url = new Uri("https://www.barnesandnoble.com/store/info/offer/JDSalinger"),
//                WorkExample = new List<ICreativeWork>()
//                {
//                    new Book()
//                    {
//                        Isbn = "031676948",
//                        BookEdition = "2nd Edition",
//                        BookFormat = BookFormatType.Hardcover,
//                        PotentialAction = new ReadAction()
//                        {
//                            Target = new EntryPoint()
//                            {
//                                UrlTemplate = "https://www.barnesandnoble.com/store/info/offer/0316769487?purchase=true",
//                                ActionPlatform = new List<Uri>()
//                                {
//                                    new Uri("https://schema.org/DesktopWebPlatform"),
//                                    new Uri("https://schema.org/IOSPlatform"),
//                                    new Uri("https://schema.org/AndroidPlatform"),
//                                },
//                            },
//                            ExpectsAcceptanceOf = new Offer()
//                            {
//                                Price = 6.99M,
//                                PriceCurrency = "USD",
//                                EligibleRegion = new Country()
//                                {
//                                    Name = "US",
//                                },
//                                Availability = ItemAvailability.InStock,
//                            },
//                        },
//                    },
//                    new Book()
//                    {
//                        Isbn = "031676947",
//                        BookEdition = "1st Edition",
//                        BookFormat = BookFormatType.EBook,
//                        PotentialAction = new ReadAction()
//                        {
//                            Target = new EntryPoint()
//                            {
//                                UrlTemplate = "https://www.barnesandnoble.com/store/info/offer/031676947?purchase=true",
//                                ActionPlatform = new List<Uri>()
//                                {
//                                    new Uri("https://schema.org/DesktopWebPlatform"),
//                                    new Uri("https://schema.org/IOSPlatform"),
//                                    new Uri("https://schema.org/AndroidPlatform"),
//                                },
//                            },
//                            ExpectsAcceptanceOf = new Offer()
//                            {
//                                Price = 1.99M,
//                                PriceCurrency = "USD",
//                                EligibleRegion = new Country()
//                                {
//                                    Name = "UK",
//                                },
//                                Availability = ItemAvailability.InStock,
//                            },
//                        },
//                    },
//                },
//            };
//            */
//        }

//        public void SetCourse()
//        {
//            /* Examples
//            https://github.com/RehanSaeed/Schema.NET/blob/main/Tests/Test/Examples/CourseTest.cs7
//            var course = new Course()
//            {
//                Name = "Introduction to Computer Science and Programming", // Required
//                Description = "Introductory CS course laying out the basics.", // Required
//                Provider = new Organization() // Recommended
//                {
//                    Name = "University of Technology - Eureka",
//                    SameAs = new Uri("https://www.ut-eureka.edu"),
//                },
//            };
//            */
//        }
//        #endregion
//    }
//}
