using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionSample.Models;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleRepositoryService _peopleService;

        public PeopleController(IPeopleRepositoryService peopleService)
        {
            _peopleService = peopleService;
        }


        [HttpGet]
        public async Task<ActionResult<Person[]>> GetAll([FromServices] IMetadataService metadata)
        {
            var data = await _peopleService.GetAllPeople();
            var peopleCount = metadata.PeopleCount;
            return Ok(data);
        }
    }
}


