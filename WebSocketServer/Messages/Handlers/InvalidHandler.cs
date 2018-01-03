﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the InvalidHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    /// <inheritdoc />
    public class InvalidHandler : IHandler
    {
        /// <inheritdoc />
        public string Target => string.Empty;

        /// <inheritdoc />
        public void Handle(Fleck2.Interfaces.IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            var usersListMessage = new Payloads.UsersListMessage();
            socket.Send(Helper.BuildMessage(usersListMessage));
        }
    }
}