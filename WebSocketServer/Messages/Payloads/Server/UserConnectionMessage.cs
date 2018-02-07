// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserConnectionMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the UserConnectionMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Server
{
    /// <inheritdoc />
    public class UserConnectionMessage : BaseMessage
    {
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Status { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.UserConnectionMessage;
    }
}
