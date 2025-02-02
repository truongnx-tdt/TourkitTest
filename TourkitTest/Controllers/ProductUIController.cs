// "/*
// * -----------------------------------------------------------------------------
// * File name: ProductUIController.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using Microsoft.AspNetCore.Mvc;

namespace TourkitTest.Controllers
{
    public class ProductUIController : Controller
    {
        private readonly ILogger<ProductUIController> _logger;

        public ProductUIController(ILogger<ProductUIController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
