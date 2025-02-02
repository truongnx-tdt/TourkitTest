// "/*
// * -----------------------------------------------------------------------------
// * File name: DbInit.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using static System.Runtime.InteropServices.JavaScript.JSType;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.EF.Seed
{
    public class DbInit
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInit> _logger;
        public DbInit(ApplicationDbContext context, ILogger<DbInit> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Seeding data template to db
        /// </summary>
        /// <returns></returns>
        public async Task Seed()
        {
            var excutionStrategy = _context.Database.CreateExecutionStrategy();
            await excutionStrategy.Execute(async () =>
            {
                using (var db = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var categories = Enumerable.Range(1, 20)
                               .Select(i => new Category
                               {
                                   Id = Guid.NewGuid(),
                                   Name = $"Category {i}",
                                   DateAdd = DateTimeOffset.UtcNow,
                                   CreatedAt = DateTimeOffset.UtcNow,
                                   UpdatedAt = DateTimeOffset.UtcNow,
                               }).ToList();
                        if (!_context.Categories.Any())
                        {
                            await _context.BulkInsertAsync(categories).ConfigureAwait(false);
                        }

                        if (!_context.Products.Any())
                        {
                            var random = new Random();
                            var products = Enumerable.Range(1, 10000)
                                .Select(i => new Product
                                {
                                    Id = Guid.NewGuid(),
                                    Name = $"Product {i}",
                                    Price = random.Next(1000, 10000000),
                                    DateAdd = DateTimeOffset.UtcNow,
                                    CreatedAt = DateTimeOffset.UtcNow,
                                    UpdatedAt = DateTimeOffset.UtcNow
                                }).ToList();

                            await _context.BulkInsertAsync(products).ConfigureAwait(false);

                            var productCategories = new List<ProductCategory>();
                            foreach (var product in products)
                            {
                                var randomCategoryIds = categories
                                    .OrderBy(x => random.Next())
                                    .Take(random.Next(1, 5))
                                    .ToList();

                                productCategories.AddRange(randomCategoryIds.Select(catId => new ProductCategory
                                {
                                    ProductId = product.Id,
                                    CategoryId = catId.Id
                                }));
                            }
                            await _context.BulkInsertAsync(productCategories).ConfigureAwait(false);
                        }


                        await db.CommitAsync();
                        _logger.LogInformation("Init Complement to db");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        await db.RollbackAsync();
                    }
                }
            });

        }
    }
}
