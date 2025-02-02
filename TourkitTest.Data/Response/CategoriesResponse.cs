// "/*
// * -----------------------------------------------------------------------------
// * File name: CategoriesResponse.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Data.DTO;

namespace TourkitTest.Data.Response
{
    public class CategoriesResponse
    {
        public IEnumerable<CategoryDTO> data { get; set; }
    }
}
