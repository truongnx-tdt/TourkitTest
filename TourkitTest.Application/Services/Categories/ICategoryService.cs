// "/*
// * -----------------------------------------------------------------------------
// * File name: ICategoryService.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;
using TourkitTest.Data.Response;

namespace TourkitTest.Application.Services.Categories
{
    public interface ICategoryService : IBaseService
    {
        Task<CategoriesResponse> GetAllCategories();
        Task<Response<object>> DeleteCategorys(List<Guid> ids);
        Task<Response<object>> UpdateCategory(CategoryUDTO Category);
        Task<Response<object>> AddCategory(CategoryCDTO Category);
        Task<CategoryDTO> GetCategoryId(Guid id);
    }
}
