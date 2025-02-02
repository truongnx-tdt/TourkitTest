// "/*
// * -----------------------------------------------------------------------------
// * File name: IUnitOfWork.cs
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
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IExecutionStrategy CreateExecutionStrategy();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> SaveAsync();
    }
}
