using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. controller calismasi icin 
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));//Diger yontem asagidaki gibi
//builder.Services.AddDbContext<BlogContext>(opt =>
//{
//    var config = builder.Configuration;
//    var connectionString = config.GetConnectionString("DefaultConnection");
//    opt.UseSqlite(connectionString);
//    //MySql db
//    //var connectionMysql = config.GetConnectionString("MySqlConnection");

//    //opt.UseMySQL(connectionMysql);
//});

#region Dependency injection
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {//giris yapilmadan islem yapilacagi zaman gitmesini istedigimiz sayfaya yönlendiriyoruz
        opt.LoginPath = "/user/login";
        opt.LogoutPath= "/";
    });//Uygulamaya üye olduktan sonra browserde cookie ile Authentication edilmesini aktif ediyoruz

var app = builder.Build();

app.UseRouting();//CookieAuthenticationDefaults calistirabilmek icin gerekli
app.UseAuthentication();//yukaridaki CookieAuthenticationDefaults uygulamada aktif edilmesi saglandi
app.UseAuthorization();// ve uygulamanin belli bölümlerini kullanmayi saglayacak

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SeedData.TestVerileriniDoldur(app);

app.UseHttpsRedirection();
app.UseStaticFiles();//wwwroot altindaki dosyalari aktif etme

app.UseRouting();

app.UseAuthorization();

//https://localhost:7215/posts/AspNet-Core    gibi bir url hazirlanmasini saglayacagiz.
//post details Url
app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/details/{url}", //daha sonra index.cshtml de link verirken <a href="/posts/@post.Url" </a>
    defaults:new {Controller= "Post",Action= "Details" }
    );
//tag url
app.MapControllerRoute(
    name:"post-by-tag",
    pattern: "posts/tags/{tag}", //index action da string tag olarak parametre eklenecek
     defaults: new { Controller = "Post", Action = "Index" }
    );
//user profil
app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}", //index action da string tag olarak parametre eklenecek
     defaults: new { Controller = "User", Action = "Profile" }
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
