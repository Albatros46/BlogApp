using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublisedOn { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;//Her bir yorum sadece bir Post icin oldugundan one to many iliskisi

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
