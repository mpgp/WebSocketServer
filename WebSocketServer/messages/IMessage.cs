namespace WebSocketServer
{
    public abstract class IMessage
    {
        public override string ToString() { return MessageType; }
        protected abstract string MessageType { get; }
    }
}
