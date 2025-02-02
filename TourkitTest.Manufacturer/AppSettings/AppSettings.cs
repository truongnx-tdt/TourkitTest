// "/*
// * -----------------------------------------------------------------------------
// * File name: AppSettings.cs
// * Create date: {date}
// * Update date: {date}
// * Author: {author}
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-TDT. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using Microsoft.Extensions.Configuration;

namespace TourkitTest.Manufacturer.AppSettings
{
    public static class AppSettings
    {
        public static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true).AddEnvironmentVariables().Build();

        /// <summary>
        /// ConnectionString From AppSettings
        /// </summary>
        public static string ConnectionString
        {
            get { return configuration.GetConnectionString("DefaultConnection") ?? "Not found"; }
        }
    }
}
