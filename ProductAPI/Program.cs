using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Api.Configuration.Swagger.Examples;
using ProductAPI.Common.Profiles.Product;
using ProductAPI.Common.Profiles.ProductGroup;
using ProductAPI.Common.Validators;
using ProductAPI.Core.Services.Product;
using ProductAPI.Core.Services.ProductGroup;
using ProductAPI.Core.Services.ProductStore;
using ProductAPI.Core.Validators;
using ProductAPI.Data.Data;
using ProductAPI.Data.Repositories.Product;
using ProductAPI.Data.Repositories.ProductGroup;
using ProductAPI.Data.Repositories.ProductStore;
using ProductAPI.Data.Repositories.Store;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
});

// Add validators
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductGetDtoValidator>();

var connectionString =
    builder.Configuration.GetConnectionString("ProductsDatabase")
    ?? throw new InvalidOperationException("Connection string" + "'ProductsDatabase' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString)
);

// Add Services
builder.Services.AddScoped<IProductStoreService, ProductStoreService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductGroupService, ProductGroupService>();
builder.Services.AddScoped<IStoreService, StoreService>();

//Add Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductStoreRepository, ProductStoreRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

//Add Profiles
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(ProductGroupProfile));

//Add swagger examples
builder.Services.AddSwaggerExamplesFromAssemblyOf<NotFoundErrorExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProductCreatedExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProductGroupTreeExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProductListExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ServerErrorExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ValidationErrorExample>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.ExampleFilters();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
        c.SwaggerEndpoint("/swagger/v1/ProductAPI.json", "ProductAPI V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
