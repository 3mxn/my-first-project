using Microsoft.EntityFrameworkCore;
using Services;
using TodoApi;
var builder = WebApplication.CreateBuilder();
//builder.Services.AddDbContext<Tododb>(opt => opt.UseInMemoryDatabase("TodoList"));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod(); 

                                             
                      });
});
var app = builder.Build();
Console.WriteLine("API restarted with new CORS settings");
app.UseCors(MyAllowSpecificOrigins);


//app.MapGet("/todoitems", async (Tododb db) =>
//await db.Todos.ToListAsync());
//var todoItems = app.MapGroup("todoitems");

//app.MapGet("/Complete", async (Tododb db) =>
//await db.Todos.Where(t => t.IsComplete).ToListAsync());

//app.MapGet("/{id}", async (int id, Tododb db) =>
//await db.Todos.FindAsync(id)
//    is Todo todo
//       ? Results.Ok(todo)
//       : Results.NotFound());
//app.MapPost("/", async (Tododb db, Todo todo) =>
//{
//    db.Todos.Add(todo);
//    await db.SaveChangesAsync();
//    return Results.Created($"/todoitems/{todo.Id}", todo);
//});
//app.MapPut("/{id}", async (int id, Todo inputTodo, Tododb db) =>
//{
//    var todo = await db.Todos.FindAsync(id);
//    if (todo is null)
//    {
//        return Results.NotFound();
//    }
//    todo.Name = inputTodo.Name;
//    todo.IsComplete = inputTodo.IsComplete;
//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});
//app.MapDelete("/{id}", async (int id, Tododb db) =>
//{
//    if (await db.Todos.FindAsync(id) is Todo todo)
//    {
//        db.Todos.Remove(todo);
//        await db.SaveChangesAsync();
//        return Results.NoContent();
//    }
//    return Results.NotFound();
//});
//app.MapPatch("/{id}", async (int id, Tododb db, todoPatchDto inputTodo) =>
//{
//    var todo = await db.Todos.FindAsync(id);
//    if (todo is null) return Results.NotFound();
//    if (inputTodo.Name is not null) todo.Name = inputTodo.Name;
//    if (inputTodo.IsComplete is not null) todo.IsComplete = inputTodo.IsComplete.Value;
//    await db.SaveChangesAsync();


//    var ser = new BlogService();
//    ser.AddBlog(todo);
//    return Results.NoContent();
//});



/// blog apis
/// 

app.MapPost("/Blogs/", async (BlogDto blog) =>
{
    var service = new BlogService();
    await service.AddBlog(blog);

    return Results.Created($"/todoitems/1", blog);
});
app.MapGet("/Blogs/", async () =>
{
    var service = new BlogService();
    return Results.Ok(await service.GetAll());
    
});
app.MapGet("/Blogs/{id}", async (int id) =>
{
    var service = new BlogService();
    var blog=await service.GetById(id);
    if (blog==null)
    {
        return Results.NotFound();
    }
    return Results.Ok(blog);

});
app.MapPut("/Blogs/{id}", async (int id, Blog updatedBlog) =>
{
    using var db = new BloggingContext();
    var blog = await db.Blogs.FindAsync(id);
    if (blog == null)
    {
        return Results.NotFound();
    }
    blog.Url = updatedBlog.Url;
    await db.SaveChangesAsync();
    return Results.Ok(blog);
}
);
app.MapDelete("/Blogs/{id}", async (int id) =>
{
    var db = new BloggingContext();
    var blog = await db.Blogs.FindAsync(id);
    if(blog==null)
    {
        return Results.NotFound();
    }
    db.Blogs.Remove(blog);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.Run();