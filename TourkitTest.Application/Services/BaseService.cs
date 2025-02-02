// "/*
// * -----------------------------------------------------------------------------
// * File name: BaseService.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Data.EF.Uow;

namespace TourkitTest.Application.Services
{
    public class BaseService : IBaseService
    {
        protected IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Baseservice
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
