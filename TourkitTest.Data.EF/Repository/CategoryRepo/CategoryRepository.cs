// "/*
// * -----------------------------------------------------------------------------
// * File name: CategoryRepository.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.EF.Repository.CategoryRepo
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert list category
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task InsertListAsync(List<Category> data)
        {
            await _context.BulkInsertAsync(data).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete list category
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task DeleteListAsync(List<Category> data)
        {
            await _context.BulkDeleteAsync(data).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Category>> GetCtgAsNoTracking()
        {
            return await _context.Categories.AsNoTracking()
                .OrderBy(x => x.Name)
                .Include(x => x.ProductCategories)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<CategoryDTO> GetById(Guid id)
        {
            return await _context.Categories.AsNoTracking()
                .Include(x => x.ProductCategories)
                .Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateAdd = x.DateAdd,
                    ProductCount = x.ProductCategories != null ? x.ProductCategories.Count : 0,
                    Products = x.ProductCategories.Select(pc => new Product
                    {
                        Id = pc.Product.Id,
                        Name = pc.Product.Name,
                        Price = pc.Product.Price,
                        DateAdd = pc.Product.DateAdd
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }
    }
}
