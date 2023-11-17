using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Image { get; set; } 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public Rol Rol { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();//One To Many-> Bir kullanici birden fazla yorum yapabilir.

        public List<Comment> Comments { get; set; }=new List<Comment>();//Bir kullanici birden fazla yorum yapabilir.
    }
    public enum Rol
    {
        Admin,
        Moderator,
        Blogger,
        User
    }
}
