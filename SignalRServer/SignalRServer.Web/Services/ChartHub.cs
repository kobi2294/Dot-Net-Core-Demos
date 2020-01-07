using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Web.Services
{
    public class ChartHub: Hub
    {
        private IChartDataService _dataService;

        public ChartHub(IChartDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task RandomizeAll()
        {
            var res = await _dataService.GenerateData();
            await this.Clients.All.SendAsync("chart-data", res);
        }

        public async Task RandomizeMe()
        {
            var res = await _dataService.GenerateData();
            await this.Clients.Caller.SendAsync("chart-data", res);
        }

    }
}
