# WebSocketServer
Формат данных:
```TypeScript
interface WebSocketMessage<T> {
    Type: string;
    Payload: T;
}
```
Список доступных Payload:
1. Авторизация (AUTH_MESSAGE)
```TypeScript
interface AuthMessage {
    Message?: string;
    Status?: AuthStatusCode;
    UserName: string;
}
enum AuthStatusCode { ERROR, SUCCESS }
```
2. Сообщение для чата (CHAT_MESSAGE)
```TypeScript
interface ChatMessage {
    Message: string;
    Time?: string;
    UserName?: string;
}
```