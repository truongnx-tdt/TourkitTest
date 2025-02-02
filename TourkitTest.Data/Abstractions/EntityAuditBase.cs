// "/*
// * -----------------------------------------------------------------------------
// * File name: EntityAuditBase.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"


namespace TourkitTest.Data.Abstractions
{
    public class EntityAuditBase<T> : EntityBase<T>, Interfaces.IAuditBase
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
