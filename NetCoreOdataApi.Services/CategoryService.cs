using NetCoreOdataApi.Core.Models;
using NetCoreOdataApi.Core.Repositories;
using NetCoreOdataApi.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using static NetCoreOdataApi.Services.CategoryService;

namespace NetCoreOdataApi.Services
{
    public interface ICategoryService : IService<Category>
    {
        IQueryable<CategoryViewModel> GetAllCategories();
        Category Insert(CategoryViewModel model);
        Task<Category> InsertAsync(CategoryViewModel model);
        Task<IQueryable<CategoryViewModel>> GetAllCategoriesAsync();
    }
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IRepositoryAsync<Category> repository) : base(repository)
        {
     
        }
        public Task<IQueryable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return Task.Run(() => GetAllCategories());
        }
        public IQueryable<CategoryViewModel> GetAllCategories()
        {
            return _repository.Queryable().Where(x => x.Delete == false)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreateDate = x.CreateDate,
                    Delete = x.Delete,
                    Description = x.Description,
                    LastModifiedDate = x.LastModifiedDate,
                    Products = x.Products.Select(p => new ProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Color = p.Color
                    }).ToList()
                });
            
        }
        public Category Insert(CategoryViewModel model)
        {
            Category newCate = new Category()
            {
                Name = model.Name,
                Description = model.Description,
                Delete = false,
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };
            base.Insert(newCate);
            return newCate;
        }

        public Task<Category> InsertAsync(CategoryViewModel model)
        {
            return Task.Run(() => Insert(model));
        }
    }
}
