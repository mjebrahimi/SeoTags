using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SeoTags
{
    /// <summary>
    /// JSON-LD
    /// </summary>
    public class JsonLd
    {
        private readonly DateTimeOffsetToIso8601ContractResolver dateTimeOffsetToIso8601ContractResolver;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLd"/> class.
        /// </summary>
        public JsonLd()
        {
            dateTimeOffsetToIso8601ContractResolver = new()
            {
                RenderDateAsUTC = true
            };
            jsonSerializerSettings = new()
            {
                Converters = [new StringEnumConverter()],
                ContractResolver = dateTimeOffsetToIso8601ContractResolver,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };
        }

        /// <summary>
        /// Gets or sets a value indicating render date times as UTC. (default: <see langword="true"/>)
        /// </summary>
        public bool RenderDateAsUTC
        {
            get => dateTimeOffsetToIso8601ContractResolver.RenderDateAsUTC;
            set => dateTimeOffsetToIso8601ContractResolver.RenderDateAsUTC = value;
        }

        /// <summary>
        /// Gets or sets a value indicating escape non ASCII characters. (Default is <see langword="false"/>)
        /// </summary>
        /// <value>
        ///   <c>true</c> if escape non ASCII characters; otherwise, <c>false</c>.
        /// </value>
        public bool EscapeNonAsciiCharacters { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating graph mode enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if graph mode enabled; otherwise, <c>false</c>.
        /// </value>
        public bool GraphEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the things.
        /// </summary>
        public List<Thing> Things { get; set; } = [];

        /// <summary>
        /// Renders the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Render(StringBuilder builder)
        {
            if (Things?.Count > 0)
            {
                if (GraphEnabled)
                {
                    var graph = new JsonLdGraph { Things = Things };
                    var json = graph.ToString(jsonSerializerSettings);
                    if (EscapeNonAsciiCharacters)
                        json = json.EscapeNonAscii();
                    builder.Append("<script type=\"application/ld+json\">").Append(json).AppendLine("</script>");
                }
                else
                {
                    foreach (var thing in Things)
                    {
                        var json = thing.ToString(jsonSerializerSettings);
                        if (EscapeNonAsciiCharacters)
                            json = json.EscapeNonAscii();
                        builder.Append("<script type=\"application/ld+json\">").Append(json).AppendLine("</script>");
                    }
                }
            }
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="productInfo">The product information.</param>
        public JsonLd AddProduct(ProductInfo productInfo)
        {
            Add(productInfo);
            return this;
        }

        /// <summary>
        /// Adds the article.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        public JsonLd AddArticle(ArticleInfo articleInfo)
        {
            Add(articleInfo);
            return this;
        }

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        public JsonLd AddEvent(EventInfo eventInfo)
        {
            Add(eventInfo);
            return this;
        }

        /// <summary>
        /// Adds the page.
        /// </summary>
        /// <param name="pageInfo">The page information.</param>
        public JsonLd AddPage(PageInfo pageInfo)
        {
            Add(pageInfo);
            return this;
        }

        /// <summary>
        /// Adds the organization.
        /// </summary>
        /// <param name="organizationInfo">The organization information.</param>
        public JsonLd AddOrganization(OrganizationInfo organizationInfo)
        {
            Add(organizationInfo);
            return this;
        }

        /// <summary>
        /// Adds the website.
        /// </summary>
        /// <param name="webSiteInfo">The web site information.</param>
        public JsonLd AddWebsite(WebSiteInfo webSiteInfo)
        {
            Add(webSiteInfo);
            return this;
        }

        /// <summary>
        /// Adds the breadcrumb.
        /// </summary>
        /// <param name="breadcrumbInfo">The breadcrumb information.</param>
        public JsonLd AddBreadcrumb(BreadcrumbInfo breadcrumbInfo)
        {
            Add(breadcrumbInfo);
            return this;
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="imageInfo">The image information.</param>
        public JsonLd AddImage(ImageInfo imageInfo)
        {
            Add(imageInfo);
            return this;
        }

        /// <summary>
        /// Adds the person information.
        /// </summary>
        /// <param name="personInfo">The person information.</param>
        public JsonLd AddPerson(PersonInfo personInfo)
        {
            Add(personInfo);
            return this;
        }

        /// <summary>
        /// Adds the review information.
        /// </summary>
        /// <param name="reviewInfo">The review information.</param>
        public JsonLd AddReview(ReviewInfo reviewInfo)
        {
            Add(reviewInfo);
            return this;
        }

        /// <summary>
        /// Adds the specified thing information.
        /// </summary>
        /// <param name="thingInfo">The thing information.</param>
        public JsonLd Add(IThingInfo thingInfo)
        {
            Things ??= [];
            Things.Add(thingInfo.ToThing());
            return this;
        }

        #region ContractResolver and JsonConverter
        private sealed class DateTimeOffsetToIso8601ContractResolver : DefaultContractResolver
        {
            private readonly DateTimeOffsetToIso8601JsonConverter dateTimeOffsetToIso8601JsonConverter = new();
            public bool RenderDateAsUTC
            {
                get => dateTimeOffsetToIso8601JsonConverter.RenderDateAsUTC;
                set => dateTimeOffsetToIso8601JsonConverter.RenderDateAsUTC = value;
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jsonProperty = base.CreateProperty(member, memberSerialization);
                if (jsonProperty.Converter is DateTimeToIso8601DateValuesJsonConverter)
                    jsonProperty.Converter = dateTimeOffsetToIso8601JsonConverter;
                return jsonProperty;
            }

            private sealed class DateTimeOffsetToIso8601JsonConverter : DateTimeToIso8601DateValuesJsonConverter
            {
                public bool RenderDateAsUTC { get; set; }

                public override void WriteObject(JsonWriter writer, object value, JsonSerializer serializer)
                {
                    writer.EnsureNotNull(nameof(writer));
                    serializer.EnsureNotNull(nameof(serializer));

                    if (value is DateTimeOffset dateTimeOffset)
                        writer.WriteValue(dateTimeOffset.ToStringIso8601(RenderDateAsUTC));
                    else
                        base.WriteObject(writer, value, serializer);
                }
            }
        }
        #endregion
    }

    #region Examples
    //Schema.Org References
    //https://schema.org/docs/gs.html
    //https://schema.org/docs/schemas.html
    //https://schema.org/Organization
    //https://schema.org/WebSite
    //https://schema.org/Person
    //https://schema.org/ImageObject
    //https://schema.org/Product
    //https://schema.org/Offer
    //https://schema.org/AggregateOffer
    //https://schema.org/Review
    //https://schema.org/AggregateRating
    //https://schema.org/Book
    //https://schema.org/Event
    //https://schema.org/VideoObject
    //https://schema.org/AudioObject

    //Google Reference
    //https://developers.google.com/search/docs/advanced/structured-data/intro-structured-data
    //https://developers.google.com/search/docs/advanced/structured-data/search-gallery
    //https://developers.google.com/search/docs/advanced/structured-data/sd-policies
    //https://codelabs.developers.google.com/codelabs/structured-data/index.html#2
    //https://codelabs.developers.google.com/codelabs/structured-data/index.html#3
    //https://developers.google.com/search/docs/data-types/logo
    //https://developers.google.com/search/docs/data-types/article
    //https://developers.google.com/search/docs/data-types/product
    //https://developers.google.com/search/docs/data-types/breadcrumb
    //https://developers.google.com/search/docs/data-types/image-license-metadata
    //https://developers.google.com/search/docs/data-types/video
    //https://developers.google.com/search/docs/data-types/movie
    //https://developers.google.com/search/docs/data-types/book
    //https://developers.google.com/search/docs/data-types/event
    //https://developers.google.com/search/docs/data-types/course
    //https://developers.google.com/search/docs/data-types/local-business
    //https://developers.google.com/search/docs/data-types/factcheck
    //https://developers.google.com/search/docs/data-types/faqpage
    //https://developers.google.com/search/docs/data-types/software-app
    //https://developers.google.com/search/docs/data-types/home-activities

    //Examples
    //https://jsonld.com/blog/
    //https://jsonld.com/organization/
    //https://jsonld.com/website/
    //https://jsonld.com/web-page/
    //https://jsonld.com/product/
    //https://jsonld.com/article/
    //https://jsonld.com/news-article/
    //https://jsonld.com/person/
    //https://jsonld.com/review/
    //https://jsonld.com/breadcrumb/
    //https://jsonld.com/book/
    //https://jsonld.com/video/
    //https://jsonld.com/event/
    //https://jsonld.com/json-ld-course/

    //Generators
    //https://technicalseo.com/tools/schema-markup-generator/
    //https://webcode.tools/generators/json-ld
    //https://jsonld.com/json-ld-generator/
    //https://www.google.com/webmasters/markup-helper/

    //Validator / Tool
    //https://search.google.com/test/rich-results
    //https://search.google.com/structured-data/testing-tool
    #endregion
}
