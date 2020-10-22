using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreOdataApi.Core.UnitOfWork;
using NetCoreOdataApi.Domain;
using NetCoreOdataApi.Services;
using static NetCoreOdataApi.Services.CategoryService;

namespace NetCoreOdataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public CategoriesController(ICategoryService categoryService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _categoryService = categoryService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize]
        public async Task<IQueryable<CategoryViewModel>> Get()
        {
            return await _categoryService.GetAllCategoriesAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var stf = await _categoryService.InsertAsync(model);
                _unitOfWorkAsync.Commit();
                var resultObject = new CategoryViewModel()
                {
                    Id = stf.Id,
                    Name = stf.Name,

                };
                return Created("Created new category",resultObject);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
