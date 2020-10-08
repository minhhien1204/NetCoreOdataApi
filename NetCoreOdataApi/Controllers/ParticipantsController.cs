using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using NetCoreOdataApi.Core.UnitOfWork;
using NetCoreOdataApi.Domain;
using NetCoreOdataApi.Services;
using Microsoft.AspNetCore.Authorization;
namespace NetCoreOdataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
      
        public ParticipantsController(IParticipantService participantService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _participantService = participantService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpGet, EnableQuery]
        public async Task<IQueryable<ParticipantViewModel>> Get()
        {
           
            return await _participantService.GetAllParticipantsAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post(ParticipantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var participant = await _participantService.InsertAsync(model);
                _unitOfWorkAsync.Commit();
                return Created("Created new participant", participant);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(string key, ParticipantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var participant = await _participantService.UpdateAsync(key,model);
                _unitOfWorkAsync.Commit();
                return Ok(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
