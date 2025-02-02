// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductRepository.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using System;
using System.Linq;
using Azure.Core;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;
using TourkitTest.Manufacturer.Utils;

namespace TourkitTest.Data.EF.Repository.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// get product with category
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDTO>> GetAllProductsWithCategoriesAsync(DataTableRequest request)
        {
            var query = _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                string searchLower = request.Search.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(searchLower) ||
                    p.Id.ToString().ToLower().Contains(searchLower)
                );
            }

            if (request.CategoryId != null && request.CategoryId.Any())
            {
                query = query.Where(p =>
                    p.ProductCategories.Any(pc => request.CategoryId.Contains(pc.CategoryId))
                );
            }
            var products = await query
                .OrderBy(p => p.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    DateAdd = p.DateAdd,
                    ProductCategories = p.ProductCategories.Select(pc => new Category
                    {
                        Id = pc.Category.Id,
                        Name = pc.Category.Name,
                        DateAdd = pc.Category.DateAdd
                    }).ToList()
                })
                .ToListAsync();

            return products;
        }

        public async Task<ProductDTO> GetByidAsync(Guid id)
        {
            return await _context.Products.Where(p => p.Id == id)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    DateAdd = p.DateAdd,
                    ProductCategories = p.ProductCategories.Select(pc => new Category
                    {
                        Id = pc.Category.Id,
                        Name = pc.Category.Name,
                        DateAdd = pc.Category.DateAdd
                    })
                })
                .SingleOrDefaultAsync();
        }
        public async Task<int> InsertListAsync(List<Product> data)
        {
            await _context.BulkInsertAsync(data).ConfigureAwait(false);
            return data.Count;
        }
    }
}
