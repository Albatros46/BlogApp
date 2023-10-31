using BlogApp.Entities;

namespace BlogApp.ViewModels
{
    public class PostsViewModel
    {//Sayfaya hem post hemde tag entitisini tasiyacak
        public List<Post> Posts { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
