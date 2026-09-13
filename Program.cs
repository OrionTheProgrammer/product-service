using Scalar.AspNetCore;
using Asp.Versioning;
using Product_Service.Data;
using Microsoft.EntityFrameworkCore;
using Product_Service.Repository;
using Product_Service.Service;
using Product_Service.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("Sqlite") ?? throw new InvalidOperationException("No existe ConnectionStrings:Sqlite.");

builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IProductRepository, SqliteProductRepository>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("Version");
}).AddMvc(options =>
{
    options.Conventions
        .Controller<ProductController>()
        .HasApiVersion(new(1, 0));
    options.Conventions
        .Controller<ProductController>()
        .HasApiVersion(new(2, 0));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
}).AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().WithDocumentPerVersion();

    app.MapScalarApiReference(options =>
    {
        foreach (var version in app.DescribeApiVersions())
        {
            options.AddDocument(
                version.GroupName,
                $"Productos {version.GroupName}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
