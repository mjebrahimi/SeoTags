using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// An organization
    /// </summary>
    /// <seealso cref="ThingInfo{Organization}" />
    public class OrganizationInfo : ThingInfo<Organization>
    {
        private Uri referId;
        private Uri id;
        private Uri url;

        /// <summary>
        /// Gets or sets the identifier used to reference in a graph. (e.g "https://site.com/#organization")
        /// </summary>
        public string Id { get => id?.ToString(); set => id = value?.ToUri(); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => url?.ToString(); set => id = (url = value?.ToUri())?.Relative("#organization"); }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alias for the item.
        /// </summary>
        public string AlternateName { get; set; }

        /// <summary>
        /// Gets or sets the social media urls. (SameAs property)
        /// </summary>
        public IEnumerable<string> SocialMediaUrls { get; set; }

        /// <summary>
        /// Gets or sets the logo (and Image property). (an instance of ImageInfo or ImageInfo.ReferTo method)
        /// </summary>
        public ImageInfo Logo { get; set; }

        /// <summary>
        /// Gets or sets the contact points.
        /// </summary>
        public IEnumerable<ContactPointInfo> ContactPoints { get; set; }

        /// <summary>
        /// Converts to <see cref="Organization"/>.
        /// </summary>
        /// <returns>An <see cref="Organization"/> instance</returns>
        public override Organization ConvertTo()
        {
            if (referId is not null)
                return new OrganizationRefId(referId);

            Name.EnsureNotNullOrWhiteSpace(nameof(Name));
            //Url.EnsureNotNullOrWhiteSpace(nameof(Url));
            //Logo.EnsureNotNull(nameof(Logo));

            var organization = new Organization()
            {
                Id = id,
                Url = url,
                Name = Name,
                AlternateName = AlternateName,
                SameAs = SocialMediaUrls?.Select(p => p.ToUri()).ToArray(),
                ContactPoint = new(ContactPoints?.Select(p => p.ConvertTo())),

                //Telephone = default,
                //Email = default,
                //FaxNumber = default,
                //Address = default,
            };

            if (Logo is not null)
            {
                var image = Logo.ConvertTo();
                organization.Logo = image;
                organization.Image = new ImageObjectRefId(image.Id); //image;
            }

            return organization;
        }

        /// <summary>
        /// Refers to a organization.
        /// </summary>
        /// <param name="organizationInfo">The OrganizationInfo instance.</param>
        /// <returns>OrganizationInfo</returns>
        public static OrganizationInfo ReferTo(OrganizationInfo organizationInfo)
        {
            organizationInfo.EnsureNotNull(nameof(organizationInfo));
            organizationInfo.id.EnsureNotNull(nameof(organizationInfo.Id));
            return new() { referId = organizationInfo.id };
        }

        /// <summary>
        /// Refers to a organization.
        /// </summary>
        /// <param name="id">The identifier. (OrganizationInfo.Id)</param>
        /// <returns>OrganizationInfo</returns>
        public static OrganizationInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            return new() { referId = id.ToUri() };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="OrganizationInfo"/> to refers to the organization.
        /// </summary>
        /// <param name="id">The identifier. (OrganizationInfo.Id)</param>
        /// <returns>OrganizationInfo</returns>
        public static implicit operator OrganizationInfo(string id) => ReferTo(id);
    }
}
