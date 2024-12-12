using Microsoft.EntityFrameworkCore;
using PetShopApplication;
using PetShopApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true; // Enabling Client-Side Validation
    });

// Configure DbContext with SQL Server
builder.Services.AddDbContext<PetShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetShopConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PetShopDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();
