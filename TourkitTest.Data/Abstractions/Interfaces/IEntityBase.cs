// "/*
// * -----------------------------------------------------------------------------
// * File name: IEntityBase.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"


namespace TourkitTest.Data.Abstractions.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
