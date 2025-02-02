// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductsAPIController.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourkitTest.Application.Services.Products;
using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;
using TourkitTest.Data.Response;
using TourkitTest.Manufacturer.CommonConstant;

namespace TourkitTest.Controllers
{
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ILogger<ProductsAPIController> _logger;
        public ProductsAPIController(IProductServices productServices, ILogger<ProductsAPIController> logger)
        {
            _productServices = productServices;
            _logger = logger;
        }

        [HttpGet]
        [Route(RouteCommon.ProductByID)]
        public async Task<TourkitAPIResponse<ProductDTO>> GetProductById(Guid id)
        {
            var product = await _productServices.GetProductId(id);
            return new TourkitAPIResponse<ProductDTO>(200, new Response<ProductDTO>(true, product));
        }

        [HttpPost]
        [Route(RouteCommon.Product)]
        public async Task<ProductResponse> GetProducts(DataTableRequest request)
        {
            var products = await _productServices.GetAllProductsWithCategoriesAsync(request);
            return products;
        }

        [HttpDelete]
        [Route(RouteCommon.DeleteProduct)]
        public async Task<TourkitAPIResponse<bool>> DeleteProduct([FromBody] List<Guid> ids)
        {
            var rs = await _productServices.DeleteProducts(ids);
            return new TourkitAPIResponse<bool>(200, new Response<bool>(rs, rs));
        }
        [HttpPost]
        [Route(RouteCommon.AddProduct)]
        public async Task<TourkitAPIResponse<object>> AddProduct([FromBody] ProductCDTO product)
        {
            var rs = await _productServices.AddProduct(product);
            return new TourkitAPIResponse<object>(200, rs);
        }
        [HttpPost]
        [Route(RouteCommon.UpdateProduct)]
        public async Task<TourkitAPIResponse<object>> UpdateProduct([FromBody] ProductUDTO product)
        {
            var rs = await _productServices.UpdateProduct(product);
            return new TourkitAPIResponse<object>(200, rs);
        }
    }
}
