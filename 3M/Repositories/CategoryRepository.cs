using _3M.DataModels.Products;
using _3M.DbContexts;
using _3M.Dtos.Products;
using _3M.Repositories.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Repositories
{
    public class CategoryRepository : IRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> GetPagesCountAsync(int itemsPerPage)
        {
            if (itemsPerPage < 1) itemsPerPage = 1;

            var categories = await _dbContext.Categories.CountAsync();
            return (int)Math.Ceiling((double)categories / itemsPerPage);
        }
        public async Task<IList<CategoryDto>> GetAllAsync(int page, int itemsPerPage)
        {
            if (itemsPerPage < 1) itemsPerPage = 1;
            if (page < 1) page = 1;

            var categories = await _dbContext.Categories
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(i => _mapper.Map<Category, CategoryDto>(i))
                .ToListAsync();
            return categories;
        }
        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (category == null)
                return null;
            return _mapper.Map<Category, CategoryDto>(category);
        }
        public async Task<CategoryDto> GetByKeyAsync(string key)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(i => i.Key == key);
            if (category == null)
                return null;
            return _mapper.Map<Category, CategoryDto>(category);
        }

        public async Task UpdateAsync(CategoryDto category)
        {
            if (category == null)
                return;
            var current = await _dbContext.Categories.FirstOrDefaultAsync(i => i.Id == category.Id);
            if (current == null)
                return;
            current.Key = category.Key;
            current.Name = category.Name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var current = await _dbContext.Categories
                .FirstOrDefaultAsync(i => i.Id == id);
            if (current == null)
                return;
            if (_dbContext.Products.Any(i => i.Category == current))
                throw new Exception("این دسته بندی در یک یا چند محصول استفاده شده است. لطفا ابتدا محصولات این دسته بندی را حذف کنید.");
            _dbContext.Categories.Remove(current);
            await _dbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(CategoryDto category)
        {
            _dbContext.Categories.Add(new Category()
            {
                Id = Guid.NewGuid(),
                Key = category.Key,
                Name = category.Name
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
