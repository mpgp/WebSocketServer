// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthMessage.StatusCode.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the AuthMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages
{
    /// <inheritdoc />
    /// <summary>
    /// The auth message.
    /// </summary>
    public partial class AuthMessage
    {
        /// <summary>
        /// The status code.
        /// </summary>
        public enum StatusCode
        {
            /// <summary>
            /// The error.
            /// </summary>
            Error,

            /// <summary>
            /// The success.
            /// </summary>
            Success
        }
    }
}
