// "/*
// * -----------------------------------------------------------------------------
// * File name: TourkitAPIResponse.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"
using System.ComponentModel.DataAnnotations;

namespace TourkitTest.Data.Response
{
    public class TourkitAPIResponse<T>
    {
        [Required]
        public int Code { get; set; }
        [Required]
        public Response<T> Response { get; set; }
        public TourkitAPIResponse(int statusCode = 200, Response<T> response = null)
        {
            Response = response;
            Code = statusCode;
        }
    }
    public class Response<T>
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public T Result { get; set; }
        /// <summary>
        /// Return data API
        /// </summary>
        /// <param name="s">True or Faslse</param>
        /// <param name="d">Params</param>
        public Response(bool success, T result)
        {
            Success = success;
            Result = result;
        }
    }
}
