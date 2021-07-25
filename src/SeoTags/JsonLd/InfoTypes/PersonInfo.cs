using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// A person
    /// </summary>
    /// <seealso cref="ThingInfo{Person}" />
    public class PersonInfo : ThingInfo<Person>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/person-url/#person")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#person"); }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the social media urls. (SameAs property)
        /// </summary>
        public IEnumerable<string> SocialMediaUrls { get; set; }

        /// <summary>
        /// Gets or sets the image. (instance of ImageInfo or ImageInfo.ReferTo method)
        /// </summary>
        public ImageInfo Image { get; set; }

        /// <summary>
        /// Converts to <see cref="Person"/>.
        /// </summary>
        /// <returns>A <see cref="Person"/> instance</returns>
        public override Person ConvertTo()
        {
            if (referId is not null)
                return new PersonRefId(referId);

            Name.EnsureNotNullOrWhiteSpace(nameof(Name));

            return new()
            {
                Id = id,
                Url = url,
                Name = Name,
                Description = Description,
                SameAs = SocialMediaUrls?.Select(p => p.ToUri()).ToArray(),
                Image = Image?.ConvertTo(),

                //Person.FamilyName = "last name",
                //Person.GivenName = "first name",
                //Person.Email = "email",
                //Person.Address = "address",
                //Person.BirthDate = DateTime ?,
                //WorksFor = default,
                //Person.ContactPoint = default,
            };
        }

        /// <summary>
        /// Refers to a person.
        /// </summary>
        /// <param name="personInfo">The PersonInfo instance.</param>
        /// <returns>PersonInfo</returns>
        public static PersonInfo ReferTo(PersonInfo personInfo)
        {
            personInfo.EnsureNotNull(nameof(personInfo));
            personInfo.id.EnsureNotNull(nameof(personInfo.Id));
            return new() { referId = personInfo.id };
        }

        /// <summary>
        /// Refers to a person.
        /// </summary>
        /// <param name="id">The identifier. (PersonInfo.Id)</param>
        /// <returns>PersonInfo</returns>
        public static PersonInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="PersonInfo"/> to refers to the person.
        /// </summary>
        /// <param name="id">The identifier. (PersonInfo.Id)</param>
        /// <returns>PersonInfo</returns>
        public static implicit operator PersonInfo(string id) => ReferTo(id);
    }
}
