using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using RubberDuckShop.ProductService.Application.Queries.Product;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetProductsPaginatedQuery).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"];

app.MapGet("/products", async ([FromServices] IMediator mediator, Guid? productCategoryId, int pageSize, int pageNumber) =>
{
    var products = await mediator.Send(new GetProductsPaginatedQuery()
    {
        PageSize = pageSize,
        PageNumber = pageNumber,
        ProductCategoryId = productCategoryId
    });

    return products is not null ? Results.Ok(products) : Results.NotFound();
});

//app.MapGet("/weatherforecast", (HttpContext httpContext) =>
//{
//    httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.RequireAuthorization();

app.Run();
