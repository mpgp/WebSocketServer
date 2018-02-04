// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Данные для авторизации.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Server
{
    /// <inheritdoc />
    public class AuthMessage : BaseMessage
    {
        /// <inheritdoc />
        public AuthMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthMessage"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AuthMessage(string message)
        {
            Message = message;
        }
        
        /// <summary>
        /// Текст сообщения об успешной или неудачной авторизации.
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.AuthMessage;
    }
}
