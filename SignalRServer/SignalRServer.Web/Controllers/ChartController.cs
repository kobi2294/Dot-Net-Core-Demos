using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Web.Models;
using SignalRServer.Web.Services;

namespace SignalRServer.Web.Controllers
{
    [Route("api/chart")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IHubContext<ChartHub> _hub;
        private IChartDataService _service;

        public ChartController(IHubContext<ChartHub> hub, IChartDataService service)
        {
            _hub = hub;
            _service = service;
        }

        [HttpPost("")]
        public async Task<ActionResult> Set([FromBody] ChartModel[] model)
        {
            await _hub.Clients.All.SendAsync("chart-data", model);
            return Ok();
        }

        [HttpGet("")]
        public async Task<ActionResult<ChartModel[]>> Get()
        {
            var res = await _service.GenerateData();
            return Ok(res.ToArray());
            
        }
    }
}