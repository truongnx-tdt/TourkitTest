// "/*
// * -----------------------------------------------------------------------------
// * File name: DataTableRequest.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

namespace TourkitTest.Data.DTO
{
    public class DataTableRequest
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public string? Search { get; set; }
        public List<Guid>? CategoryId { get; set; }
        public int Draw { get; set; }
    }
}
