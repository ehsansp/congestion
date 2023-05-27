using System.Reflection;
using CodePractice.Product.ApplicationService.Features.Person.Commands.CreatePerson;
using CodePractice.Product.ApplicationService.Profile;
using CodePractice.Product.Domain.Context;
using CodePractice.Product.Repository.GenericRepository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IProductUnitOfWork, ProductUnitOfWork>();
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddMediatR(typeof(CreateProductCommand).GetTypeInfo().Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
