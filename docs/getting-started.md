# Picking an API

Music Assistant presents two APIs, which access (nearly) all the same data, with all the same method calls.

The RPC-like API is a standard HTTP request/response API, which is ideal for one off calls, or when connecting from outside the network.

The WebSocket API is a bi-directional, real time API which adds in events pushed by the server. ***It is the recommended (by Music Assistant) API.***

Both APIs use the same underlying credentials, so they can be mixed and matched where approapriate. For example, you could use the WebSocket API for your main application, and then use the RPC API for a one off call from a script.