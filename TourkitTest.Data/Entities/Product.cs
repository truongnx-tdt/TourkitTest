// "/*
// * -----------------------------------------------------------------------------
// * File name: Product.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using System.ComponentModel.DataAnnotations;
using TourkitTest.Data.Abstractions;

namespace TourkitTest.Data.Entities
{
    public class Product : EntityAuditBase<Guid>
    {
        [Required]
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTimeOffset DateAdd { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
