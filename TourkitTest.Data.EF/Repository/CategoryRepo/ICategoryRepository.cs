// "/*
// * -----------------------------------------------------------------------------
// * File name: ICategoryRepository.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.EF.Repository.CategoryRepo
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task InsertListAsync(List<Category> data);
        Task DeleteListAsync(List<Category> data);
        Task<IEnumerable<Category>> GetCtgAsNoTracking();
        Task<CategoryDTO> GetById(Guid id);
    }
}
