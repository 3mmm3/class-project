using _3M.DataModels.Sales;
using _3M.DbContexts;
using _3M.Dtos.Sales;
using _3M.Repositories.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Repositories
{
    public class BillRepository : IRepository
    {
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public BillRepository(AppDbContext dbContext, IMapper mapper,
            ProductRepository productRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task AddBill(BillDto billDto)
        {
            var Bill = new Bill()
            {
                Id = billDto.Id,
                Tel = billDto.Tel,
                TotalPrice = billDto.ShoppingCart.Any()
                    ? billDto.ShoppingCart.Sum(i => i.ProductPrice * i.Count)
                    : 0M,
                Province = billDto.Province,
                City = billDto.City,
                Address = billDto.Address,
                CustomerId = billDto.CustomerId,
                CustomerName = billDto.CustomerName,
                RecipientName = billDto.RecipientName,
                PostalCode = billDto.PostalCode,
                ShoppingCart = billDto.ShoppingCart.Select(i => new ShoppingItem()
                {
                    Count = i.Count,
                    ProductPrice = i.ProductPrice,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Id = i.Id
                }).ToList(),
                IsProcessed = billDto.IsProcessed,
                IsPaid = billDto.IsPaid,
                RegisterDate = billDto.RegisterDate,
            };
            _dbContext.Bills.Add(Bill);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetInvoicePaidState(Guid invoiceId, bool isPaid)
        {
            var current = await _dbContext.Bills.FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (current == null)
                return;
            current.IsPaid = isPaid;
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetInvoiceProcessedState(Guid invoiceId, bool isProcessed)
        {
            var current = await _dbContext.Bills.FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (current == null)
                return;
            current.IsProcessed = isProcessed;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BillDto>> GetUserInvoices(Guid userId, int page, int itemsPerPage)
        {
            if (itemsPerPage < 1) itemsPerPage = 1;
            if (page < 1) page = 1;
            var invoices = await _dbContext.Bills
                .Include(i => i.ShoppingCart)
                .Where(i => i.CustomerId == userId)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .OrderByDescending(i => i.RegisterDate)
                .ToListAsync();
            return invoices.Select(i => _mapper.Map<BillDto>(i)).ToList();
        }

        public async Task<int> GetPagesCountAsync(int itemsPerPage)
        {
            if (itemsPerPage < 1) itemsPerPage = 1;

            var categories = await _dbContext.Bills.CountAsync();
            return (int)Math.Ceiling((double)categories / itemsPerPage);
        }

        public async Task<List<BillDto>> GetAllInvoices(int page, int itemsPerPage)
        {
            if (itemsPerPage < 1) itemsPerPage = 1;
            if (page < 1) page = 1;
            var invoices = await _dbContext.Bills
                .Include(i => i.ShoppingCart)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .OrderByDescending(i => i.RegisterDate)
                .ToListAsync();
            return invoices.Select(i => _mapper.Map<BillDto>(i)).ToList();
        }

        public async Task<BillDto> GetInvoiceById(Guid invoiceId)
        {
            var current = await _dbContext.Bills
                .Include(i => i.ShoppingCart)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (current == null)
                return null;
            return _mapper.Map<BillDto>(current);
        }

        public async Task DeleteAsync(Guid id)
        {
            var current = await _dbContext.Bills
                .Include(i => i.ShoppingCart)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (current == null)
                return;
            _dbContext.ShoppingItems.RemoveRange(current.ShoppingCart);
            _dbContext.Bills.Remove(current);
            await _dbContext.SaveChangesAsync();
        }
    }
}
