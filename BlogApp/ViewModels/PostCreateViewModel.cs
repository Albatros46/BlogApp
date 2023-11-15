using BlogApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class PostCreateViewModel
    {
        public int PostId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? Url { get; set; }//url de id ile gorunmesi yerine url uzantisi ekleyerek goruntulenmesini saglayacagiz.
    //    public string? Image { get; set; }
    //    public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }
    //    public List<Tag> Tags { get; set; } = new List<Tag>();//Bir yorum birden fazla tag alabilir.
    }
}
