// "/*
// * -----------------------------------------------------------------------------
// * File name: Program.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using TourkitTest.Manufacturer.AppSettings;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TourkitTest.Data.EF;
using TourkitTest.Data.EF.Seed;
using TourkitTest.Data.EF.Uow;
using TourkitTest.Application.Services.Products;
using Microsoft.AspNetCore.Diagnostics;
using TourkitTest.Data.Response;
using Microsoft.AspNetCore.Builder;
using TourkitTest.Application.Services.Categories;
using TourkitTest.Manufacturer.CommonConstant;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddControllersWithViews();
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(AppSettings.ConnectionString, cfg =>
        {
            cfg.MigrationsAssembly("TourkitTest.Data.EF");
            cfg.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
        });
    });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<DbInit>();
    builder.Services.AddTransient<IProductServices, ProductServices>();
    builder.Services.AddTransient<ICategoryService, CategoryService>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.CustomSchemaIds(type => type.ToString()); 
    });

    var app = builder.Build();

    #region seed data
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        //var context = services.GetRequiredService<ApplicationDbContext>();
        //await context.Database.MigrateAsync();
        //var dbInit = services.GetRequiredService<DbInit>();
        //dbInit.Seed().Wait();
    }
    #endregion

    app.UseExceptionHandler(cfg =>
    {
        dynamic responseError = new System.Dynamic.ExpandoObject();
        cfg.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var apiResponse = new TourkitAPIResponse<object>(
                   statusCode: context.Response.StatusCode,
                   response: new Response<object>(
                       success: false,
                       result: exception?.Message ?? StringConst.Exeption
                   )
               );
            logger.Error(exception, StringConst.Exeption);
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(apiResponse));
        });
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHsts();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=ProductUI}/{action=Index}");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}