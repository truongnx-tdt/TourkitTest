// "/*
// * -----------------------------------------------------------------------------
// * File name: Category.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"


using TourkitTest.Data.Abstractions;

namespace TourkitTest.Data.Entities
{
    public class Category : EntityAuditBase<Guid>
    {
        public string Name { get; set; } = null!;
        public DateTimeOffset DateAdd { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
