// "/*
// * -----------------------------------------------------------------------------
// * File name: IDateTracking.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

namespace TourkitTest.Data.Abstractions.Interfaces
{
    public interface IDateTracking
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
