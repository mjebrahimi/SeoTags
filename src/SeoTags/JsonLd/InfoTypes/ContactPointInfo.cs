using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// A contact point.
    /// </summary>
    /// <seealso cref="ThingInfo{ContactPoint}" />
    public class ContactPointInfo : ThingInfo<ContactPoint>
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// An organization can have different contact points, for different purposes.
        /// For example, a sales contact point, a PR contact point and so on. This property is used to specify the kind of contact point.
        /// </summary>
        public string ContactType { get; set; }

        /// <summary>
        /// Gets or sets the available language. (e.g "English")
        /// </summary>
        public IEnumerable<string> AvailableLanguage { get; set; }

        /// <summary>
        /// The area where this service is provided.
        /// </summary>
        public IEnumerable<string> AreaServed { get; set; }

        //public IEnumerable<ContactPointOption> ContactOption { get; set; }
        //public IEnumerable<TimeSpan?> Opens { get; set; }
        //public IEnumerable<TimeSpan?> Close { get; set; }
        //public IEnumerable<Schema.NET.DayOfWeek?> DayOfWeek { get; set; }

        /// <summary>
        /// Converts to <see cref="ContactPoint"/>.
        /// </summary>
        /// <returns>A <see cref="ContactPoint"/> instance</returns>
        public override ContactPoint ConvertTo()
        {
            //Telephone.EnsureNotNullOrWhiteSpace(nameof(Telephone));
            if (string.IsNullOrWhiteSpace(Email) is true && string.IsNullOrWhiteSpace(Telephone) is true)
                throw new ArgumentException("Either of Email or Telephone is required.");
            else if (string.IsNullOrWhiteSpace(Email) is false && string.IsNullOrWhiteSpace(Telephone) is false)
                throw new ArgumentException("Either of Email or Telephone is required not both.");

            return new()
            {
                Email = Email,
                Telephone = Telephone,
                ContactType = ContactType,
                AvailableLanguage = AvailableLanguage?.ToArray(),
                AreaServed = AreaServed?.ToArray(),
                //ContactOption = ContactOption?.Select(p => (ContactPointOption?)p).ToArray(),
                //HoursAvailable = default
            };
        }
    }
}
