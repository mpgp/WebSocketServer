namespace WebSocketServer
{
    public abstract class BaseMessage
    {
        public override string ToString() { return MessageType; }
        protected abstract string MessageType { get; }
    }
}
