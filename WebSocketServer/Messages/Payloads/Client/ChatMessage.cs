﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the ChatMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Client
{
    /// <inheritdoc />
    public class ChatMessage : BaseMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.ChatMessage;
    }
}
