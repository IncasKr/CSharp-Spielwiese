using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using vtortola.WebSockets;
using vtortola.WebSockets.Deflate;
using vtortola.WebSockets.Rfc6455;

namespace WebSocketListenerServer
{
    public delegate void WebSocketEventListenerOnConnect(WebSocket webSocket);
    public delegate void WebSocketEventListenerOnDisconnect(WebSocket webSocket);
    public delegate void WebSocketEventListenerOnMessage(WebSocket webSocket, String message);
    public delegate void WebSocketEventListenerOnError(WebSocket webSocket, Exception error);
    public delegate void WebSocketEventListenerOnData(WebSocket webSocket, byte[] data);

    public class WebSocketEventListener : IDisposable
    {
        public event WebSocketEventListenerOnConnect OnConnect;
        public event WebSocketEventListenerOnDisconnect OnDisconnect;
        public event WebSocketEventListenerOnMessage OnMessage;
        public event WebSocketEventListenerOnError OnError;
        public event WebSocketEventListenerOnData OnData;

        readonly WebSocketListener _listener;

        /// <summary>
        /// Constructor with only an endpoint.
        /// </summary>
        /// <param name="endpoint">Represents a network endpoint as an IP address and a port number.</param>
        public WebSocketEventListener(IPEndPoint endpoint) : this(endpoint, new WebSocketListenerOptions())
        {   }

        /// <summary>
        /// Constructor with an endpoint and a certificate.
        /// </summary>
        /// <param name="endpoint">Represents a network endpoint as an IP address and a port number.</param>
        /// <param name="certificate">Represents an X.509 certificate. </param>
        public WebSocketEventListener(IPEndPoint endpoint, X509Certificate2 certificate) : this(endpoint, new WebSocketListenerOptions(), certificate)
        { }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="endpoint">Represents a network endpoint as an IP address and a port number.</param>
        /// <param name="options">Represents several options of WebSocketListener</param>
        /// <param name="certificate">Represents an X.509 certificate.</param>
        public WebSocketEventListener(IPEndPoint endpoint, WebSocketListenerOptions options, X509Certificate2 certificate = null)
        {
            _listener = new WebSocketListener(endpoint, options);
            var rfc6455 = new WebSocketFactoryRfc6455(_listener);
            rfc6455.MessageExtensions.RegisterExtension(new WebSocketDeflateExtension());
            _listener.Standards.RegisterStandard(rfc6455);
            if(certificate != null)
                _listener.ConnectionExtensions.RegisterExtension(new WebSocketSecureConnectionExtension(certificate));
        }

        /// <summary>
        /// Start to listen a port
        /// </summary>
        public void Start()
        {
            _listener.Start();
            Task.Run((Func<Task>)ListenAsync);
        }

        /// <summary>
        /// Stop to listen a port
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
        }

        /// <summary>
        /// accept the connection until the server is activated
        /// </summary>
        /// <returns></returns>
        private async Task ListenAsync()
        {
            while (_listener.IsStarted)
            {
                try
                {
                    var websocket = await _listener.AcceptWebSocketAsync(CancellationToken.None)
                                                   .ConfigureAwait(false);
                    if (websocket != null)
                        /*await*/ Task.Run(() => HandleWebSocketAsync(websocket));
                }
                catch (Exception ex)
                {
                    if (OnError != null)
                        OnError.Invoke(null, ex);
                }
            }
        }

        /// <summary>
        /// Establishes the connection received and starts the client/server communication.
        /// </summary>
        /// <param name="websocket">Represents the client's websocket.</param>
        /// <returns></returns>
        private async Task HandleWebSocketAsync(WebSocket websocket)
        {
            try
            {
                if (OnConnect != null)
                    OnConnect.Invoke(websocket);

                while (websocket.IsConnected)
                {
                    var message = await websocket.ReadStringAsync(CancellationToken.None)
                                                 .ConfigureAwait(false);
                    if (message != null && OnMessage != null)
                        OnMessage.Invoke(websocket, message);
                }

                if (OnDisconnect != null)
                    OnDisconnect.Invoke(websocket);
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError.Invoke(websocket, ex);
            }
            finally
            {
                websocket.Dispose();
            }
        }

        /// <summary>
        /// Destroyed the server instance
        /// </summary>
        public void Dispose()
        {
            _listener.Dispose();
        }
    }
}
