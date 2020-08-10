using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WepApi.Hubs
{
    public class VotingHub : Hub
    {
        public async Task RefreshDashboardForAllAsync() 
        {
            await Clients.All.SendAsync("RefreshDashboardData");
        }
    }
}
