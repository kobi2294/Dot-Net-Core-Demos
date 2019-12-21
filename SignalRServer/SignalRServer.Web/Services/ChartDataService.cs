using SignalRServer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Web.Services
{
    public class ChartDataService : IChartDataService
    {
        public int ChartModels { get; set; } = 4;

        private Random _rand = new Random();

        private ChartModel _generateModel(int i)
        {
            return new ChartModel
            {
                Data = new List<int> { _rand.Next(1, 40) },
                Label = "Data" + i
            };
        }

        private IEnumerable<ChartModel> _generateModels()
        {
            for (int i = 0; i < ChartModels; i++)
            {
                yield return _generateModel(i);
            }
        }

        public Task<List<ChartModel>> GenerateData()
        {
            var res = _generateModels().ToList();
            return Task.FromResult(res);
        }
    }
}
