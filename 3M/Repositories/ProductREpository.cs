using _3M.DataModels;
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
    public class ProductRepository : IRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> GetPagesCountAsync(string categoryKey, string searchText, int itemPerPage)
        {
            if (itemPerPage < 1)
                itemPerPage = 1;
            var query = _dbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(categoryKey))
                query = query.Where(i => i.Category.Key == categoryKey);
            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(i => i.Name.Contains(searchText) || i.Key.Contains(searchText));
            var pages = (int)Math.Ceiling((double)(await query.CountAsync()) / itemPerPage);
            return pages;
        }

        public async Task<IList<ProductDto>> FindAsync(string categoryKey, string searchText, int page, int itemPerPage)
        {
            if (itemPerPage < 1) itemPerPage = 1;
            if (page < 1) page = 1;
            var query = _dbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(categoryKey))
                query = query.Where(i => i.Category.Key == categoryKey);
            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(i => i.Name.Contains(searchText) || i.Key.Contains(searchText));
            query = query.Skip((page - 1) * itemPerPage).Take(itemPerPage);
            var result = await query.Select(i => _mapper.Map<Product, ProductDto>(i))
                .ToListAsync();
            return result;
        }

        public async Task<ProductDto> GetByKey(string productKey)
        {
            var product = await _dbContext.Products
                .Include(i => i.Properties)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Key == productKey);
            ProductDto result = null;
            if (product != null)
                result = _mapper.Map<ProductDto>(product);
            return result;
        }

        public async Task<ProductDto> GetById(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(i => i.Properties)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == productId);
            ProductDto result = null;
            if (product != null)
                result = _mapper.Map<ProductDto>(product);
            return result;
        }
    }
}
