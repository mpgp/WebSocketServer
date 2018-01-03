// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the Helper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer
{
    using Messages;
    using Messages.Payloads;

    /// <summary>
    /// The helper.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// The build message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <typeparam name="T">Message Payload</typeparam>
        /// <returns>
        /// The WebSocketMessage.
        /// </returns>
        public static WebSocketMessage<T> BuildMessage<T>(T message) where T : BaseMessage
        {
            return new WebSocketMessage<T>()
            {
                Type = message.ToString(),
                Payload = message
            };
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <typeparam name="T">Any type</typeparam>
        public static void Send<T>(this Fleck2.Interfaces.IWebSocketConnection socket, WebSocketMessage<T> message) where T : BaseMessage
        {
            socket.Send(Serialize(message));
        }

        /// <summary>
        /// Serializes specified object to a JSON string.
        /// </summary>
        /// <param name="message">
        /// Specified object type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize(object message) => Newtonsoft.Json.JsonConvert.SerializeObject(message);
    }
}
