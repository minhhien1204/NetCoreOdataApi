using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using NetCoreOdataApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreOdataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet, EnableQuery]
        public IEnumerable<Students> Get()
        {
            return new List<Students>
            {
                new Students
                {
                    Id = Guid.NewGuid(),
                    Name = "Vishwa Goli",
                    Score = 4
                },
                new Students
                {
                    Id = Guid.NewGuid(),
                    Name = "Josh McCall",
                    Score = 6
                },
                 new Students
                {
                    Id = Guid.NewGuid(),
                    Name = "Hien Minh",
                    Score = 10
                }
            };
        }
     
    }
}
