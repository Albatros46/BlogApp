using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        
        private IPostRepository _postRepository;
       

        public PostController( IPostRepository postRepository)
        {
           
            _postRepository = postRepository;
            
        }

        public async Task<IActionResult> Index(string tag)
        {
            var post = _postRepository.Posts;

            if (!string.IsNullOrEmpty(tag))
            {//Metod a tag bilgisi gelmis ise 
                post = post.Where(x => x.Tags.Any(t => t.Url == tag));
            }
            return View(
                    new PostsViewModel
                    {
                        Posts=await post.ToListAsync(),
                        
                    }
                );
        }

        public async Task<IActionResult> Details(string url)
        {//Herhangi bir Url ile Detay sayfasina gidildiginde o detaya sayfasina ait yorumlari da getirecek
            return View(await _postRepository
                .Posts
                .Include(x => x.Tags)
                .Include(x => x.Comments)//Comments lere gidildikten sonra
                .ThenInclude(x => x.User)//Comments icindeki user bilgisi de cekilecek
                .FirstOrDefaultAsync(p=>p.Url==url));
        }
    }
}
