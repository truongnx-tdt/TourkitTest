// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductDTO.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using System.ComponentModel.DataAnnotations;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTimeOffset DateAdd { get; set; }
        public IEnumerable<Category>? ProductCategories { get; set; } 
    }

    public class ProductCDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        [Required]
        public DateTimeOffset DateAdd { get; set; }
        public List<Guid> Categories { get; set; } = null!;
    }

    public class ProductUDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTimeOffset DateAdd { get; set; }
        public List<Guid> Categories { get; set; } = null!;
    }
}
