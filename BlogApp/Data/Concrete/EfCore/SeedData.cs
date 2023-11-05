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
                            new Tag { Text="web programlama",Url= "web-programlama", Color=TacColors.primary},
                            new Tag { Text="mobil programlama",Url= "mobil-programlama" ,Color = TacColors.info },
                            new Tag { Text="frontent",Url="frontend",Color=TacColors.sencondary},
                            new Tag { Text="backend programlama",Url= "backend-programlama" ,Color=TacColors.warning},
                            new Tag { Text="android",Url="android",Color=TacColors.danger},
                            new Tag { Text="ios",Url="ios",Color=TacColors.success}
                        );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                            new User { UserId=1,UserName="Albatros46",Image="p1.jpg"},
                            new User { UserId=2,UserName="MarasliAslan46", Image ="p4.jpg"},
                            new User { UserId=3,UserName="Cengaver46", Image ="p3.jpg"},
                            new User { UserId=4,UserName="Akif46", Image ="p2.jpg"}
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
                                Url="asp-net-core",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-10),
                                Tags = context.Tags.Take(3).ToList(),
                                Image = "1.jpg",
                                UserId = 1,
                                Comments=new List<Comment>
                                {
                                    new Comment{CommentId=1,Text="Cok güzel ve faydali bir egitim serisi tesekkür ederim.",PublisedOn=new DateTime(),UserId=1}, 
                                    new Comment{CommentId=2,Text="Egitminizin devamini sabirsizlikla bekliyorum.",PublisedOn=new DateTime().AddHours(-2),UserId=2}, 
                                }
                            },
                            new Post
                            {
                                PostId = 2,
                                Title = "Java Spring Boot",
                                Content = "Java Spring Boot ile Web programlama dersleri",
                                Url = "java-spring-boot",

                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-5),
                                Tags = context.Tags.Take(2).ToList(),
                                Image = "2.jpg",

                                UserId = 3
                            },
                            new Post
                            {
                                PostId = 3,
                                Title = "Python Programlama",
                                Content = "Python Pandas, numpy ve Django Dersleri",
                                Url = "python-programlama",

                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-15),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",

                                UserId = 2
                            },
                             new Post
                             {
                                 PostId = 4,
                                 Title = "C++ Programlama",
                                 Content = "C++ ile programlama",
                                 Url = "C-ile-programlama",

                                 IsActive = true,
                                 PublishedOn = DateTime.Now.AddDays(-3),
                                 Tags = context.Tags.Take(4).ToList(),
                                 Image = "4.jpg",

                                 UserId = 1
                             }
                        ); 
                    context.SaveChanges();
                }
            }
        }
    }
}
