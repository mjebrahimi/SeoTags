
using SeoTags;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// IServiceCollection extensions for seo tags
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds the seo tag services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The configuration.</param>
        /// <returns>Services</returns>
        public static IServiceCollection AddSeoTags(this IServiceCollection services, Action<SeoInfo> config)
        {
            return services.AddScoped(_ =>
            {
                var seoInfo = new SeoInfo();
                config(seoInfo);
                return seoInfo;
            });
        }

        /// <summary>
        /// Adds the seo tag services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The configuration.</param>
        /// <returns>Services</returns>
        public static IServiceCollection AddSeoTags(this IServiceCollection services, Action<IServiceProvider, SeoInfo> config)
        {
            return services.AddScoped(serviceProvider =>
            {
                var seoInfo = new SeoInfo();
                config(serviceProvider, seoInfo);
                return seoInfo;
            });
        }
    }
}