using DynamicPrices;
using DynamicPricing.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProduseService, ProduseService>();
builder.Services.AddTransient<DatabaseService, DatabaseService>();

builder.Services.AddDbContextPool<ApplicationDbContext>((ServiceProvider, options) =>
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MySQLConnection");
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

app.Run();