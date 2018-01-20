// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserTokenModel.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the UserTokenModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Models
{
    /// <summary>
    /// The user token model.
    /// </summary>
    public class UserTokenModel
    {
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public long CreatedAt { get; set; }
    
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }
    
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }
    }
}
