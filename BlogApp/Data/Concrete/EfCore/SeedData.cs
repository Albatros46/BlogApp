using Microsoft.EntityFrameworkCore;
using BlogApp.Entities;
namespace BlogApp.Data.Concrete.EfCore
{
    public class SeedData
    {//Projeye test verileri eklenecek.Program.cs de calistirarak test verilerini gönderecegiz.
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context=app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {//eger herhangi bir migration yoksa uygulama her calistiginda migration uygulanacak. 
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                            new Tag { Text="web programlama"},
                            new Tag { Text="mobil programlama"},
                            new Tag { Text="frontent"},
                            new Tag { Text="backend programlama"},
                            new Tag { Text="android"},
                            new Tag { Text="ios"}
                        );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                            new User { UserId=1,UserName="Albatros46"},
                            new User { UserId=2,UserName="MarasliAslan46"},
                            new User { UserId=3,UserName="Cengaver46"},
                            new User { UserId=4,UserName="Akif46"}
                        );
                    context.SaveChanges();

                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                            new Post
                            {
                                PostId = 1,
                                Title = "Asp.Net Core 7.0",
                                Content = "Asp.Net core Dersleri",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-10),
                                Tags = context.Tags.Take(3).ToList(),
                                UserId = 1
                            },
                            new Post
                            {
                                PostId = 2,
                                Title = "Java Spring Boot",
                                Content = "Java Spring Boot ile Web programlama dersleri",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-5),
                                Tags = context.Tags.Take(2).ToList(),
                                UserId = 3
                            },
                            new Post
                            {
                                PostId = 3,
                                Title = "Python Programlama",
                                Content = "Python Pandas, numpy ve Django Dersleri",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-3),
                                Tags = context.Tags.Take(4).ToList(),
                                UserId = 2
                            }
                        ) ;
                    context.SaveChanges();
                }
            }
        }
    }
}
