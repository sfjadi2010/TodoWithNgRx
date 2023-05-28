using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
