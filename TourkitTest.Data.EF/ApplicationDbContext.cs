// "/*
// * -----------------------------------------------------------------------------
// * File name: ApplicationDbContext.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.EF
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context, ILogger<ApplicationDbContext> logger) : base(context)
        {
            _logger = logger;
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ProductCategory>()
           .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Gán giá trị tự động cho các trường audit fields
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Product product)
                {
                    if (entry.State == EntityState.Added)
                    {
                        product.Id = Guid.NewGuid();
                        product.CreatedAt = DateTimeOffset.UtcNow;
                        product.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        product.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                }
                else if (entry.Entity is Category category)
                {
                    if (entry.State == EntityState.Added)
                    {
                        category.Id = Guid.NewGuid();
                        category.CreatedAt = DateTimeOffset.UtcNow;
                        category.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        category.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                }
            }
            return base.SaveChanges();
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Product product)
                {
                    if (entry.State == EntityState.Added)
                    {
                        product.Id = Guid.NewGuid();
                        product.CreatedAt = DateTimeOffset.UtcNow;
                        product.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        product.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                }
                else if (entry.Entity is Category category)
                {
                    if (entry.State == EntityState.Added)
                    {
                        category.Id = Guid.NewGuid();
                        category.CreatedAt = DateTimeOffset.UtcNow;
                        category.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        category.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
