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
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <inheritdoc />
    public class UsersListHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => Payloads.Types.UsersListMessage;

        /// <inheritdoc />
        public override void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            var usersListMessage = new Payloads.Server.UsersListMessage()
            {
                UsersList = server.ConnectedSockets.Values.Select(user => user.Login).ToArray()
            };
            socket.Send(Helper.BuildMessage(usersListMessage));
        }
    }
}
