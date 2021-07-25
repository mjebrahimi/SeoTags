using Schema.NET;
using System;

namespace SeoTags
{
    /// <summary>
    /// A brand is a name used by an organization or business person for labeling a product
    /// </summary>
    /// <seealso cref="ThingInfo{Thing}" />
    public class BrandInfo : ThingInfo<Thing>
    {
        private Organization organization;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Converts to <see cref="Brand"/> or <see cref="Organization"/>.
        /// </summary>
        /// <returns>A <see cref="Brand"/> or <see cref="Organization"/> instance</returns>
        public override Thing ConvertTo()
        {
            if (organization is not null)
                return organization;

            Name.EnsureNotNullOrWhiteSpace(nameof(Name));

            return new Brand { Name = Name };
        }

        /// <summary>
        /// Refers to an organization.
        /// </summary>
        /// <param name="organizationInfo">The OrganizationInfo instance.</param>
        /// <returns>OrganizationInfo</returns>
        public static BrandInfo ReferTo(OrganizationInfo organizationInfo)
        {
            organizationInfo.EnsureNotNull(nameof(organizationInfo));
            if (organizationInfo.Id is not null)
            {
                return new()
                {
                    organization = new OrganizationRefId(organizationInfo.Id)
                };
            }
            else if (string.IsNullOrWhiteSpace(organizationInfo.Name) is false)
            {
                return new()
                {
                    organization = new() { Name = organizationInfo.Name }
                };
            }

            throw new InvalidOperationException("The organizationInfo.Id is null and Name is null or whitespace.");
        }

        /// <summary>
        /// Refers to an organization.
        /// </summary>
        /// <param name="id">The identifier. (OrganizationInfo.Id)</param>
        /// <returns>BrandInfo</returns>
        public static BrandInfo ReferTo(string id)
        {
            id.EnsureNotNullOrWhiteSpace(nameof(id));
            var organizationInfo = OrganizationInfo.ReferTo(id).ConvertTo();
            return new() { organization = organizationInfo };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="BrandInfo"/> to refers to the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>BrandInfo</returns>
        public static implicit operator BrandInfo(string id) => ReferTo(id);

        /// <summary>
        /// Performs an implicit conversion from <see cref="OrganizationInfo"/> to <see cref="BrandInfo"/> to refers to the organization.
        /// </summary>
        /// <param name="organizationInfo">The OrganizationInfo instance.</param>
        /// <returns>BrandInfo</returns>
        public static implicit operator BrandInfo(OrganizationInfo organizationInfo) => ReferTo(organizationInfo);
    }
}
