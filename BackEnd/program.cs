using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;         // AppDbContext
using BackEnd.Services;     // TodoProviderFactory
using BackEnd.Providers;    // InMemoryTodoProvider, SqlTodoProvider
using Microsoft.AspNetCore.Http;

#nullable enable   

var builder = WebApplication.CreateBuilder(args);

// 1. Configure CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// 2. Register EF Core with SQLite provider
//    This sets up AppDbContext to use a local file 'todos.db'
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=todos.db")
);

// 3.  Register our two Todo providers and the factory in the DI container
//    - InMemoryTodoProvider  :  guest mode (keeps data in memory)
//    - SqlTodoProvider       :  scoped (one per HTTP request) for user mode
//    - TodoProviderFactory   :  scoped factory to pick the right provider
builder.Services.AddSingleton<InMemoryTodoProvider>();
builder.Services.AddScoped<SqlTodoProvider>();
builder.Services.AddScoped<TodoProviderFactory>();

// 4. Add MVC controllers and Swagger support
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// check point
app.MapGet("/", () => Results.Ok("API is running"));

// 5. Configure the HTTP 


app.UseCors("AllowAll");


// In development, enable Swagger UI for API exploration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use authorization (not do it here)
app.UseAuthorization();

app.MapControllers();

app.Run();
