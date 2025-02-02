// "/*
// * -----------------------------------------------------------------------------
// * File name: IProductServices.cs
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
using TourkitTest.Data.Response;

namespace TourkitTest.Application.Services.Products
{
    public interface IProductServices : IBaseService
    {
        Task<ProductResponse> GetAllProductsWithCategoriesAsync(DataTableRequest request);
        Task<bool> DeleteProducts(List<Guid> ids);
        Task<Response<object>> UpdateProduct(ProductUDTO product);
        Task<Response<object>> AddProduct(ProductCDTO product);
        Task<ProductDTO> GetProductId(Guid id);
    }
}
