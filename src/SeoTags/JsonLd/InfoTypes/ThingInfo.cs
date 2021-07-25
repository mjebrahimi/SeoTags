using Schema.NET;

namespace SeoTags
{
    /// <summary>
    /// IThingInfo contract
    /// </summary>
    public interface IThingInfo
    {
        /// <summary>
        /// Converts to <see cref="Thing"/>.
        /// </summary>
        /// <returns>A <see cref="Thing"/> instance</returns>
        Thing ToThing();
    }

    /// <summary>
    /// ThingInfo abstract to convert to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">A <see cref="Thing"/> concrete type</typeparam>
    /// <seealso cref="IThingInfo" />
    public abstract class ThingInfo<T> : IThingInfo
        where T : Thing
    {
        /// <inheritdoc/>
        public Thing ToThing() => ConvertTo();

        /// <summary>
        /// Converts to <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A <typeparamref name="T"/> instance</returns>
        public abstract T ConvertTo();
    }
}
