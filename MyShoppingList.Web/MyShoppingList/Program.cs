using Microsoft.EntityFrameworkCore;
using MyShoppingList.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ShoppingListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingListContext") ?? throw new InvalidOperationException("Connection string 'ShoppingListContext' not found.")));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.UseStaticFiles();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ShoppingListContext>();
    //EnsureCreated doesn't create a migrations history table and so can't be used with migrations.
    //It's designed for testing or rapid prototyping where the database is dropped and re-created frequently.
    //context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.Run();
