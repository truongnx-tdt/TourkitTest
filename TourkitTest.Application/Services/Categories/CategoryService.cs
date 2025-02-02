// "/*
// * -----------------------------------------------------------------------------
// * File name: CategoryService.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Index.HPRtree;
using TourkitTest.Data.DTO;
using TourkitTest.Data.EF.Uow;
using TourkitTest.Data.Entities;
using TourkitTest.Data.Response;
using TourkitTest.Manufacturer.CommonConstant;
using TourkitTest.Manufacturer.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TourkitTest.Application.Services.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<object>> AddCategory(CategoryCDTO data)
        {
            var createStrategy = _unitOfWork.CreateExecutionStrategy();
            var result = new Response<object>(false, StringConst.Exeption);
            await createStrategy.Execute(async () =>
            {
                using (var db = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var newCtg = new Category
                        {
                            Name = data.Name,
                            DateAdd = data.DateAdd.UtcDateTime
                        };

                        var hasName = await _unitOfWork.CategoryRepository.FindAsync(x => x.Name.ToLower().Equals(data.Name.ToLower()));
                        // check product name exists
                        if (hasName != null && !string.IsNullOrEmpty(hasName.Name))
                        {
                            result.Result = StringConst.CtgExists;
                            return;
                        }
                        else
                        {
                            await _unitOfWork.CategoryRepository.AddAsyn(newCtg);
                            await db.CommitAsync();
                            result.Success = true;
                            result.Result = StringConst.CtgAddDone;
                        }
                    }
                    catch (Exception ex)
                    {
                        await db.RollbackAsync();
                        _logger.LogError(ex, StringConst.Exeption);
                    }
                }
            });
            return result;
        }

        public async Task<Response<object>> DeleteCategorys(List<Guid> ids)
        {
            var result = new Response<object>(false, StringConst.Exeption);
            var createStrategy = _unitOfWork.CreateExecutionStrategy();
            await createStrategy.Execute(async () =>
            {
                using (var db = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var categories = await _unitOfWork.CategoryRepository.FindAllAsync(x => ids.Contains(x.Id));
                        var productCategories = await _unitOfWork.ProductCategoryRepository.FindAllAsync(x => ids.Contains(x.CategoryId));

                        if (productCategories.Any())
                        {
                            result.Result = StringConst.CtgExistsProduct;
                            return;
                        }
                        else
                        {
                            await _unitOfWork.CategoryRepository.DeleteListAsync(categories.ToList());
                            await db.CommitAsync();
                            result.Success = true;
                            result.Result = StringConst.CtgDelete;
                        }
                    }
                    catch (Exception ex)
                    {
                        await db.RollbackAsync();
                        _logger.LogError(ex.ToString());
                    }
                }
            });
            return result;
        }

        public async Task<CategoriesResponse> GetAllCategories()
        {
            try
            {
                var ctg = await _unitOfWork.CategoryRepository.GetCtgAsNoTracking();
                var rs = ctg.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateAdd = x.DateAdd,
                    ProductCount = x.ProductCategories != null ? x.ProductCategories.Count : 0
                });
                return new CategoriesResponse
                {
                    data = rs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), StringConst.Exeption);
                throw;
            }
        }

        public async Task<CategoryDTO> GetCategoryId(Guid id)
        {
            try
            {
                var products = await _unitOfWork.CategoryRepository.GetById(id);
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), StringConst.Exeption);
                throw;
            }
        }

        public async Task<Response<object>> UpdateCategory(CategoryUDTO data)
        {
            var createStrategy = _unitOfWork.CreateExecutionStrategy();
            var result = new Response<object>(false, StringConst.Exeption);
            await createStrategy.Execute(async () =>
            {
                using (var db = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var ctg = await _unitOfWork.CategoryRepository.FindAsync(x => x.Id.Equals(data.Id));
                        if (ctg == null)
                        {
                            result.Result = StringConst.NotFound;
                            return;
                        }
                        var ctgHasName = await _unitOfWork.CategoryRepository.FindByAsyn(x => x.Name == data.Name && !x.Id.Equals(data.Id));
                        if (ctgHasName.Any())
                        {
                            result.Result = StringConst.CtgExists;
                        }
                        else 
                        {
                            ctg.Name = data.Name;
                            ctg.DateAdd = data.DateAdd.UtcDateTime;
                            await _unitOfWork.CategoryRepository.UpdateAsyn(ctg, data.Id);
                            await db.CommitAsync();
                            result.Success = true;
                            result.Result = StringConst.CtgUpdateDone;
                        }
                    }
                    catch (Exception ex)
                    {
                        await db.RollbackAsync();
                        _logger.LogError(ex, StringConst.Exeption);
                    }
                }
            });
            return result;
        }
    }
}
