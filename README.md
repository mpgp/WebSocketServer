# WebSocketServer
## Table of Contents
- [Startup](#startup)
- [Data format](#data-format)
- [List of Payload](#list-of-payload)
  - [AUTH_MESSAGE](#auth_message)
  - [CHAT_MESSAGE](#chat_message)
  - [USERS_LIST_MESSAGE](#users_list_message)

## Startup
```
WebSocketServer.exe <hostname> <port> <protocol>
```
```
WebSocketServer.exe localhost 8181 ws
```

## Data format
```TypeScript
interface WebSocketMessage<T> {
    Type: string;
    Payload?: T;
}
```
## List of Payload:
### AUTH_MESSAGE
```TypeScript
interface AuthMessage {
    Message?: string;
    Status?: AuthStatusCode;
    UserName: string;
}
enum AuthStatusCode { ERROR, SUCCESS }
```
```JavaScript
// client -> server
ws.send(JSON.stringify({
	Type: "AUTH_MESSAGE",
	Payload: {
		UserName: "admin2018"
	}
}));
// [success] server -> client
{
	Type: "AUTH_MESSAGE",
	Payload: {
		Message: null,
		Status: 1,
		UserName: "admin2018"
	}
}
// [error] server -> client
{
	Type: "AUTH_MESSAGE",
	Payload: {
		Message: "Error: the user name <admin2018> is already in use!",
		Status: 0,
		UserName: "admin2018"
	}
}
```
### CHAT_MESSAGE
```TypeScript
interface ChatMessage {
    Message: string;
    Time?: string;
    UserName?: string;
}
```
```JavaScript
// client -> server
ws.send(JSON.stringify({
	Type: "CHAT_MESSAGE",
	Payload: {
		Message: "Hello World"
	}
}));
// server -> client
{
	Type: "CHAT_MESSAGE",
	Payload: {
		Message: "Hello World",
		Time: 1515144030,
		UserName: "admin2018"
	}
}
```
### USERS_LIST_MESSAGE
```TypeScript
interface UsersListMessage {
    UsersList?: string[];
}
```
```JavaScript
// client -> server
ws.send(JSON.stringify({
	Type: "USERS_LIST_MESSAGE"
}));
// server -> client
{
	Type: "USERS_LIST_MESSAGE",
	Payload: {
		["admin2018", "randomuser", "qwerty..."]
	}
}
```