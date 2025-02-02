// "/*
// * -----------------------------------------------------------------------------
// * File name: CategoriesAPIController.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourkitTest.Application.Services.Categories;
using TourkitTest.Data.DTO;
using TourkitTest.Data.Entities;
using TourkitTest.Data.Response;
using TourkitTest.Manufacturer.CommonConstant;

namespace TourkitTest.Controllers
{
    [ApiController]
    public class CategoriesAPIController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesAPIController> _logger;
        public CategoriesAPIController(ICategoryService categoryService, ILogger<CategoriesAPIController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet]
        [Route(RouteCommon.CateByID)]
        public async Task<TourkitAPIResponse<CategoryDTO>> GetCtgById(Guid id)
        {
            var rs = await _categoryService.GetCategoryId(id);
            return new TourkitAPIResponse<CategoryDTO>(200, new Response<CategoryDTO>(true, rs));
        }

        [HttpGet]
        [Route(RouteCommon.Category)]
        public async Task<TourkitAPIResponse<CategoriesResponse>> GetCtgs()
        {
            var rs = await _categoryService.GetAllCategories();
            return new TourkitAPIResponse<CategoriesResponse>(200, new Response<CategoriesResponse>(true, rs));
        }

        [HttpDelete]
        [Route(RouteCommon.DeleteCate)]
        public async Task<TourkitAPIResponse<object>> Delete([FromBody] List<Guid> ids)
        {
            var rs = await _categoryService.DeleteCategorys(ids);
            return new TourkitAPIResponse<object>(200, rs);
        }
        [HttpPost]
        [Route(RouteCommon.AddCate)]
        public async Task<TourkitAPIResponse<object>> Add([FromBody] CategoryCDTO rq)
        {
            var rs = await _categoryService.AddCategory(rq);
            return new TourkitAPIResponse<object>(200, rs);
        }
        [HttpPost]
        [Route(RouteCommon.UpdateCate)]
        public async Task<TourkitAPIResponse<object>> UpdateProduct([FromBody] CategoryUDTO rq)
        {
            var rs = await _categoryService.UpdateCategory(rq);
            return new TourkitAPIResponse<object>(200, rs);
        }
    }
}
