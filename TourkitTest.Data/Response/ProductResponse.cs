// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductResponse.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;

namespace TourkitTest.Data.Response
{
    public class ProductResponse
    {
        public int Draw { get; set; }
        public IEnumerable<ProductDTO> Data { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
    }
}
