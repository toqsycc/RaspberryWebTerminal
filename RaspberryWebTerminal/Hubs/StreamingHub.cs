using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RaspberryWebTerminal.Hubs
{
    public class StreamingHub : Hub
    {
        public async Task SendFrame()
        {
            await Clients.All.SendAsync("NewFrameReceived");
        }
    }
}