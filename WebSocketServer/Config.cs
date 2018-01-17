// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the Config type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The config.
    /// </summary>
    internal static class Config
    {
        /// <summary>
        /// Initializes static members of the <see cref="Config"/> class.
        /// </summary>
        static Config()
        {
            Values = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        private static IConfiguration Values { get; }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Get(string key)
        {
            return Values[key];
        }
    }
}
