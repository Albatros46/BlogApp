using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogContext _context;
        private IPostRepository _repository;
        public PostController(BlogContext context, IPostRepository postRepository = null)
        {
            _context = context;
            _repository = postRepository;
        }

        public IActionResult Index()
        {
            return View(_repository.Posts.ToList());
        }
    }
}
