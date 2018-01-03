// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebSocketMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the WebSocketMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages
{
    /// <summary>
    /// The web socket message.
    /// </summary>
    /// <typeparam name="T">Message Payload</typeparam>
    public class WebSocketMessage<T> where T : Payloads.BaseMessage
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public T Payload { get; set; }
    }
}
