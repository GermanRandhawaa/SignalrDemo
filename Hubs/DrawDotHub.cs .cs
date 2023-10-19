using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalrChat.Hubs;

public class DrawDotHub : Hub
{

   private static Dictionary<string, string> userColors = new Dictionary<string, string>();

   public async Task UpdateCanvas(int x, int y)
{
    string userColor = userColors[Context.ConnectionId];
    await Clients.All.SendAsync("updateDot", x, y, userColor);
}

   public override Task OnConnectedAsync()
   {
      string connectionId = Context.ConnectionId;
      string userColor = GenerateRandomColor();
      userColors[connectionId] = userColor;
      return base.OnConnectedAsync();
   }

   private string GenerateRandomColor()
   {
      Random random = new Random();
      string color = $"#{random.Next(0x1000000):X6}"; // Generate a random color code
      return color;
   }

   public async Task ClearCanvas()
   {
      await Clients.All.SendAsync("clearCanvas");
   }
}

