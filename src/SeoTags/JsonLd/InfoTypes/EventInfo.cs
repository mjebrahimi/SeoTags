using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoTags
{
    /// <summary>
    /// An event happening at a certain time and location, such as a concert, lecture, or festival.
    /// Ticketing information may be added via the [[offers]] property. Repeated events may be structured as separate Event objects.
    /// </summary>
    /// <seealso cref="ThingInfo{Event}" />
    public class EventInfo : ThingInfo<Event>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public IEnumerable<ImageInfo> Images { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public PlaceInfo Location { get; set; }

        /// <summary>
        /// Represents the status of evenet; particularly useful when an event is cancelled or rescheduled.
        /// </summary>
        public EventStatusType? EventStatus { get; set; }

        /// <summary>
        /// Gets or sets the offers. (one for each ticket type)
        /// </summary>
        public IEnumerable<OfferInfo> Offers { get; set; }

        /// <summary>
        /// Gets or sets the participants performing at the event, such as artists and comedians.
        /// Use a PerformingGroup or Person, one for each performer.
        /// </summary>
        public IEnumerable<PersonInfo> Performers { get; set; } //PerformingGroup, and ...

        /// <summary>
        /// Gets or sets the organizer of event. The person or organization that is hosting the event.
        /// </summary>
        public OrganizationInfo Organizer { get; set; }

        /// <summary>
        /// Converts to <see cref="Event"/>.
        /// </summary>
        /// <returns>A <see cref="Event"/> instance</returns>
        public override Event ConvertTo()
        {
            Name.EnsureNotNullOrWhiteSpace(nameof(Name));
            StartDate.EnsureNotNull(nameof(StartDate));
            Location.EnsureNotNull(nameof(Location));

            //More info: https://developers.google.com/search/docs/data-types/event
            return new()
            {
                Name = Name,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate,
                Image = new(Images?.Select(p => p.ConvertTo())),
                Location = Location?.ConvertTo(),
                EventStatus = EventStatus,
                //"eventAttendanceMode": "https://schema.org/OfflineEventAttendanceMode",
                //"eventAttendanceMode": "https://schema.org/OnlineEventAttendanceMode",
                //"eventAttendanceMode": "https://schema.org/MixedEventAttendanceMode",
                Offers = new(Offers?.Select(p => p.ConvertTo())),
                Performer = new(Performers?.Select(p => p.ConvertTo())),
                Organizer = Organizer?.ConvertTo()
            };

            //Type of events:
            //Schema.NET.EventReservation;
            //Schema.NET.EventVenue;
            //Schema.NET.BusinessEvent;
            //Schema.NET.ChildrensEvent;
            //Schema.NET.ComedyEvent;
            //Schema.NET.DanceEvent;
            //Schema.NET.DeliveryEvent;
            //Schema.NET.EducationEvent;
            //Schema.NET.ExhibitionEvent;
            //Schema.NET.FoodEvent;
            //Schema.NET.LiteraryEvent;
            //Schema.NET.MusicEvent;
            //Schema.NET.OnDemandEvent;
            //Schema.NET.PublicationEvent;
            //Schema.NET.SaleEvent;
            //Schema.NET.ScreeningEvent;
            //Schema.NET.SocialEvent;
            //Schema.NET.SportsEvent;
            //Schema.NET.TheaterEvent;
            //Schema.NET.TheaterEvent;
            //Schema.NET.VisualArtsEvent;
        }
    }

    /// <summary>
    /// A place.
    /// </summary>
    /// <seealso cref="ThingInfo{Place}" />
    public class PlaceInfo : ThingInfo<Place>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceInfo"/> class by specified address.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="address">The address.</param>
        public PlaceInfo(string name, string address)
        {
            name.EnsureNotNullOrWhiteSpace(nameof(name));
            address.EnsureNotNullOrWhiteSpace(nameof(address));

            Name = name;
            Address = address;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceInfo"/> class by specified postal address.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="postalAddress">The postal address.</param>
        public PlaceInfo(string name, PostalAddressInfo postalAddress)
        {
            name.EnsureNotNullOrWhiteSpace(nameof(name));
            postalAddress.EnsureNotNull(nameof(postalAddress));

            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceInfo"/> class by specified url of virtual location.
        /// </summary>
        /// <param name="virtualLocationUrl">The virtual location URL.</param>
        public PlaceInfo(string virtualLocationUrl)
        {
            virtualLocationUrl.EnsureNotNullOrWhiteSpace(nameof(virtualLocationUrl));
            VirtualLocationUrl = virtualLocationUrl;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the postal address.
        /// </summary>
        public PostalAddressInfo PostalAddress { get; set; }

        /// <summary>
        /// Gets or sets the url of virtual location.
        /// </summary>
        public string VirtualLocationUrl { get; set; }

        /// <summary>
        /// Converts to <see cref="Place"/>.
        /// </summary>
        /// <returns>A <see cref="Place"/> instance</returns>
        public override Place ConvertTo()
        {
            if (VirtualLocationUrl is not null)
            {
                return new VirtualLocation
                {
                    Url = VirtualLocationUrl.ToUri()
                };
            }

            if (Address is not null && PostalAddress is not null)
                throw new("Only address or postal address, not both.");

            var place = new Place()
            {
                Name = Name,
            };
            if (Address is not null)
                place.Address = Address;
            else
                place.Address = PostalAddress.ConvertTo();

            return place;
        }
    }

    /// <summary>
    /// An address
    /// </summary>
    /// <seealso cref="ThingInfo{PostalAddress}" />
    public class PostalAddressInfo : ThingInfo<PostalAddress>
    {
        /// <summary>
        /// Gets or sets the street address. For example, "2635 Homestead Rd".
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the locality in which the street address is, and which is in the region. For example "Santa Clara".
        /// </summary>
        public string AddressLocality { get; set; }

        /// <summary>
        /// Gets or sets the postal code. For example, 95051.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the region in which the locality is, and which is in the country. For example "CA".
        /// </summary>
        public string AddressRegion { get; set; }

        /// <summary>
        /// Gets or sets the country. For example, "USA" or "US" (two-letter country code).
        /// </summary>
        public string AddressCountry { get; set; }

        /// <summary>
        /// Converts to <see cref="PostalAddress"/>.
        /// </summary>
        /// <returns>A <see cref="PostalAddress"/> instance</returns>
        public override PostalAddress ConvertTo()
        {
            return new()
            {
                StreetAddress = StreetAddress,
                AddressLocality = AddressLocality,
                PostalCode = PostalCode,
                AddressRegion = AddressRegion,
                AddressCountry = AddressCountry,
            };
        }
    }

    /// <summary>
    /// A virtual location for online event.
    /// </summary>
    /// <seealso cref="Schema.NET.Thing" />
    public class VirtualLocation : Place
    {
        /// <inheritdoc/>
        public override string Type => "VirtualLocation";
    }
}
