using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetValue()
        {
            var items = HttpContext.Items;
            var now = (DateTime)items["Now"];
            return Ok(now.ToString());
        }
    }
}