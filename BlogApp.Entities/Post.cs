using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<Tag> Tags { get; set; } = new List<Tag>();//Bir yorum birden fazla tag alabilir.
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
