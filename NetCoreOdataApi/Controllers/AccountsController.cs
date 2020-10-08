using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreOdataApi.Core;
using NetCoreOdataApi.Data;
using NetCoreOdataApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreOdataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public AccountsController(IDataContext dataContext)
        {
            _dataContext = dataContext as DataContext;
        }

        // POST api/<AccountsController>
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            var a = model.UserName.Trim();
            bool result = _dataContext.Accounts.Any(x=>(x.UserName == model.UserName.Trim() && x.Password.Trim() == model.Password.Trim()));
            return Ok(new
            {
                result = result,
                StatusCode = StatusCode(200)
            });
        }
        
    }
}
