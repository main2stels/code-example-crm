using FullCRM.Middleware;
using FullCRM.Models.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullCRM.Services
{
    public class InfoWebSocketService
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public InfoWebSocketService()
        {
            _sockets = InfoWebSocketMiddleware._sockets;
        }


        public async Task SendMessage(object responseObject, Mode mode)
        {
            foreach (var socket in _sockets)
            {
                if (socket.Value.State != WebSocketState.Open)
                {
                    continue;
                }

                WebSocketResult webSocketResult = new WebSocketResult
                {
                    Mode = mode,
                    Class = responseObject.GetType().Name,
                    Data = responseObject
                };

                var result = JsonConvert.SerializeObject(webSocketResult);
                await SendStringAsync(socket.Value, result);
            }
        }

        private Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }
    }
}
