// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppContextFactory.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the AppContextFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <inheritdoc />
    public class AppContextFactory : IDesignTimeDbContextFactory<Models.AppContext>
    {
        /// <inheritdoc />
        public Models.AppContext CreateDbContext(string[] args)
        {
            return Models.AppContext.Instance;
        }
    }
}
