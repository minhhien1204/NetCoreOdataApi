using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using NetCoreOdataApi.Core.UnitOfWork;
using NetCoreOdataApi.Domain;
using NetCoreOdataApi.Services;

namespace NetCoreOdataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public QuestionsController(IQuestionService questionService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _questionService = questionService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpGet, EnableQuery]
        public async Task<IQueryable<QuestionViewModel>> Get()
        {
            return await _questionService.GetAllQuestionsAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post(QuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var question = await _questionService.InsertAsync(model);
                _unitOfWorkAsync.Commit();
                return Created("Created new question", question);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(Guid key, QuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var participant = await _questionService.UpdateAsync(key, model);
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
