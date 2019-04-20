using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Models.WebSocket
{
    public class WebSocketResult
    {
        public Mode Mode { get; set; }

        public string Class { get; set; }

        public object Data { get; set; }
    }
}
