// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersListHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the UsersListHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    /// <inheritdoc />
    public class UsersListHandler : IHandler
    {
        /// <inheritdoc />
        public string Target => Payloads.UsersListMessage.Type;

        /// <inheritdoc />
        public void Handle(Fleck2.Interfaces.IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            var usersListMessage = new Payloads.UsersListMessage();
            socket.Send(Helper.BuildMessage(usersListMessage));
        }
    }
}
