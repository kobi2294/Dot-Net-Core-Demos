using System.Collections.Generic;
using System.Threading.Tasks;
using SignalRServer.Web.Models;

namespace SignalRServer.Web.Services
{
    public interface IChartDataService
    {
        int ChartModels { get; set; }

        Task<List<ChartModel>> GenerateData();
    }
}