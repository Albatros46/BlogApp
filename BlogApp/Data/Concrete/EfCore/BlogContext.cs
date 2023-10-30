using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BlogApp.Data.Concrete.EfCore
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
           

        }
        public DbSet<Post> Posts => Set<Post>(); //{ get; set; }
        public DbSet<Tag> Tags => Set<Tag>();//{ get; set; }
        public DbSet<Comment> Comments => Set<Comment>();//{ get; set; }
        public DbSet<User> Users => Set<User>();//{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Döngüsel veya çoklu cascade yollarını engellemek için dış anahtar kısıtlamalarını tanımlayın.
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;database=mydatabase;user=root;password=12345");
        //}

    }
}
