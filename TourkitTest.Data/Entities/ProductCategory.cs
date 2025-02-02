// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductCategory.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourkitTest.Data.Entities
{
    public class ProductCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
