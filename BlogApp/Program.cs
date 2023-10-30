using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<BlogContext>(opt=>opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));//Diger yontem asagidaki gibi
builder.Services.AddDbContext<BlogContext>(opt =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("DefaultConnection");
    opt.UseSqlite(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SeedData.TestVerileriniDoldur(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
