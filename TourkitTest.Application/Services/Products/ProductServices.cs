// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductServices.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using TourkitTest.Data.DTO;
using TourkitTest.Data.EF.Uow;
using TourkitTest.Data.Entities;
using TourkitTest.Data.Response;
using TourkitTest.Manufacturer.CommonConstant;
using TourkitTest.Manufacturer.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TourkitTest.Application.Services.Products
{
    public class ProductServices : BaseService, IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductServices> _logger;
        public ProductServices(IUnitOfWork unitOfWork, ILogger<ProductServices> logger) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        /// <summary>
        /// Create product with roll back transaction
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task<Response<object>> AddProduct(ProductCDTO productDTO)
        {
            var createStrategy = _unitOfWork.CreateExecutionStrategy();
            var result = new Response<object>(false, StringConst.Exeption);
            await createStrategy.Execute(async () =>
            {
                using (var db = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var newProduct = new Product
                        {
                            Name = productDTO.Name,
                            Price = productDTO.Price,
                            DateAdd = productDTO.DateAdd.UtcDateTime
                        };
                        var hasName = await _unitOfWork.ProductRepository.FindAsync(x => x.Name.ToLower().Equals(productDTO.Name.ToLower()));
                        // check product name exists
                        if (hasName != null && !string.IsNullOrEmpty(hasName.Name))
                        {
                            result.Result = StringConst.ProductExists;
                        }
                        else
                        {
                            await _unitOfWork.ProductRepository.AddAsyn(newProduct);
                            var productId = newProduct.Id;

                            foreach (var item in productDTO.Categories)
                            {
                                var productCategory = new ProductCategory
                                {
                                    ProductId = productId,
                                    CategoryId = item
                                };

                                await _unitOfWork.ProductCategoryRepository.AddAsyn(productCategory);
                            }
                            await db.CommitAsync();
                            result.Success = true;
                            result.Result = StringConst.ProductAddDone;
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
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProducts(List<Guid> ids)
        {
            try
            {
                foreach (var item in ids)
                {
                    var product = await _unitOfWork.ProductRepository.FindAsync(x => x.Id == item);
                    if (product != null)
                    {
                        await _unitOfWork.ProductRepository.DeleteAsyn(product);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Get Product in Db with Categories
        /// </summary>
        /// <returns>Infomatin of product</returns>
        public async Task<ProductResponse> GetAllProductsWithCategoriesAsync(DataTableRequest request)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllProductsWithCategoriesAsync(request);
                Expression<Func<Product, bool>> filter = c =>
                    (string.IsNullOrEmpty(request.Search) ||
                        c.Name.ToLower().Contains(request.Search.ToLower()) ||
                        c.Id.ToString().ToLower().Contains(request.Search.ToLower())) &&
                    (request.CategoryId == null || !request.CategoryId.Any() ||
                        c.ProductCategories.Any(f => request.CategoryId.Contains(f.CategoryId)));
                var totalRecords = await _unitOfWork.ProductRepository.CountAsync(filter);
                return new ProductResponse
                {
                    RecordsTotal = totalRecords,
                    RecordsFiltered = totalRecords,
                    Data = products,
                    Draw = request.Draw
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ProductDTO> GetProductId(Guid id)
        {
            try
            {
                return await _unitOfWork.ProductRepository.GetByidAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<object>> UpdateProduct(ProductUDTO product)
        {
            var rs = new Response<object>(false, StringConst.Exeption);
            var createStrategy = _unitOfWork.CreateExecutionStrategy();
            await createStrategy.ExecuteAsync(async () =>
            {
                using (var db = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var productUpdate = await _unitOfWork.ProductRepository.FindAsync(x => x.Id.Equals(product.Id));
                        var productHasName = await _unitOfWork.ProductRepository.FindByAsyn(x => !x.Id.Equals(product.Id) && x.Name.ToLower().Equals(product.Name.ToLower()));
                        if (productHasName.Any())
                        {
                            rs.Result = StringConst.ProductExists;
                            return;
                        }
                        if (productUpdate != null)
                        {
                            productUpdate.Name = product.Name;
                            productUpdate.Price = product.Price;
                            productUpdate.DateAdd = product.DateAdd.UtcDateTime;

                            await _unitOfWork.ProductRepository.UpdateAsyn(productUpdate, product.Id);

                            var existingCategories = await _unitOfWork.ProductCategoryRepository.FindAllAsync(x => x.ProductId == product.Id);
                            foreach (var existingCategory in existingCategories)
                            {
                                if (!product.Categories.Contains(existingCategory.CategoryId))
                                {
                                    {
                                        await _unitOfWork.ProductCategoryRepository.DeleteAsyn(existingCategory);
                                    }
                                }
                            }

                            foreach (var item in product.Categories)
                            {
                                var pc = await _unitOfWork.ProductCategoryRepository.FindAsync(x => x.ProductId.Equals(productUpdate.Id) && x.CategoryId.Equals(item));
                                if (pc == null)
                                {
                                    await _unitOfWork.ProductCategoryRepository.AddAsyn(new ProductCategory
                                    {
                                        ProductId = productUpdate.Id,
                                        CategoryId = item
                                    });
                                }
                            }

                            await db.CommitAsync();
                            rs.Success = true;
                            rs.Result = StringConst.ProductUpdateDone;
                        }
                        else
                        {
                            rs.Result = StringConst.NotFound;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        await db.RollbackAsync();
                    }
                }
            });

            return rs;
        }
    }
}
