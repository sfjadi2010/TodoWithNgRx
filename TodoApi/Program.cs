using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.MapGet("/todoitems", async (TodoDbContext todoDbContext) => await todoDbContext.Tasks.ToListAsync());
app.MapGet("/todoitems/{id}", async (TodoDbContext todoDbContext, long id) => await todoDbContext.Tasks.FindAsync(id));
app.MapGet("/todoitems/complete", async (TodoDbContext todoDbContext) => await todoDbContext.Tasks.Where(task => task.IsComplete).ToListAsync());

app.MapPost("/todoitems", async (TodoDbContext todoDbContext, TodoApi.Models.Task task) =>
{
    todoDbContext.Tasks.Add(task);
    await todoDbContext.SaveChangesAsync();
    return Results.Created($"/todoitems/{task.Id}", task);
});

app.MapPut("/todoitems/{id}", async (TodoDbContext todoDbContext, long id, TodoApi.Models.Task task) =>
{
    if (id != task.Id)
    {
        return Results.BadRequest();
    }

    todoDbContext.Entry(task).State = EntityState.Modified;
    await todoDbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (TodoDbContext todoDbContext, long id) =>
{
    var task = await todoDbContext.Tasks.FindAsync(id);
    if (task is null)
    {
        return Results.NotFound();
    }

    todoDbContext.Tasks.Remove(task);
    await todoDbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.UseCors("MyCorsPolicy");

app.Run();
