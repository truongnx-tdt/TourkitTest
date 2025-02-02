// "/*
// * -----------------------------------------------------------------------------
// * File name: IProductRepository.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using Microsoft.EntityFrameworkCore;
using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.EF.Repository.ProductRepo
{
    public interface IProductRepository : IGenericRepository<Entities.Product>
    {
        Task<int> InsertListAsync(List<Product> data);
        Task<IEnumerable<ProductDTO>> GetAllProductsWithCategoriesAsync(DataTableRequest request);
        Task<ProductDTO> GetByidAsync(Guid id);
    }
}
