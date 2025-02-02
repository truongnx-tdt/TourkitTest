// "/*
// * -----------------------------------------------------------------------------
// * File name: CommonConst.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

namespace TourkitTest.Manufacturer.CommonConstant
{
    public static class RouteCommon
    {
        #region route api product
        public const string Product = "/api/products";
        public const string DeleteProduct = "/api/delete-products";
        public const string AddProduct = "/api/add-product";
        public const string UpdateProduct = "/api/update-product";
        public const string ProductByID = "/api/product-id";
        #endregion

        #region route api category
        public const string Category = "/api/categories";
        public const string DeleteCate = "/api/delete-categories";
        public const string AddCate = "/api/add-category";
        public const string UpdateCate = "/api/update-category";
        public const string CateByID = "/api/category-id";
        #endregion
    }
    public static class StringConst
    {
        public const string ProductExists = "Product Name already exists";
        public const string ProductAddDone = "Add Product Successfully";
        public const string ProductUpdateDone = "Update Product Successfully";

        public const string CtgExists = "Category Name already exists";
        public const string CtgAddDone = "Add Category Successfully";
        public const string CtgUpdateDone = "Update Category Successfully";
        public const string CtgExistsProduct = "There are already products in this category";
        public const string CtgDelete = "Deleted Successfully";

        public const string Exeption = "Error during the process! Please, Try again";
        public const string NotFound = "Not Found!";
    }
}
