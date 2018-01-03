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
    using System.Linq;

    /// <inheritdoc />
    public class UsersListHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => Payloads.UsersListMessage.Type;

        /// <inheritdoc />
        public override void Handle(Fleck2.Interfaces.IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            var usersListMessage = new Payloads.UsersListMessage() { UsersList = server.ConnectedSockets.Values.ToArray() };
            socket.Send(Helper.BuildMessage(usersListMessage));
        }
    }
}
