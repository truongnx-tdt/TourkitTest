// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductCategoryRepository.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

namespace TourkitTest.Data.EF.Repository.ProdcutCategoryRepo
{
    public class ProductCategoryRepository : GenericRepository<Entities.ProductCategory>, IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
