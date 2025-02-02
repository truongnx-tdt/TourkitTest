// "/*
// * -----------------------------------------------------------------------------
// * File name: StringUtils.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

namespace TourkitTest.Manufacturer.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Utils compare two string value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueCompare"></param>
        /// <returns></returns>
        public static bool Compare(this string value, string valueCompare)
        {
            return value.Trim().ToLower().Equals(valueCompare.Trim().ToLower());
        }
        public static string ConvertString(this string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value;
        }
    }
}
