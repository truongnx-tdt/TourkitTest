// "/*
// * -----------------------------------------------------------------------------
// * File name: UnitOfWork.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using Microsoft.EntityFrameworkCore.Storage;
using TourkitTest.Data.EF.Repository.CategoryRepo;
using TourkitTest.Data.EF.Repository.ProdcutCategoryRepo;
using TourkitTest.Data.EF.Repository.ProductRepo;

namespace TourkitTest.Data.EF.Uow
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context;
        public IProductRepository ProductRepository
        {
            get;
            private set;
        }
        public ICategoryRepository CategoryRepository
        {
            get;
            private set;
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor unit of work
        /// Có thể khai báo các repository khác ở đây
        /// 1. Inject repository qua constructor
        /// 2. lazy initialization 
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            ProductCategoryRepository = new ProductCategoryRepository(_context);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var cm = await _context.SaveChangesAsync().ConfigureAwait(false);
            return cm != 0;
        }

        //Disposing of the Context Object
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }
    }
}
