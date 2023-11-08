using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entities;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;

        public PostController(IPostRepository postRepository, ICommentRepository commentRepository)
        {

            _postRepository = postRepository;
            _commentRepository = commentRepository;
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
  /*      [HttpPost]
        public JsonResult AddComment(int PostId, string UserName, string Text)
        {//Jquery kullanarak yazilan yorumu direkt sayfa icerisinde gosterme islemi. Details.cshtml de javaScrit kodlarini yazacagiz.
            var entity = new Comment
            { //JsonResult
                Text = Text,
                PublisedOn = DateTime.Now,
                PostId = PostId,
                
                User = new User { UserName = UserName, Image = "p1.jpg" }
            };
            _commentRepository.CreateComment(entity);
            //return RedirectToAction("post_details", new { url = Url });
            //return View();
            return Json(new
            {//Details sayfasindaki script alaninda t
                UserName,
                Text,
                entity.PublisedOn,
                entity.User.Image
            });
        }
  */
        public IActionResult AddComment(int PostId, string UserName, string Text,string Url)
        {
            var entity = new Comment
            {
                Text = Text,
                PublisedOn = DateTime.Now,
                PostId= PostId,
                User=new User { Name = UserName, Image= "avatar.jpg" }
            };
            _commentRepository.CreateComment(entity);
            //1. Yöntem: Program.cs de root semasindaki name 1.parametre, 2. parametre program.cs deki url kismi yukarida tanimlanan Url ye esitlenecek
            return RedirectToRoute("post_details",new {url=Url});
            //return Redirect("/posts/details/"+Url);//Yorum yapildiktan sonra Details.cshtml de input icerisinde hidden type ile url tutuluyor olacak ve o url burada tekrar döndürülecek
        }
    }
}
