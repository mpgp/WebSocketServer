// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the DialogHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <inheritdoc />
    public class DialogHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => Payloads.Types.DialogMessage;

        /// <inheritdoc />
        public override void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            try
            {
                var data =
                    Newtonsoft.Json.JsonConvert
                        .DeserializeObject<WebSocketMessage<Payloads.Client.DialogMessage>>(webSocketMessage);
                var dialogMessage = new Payloads.Server.DialogMessage()
                {
                    Login = server.ConnectedSockets[socket].Login,
                    Message = data.Payload.Message,
                    Receiver = data.Payload.Receiver
                };
                server.SendToUser(data.Payload.Receiver, dialogMessage);
                
                if (dialogMessage.Login != dialogMessage.Receiver)
                {
                    socket.Send(Helper.BuildMessage(dialogMessage));
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
    }
}
