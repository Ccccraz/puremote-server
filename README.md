## Puremote-server

This is a middlware server for puremote, which is used to handle data forwarding from other frameworks to puremote, solving the integration problem between puremote and other frameworks, such as the integration of PTB and puremote.

## Architecture
```mermaid
sequenceDiagram
    PTB-->>Puremote-server: Start puremote-server
    Puremote-server-->>PTB: Started
    Puremote-->>Puremote-server: SSE Get request
    PTB->>Puremote-server: Post request
    Puremote-server->>Puremote: Responding to get request
    PTB->>Puremote-server: Post request
    Puremote-server->>Puremote: Responding to get request
    PTB-->>Puremote-server: Stop puremote-server
    Puremote-server-->>Puremote: Stop
```