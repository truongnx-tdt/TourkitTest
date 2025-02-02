// "/*
// * -----------------------------------------------------------------------------
// * File name: CategoryDTO.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Data.Entities;

namespace TourkitTest.Data.DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public DateTimeOffset DateAdd { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class CategoryCDTO
    {
        public string Name { get; set; }
        public DateTimeOffset DateAdd { get; set; }
    }
    public class CategoryUDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateAdd { get; set; }
    }
}
