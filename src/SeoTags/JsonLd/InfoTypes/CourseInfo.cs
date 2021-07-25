using Schema.NET;

namespace SeoTags
{
    /// <summary>
    /// A course.
    /// </summary>
    /// <seealso cref="ThingInfo{Book}" />
    public class CourseInfo : ThingInfo<Course>
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
        /// Gets or sets the organization that publishes the source content of the course.
        /// For example, UC Berkeley. (an instance of OrganizationInfo or OrganizationInfo.ReferTo method)
        /// </summary>
        public OrganizationInfo Provider { get; set; }

        /// <summary>
        /// Converts to <see cref="Course"/>.
        /// </summary>
        /// <returns>A <see cref="Course"/> instance</returns>
        public override Course ConvertTo()
        {
            Name.EnsureNotNullOrWhiteSpace(nameof(Name));
            Description.EnsureNotNullOrWhiteSpace(nameof(Description));

            return new()
            {
                Name = Name,
                Description = Description,
                Provider = Provider?.ConvertTo()
            };
        }
    }
}
