// "/*
// * -----------------------------------------------------------------------------
// * File name: EntityBase.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Data.Abstractions.Interfaces;

namespace TourkitTest.Data.Abstractions
{
    public class EntityBase<T> : IEntityBase<T>
    {
        public T Id { get; set; }
    }
}
