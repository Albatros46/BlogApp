using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entities;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            var claims = User.Claims;
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

        //[HttpPost]
        //public JsonResult AddComment(int PostId,  string Text)
        //{//Jquery kullanarak yazilan yorumu direkt sayfa icerisinde gosterme islemi. Details.cshtml de javaScrit kodlarini yazacagiz.
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var userName = User.FindFirstValue(ClaimTypes.Name);
        //    var avatar = User.FindFirstValue(ClaimTypes.UserData);
        //    var entity = new Comment
        //    { //JsonResult
        //        Text = Text,
        //        PublisedOn = DateTime.Now,
        //        PostId = PostId,

        //        UserId=int.Parse(userId ?? "")
        //    };
        //    _commentRepository.CreateComment(entity);
        //    //return RedirectToAction("post_details", new { url = Url });
        //    //return View();
        //    return Json(new
        //    {//Details sayfasindaki script alaninda t
        //        userName,
        //        Text,
        //        entity.PublisedOn,
        //        avatar
        //    });
        //}
        [HttpPost]
        public IActionResult AddComment(int PostId, string UserName, string Text,string Url)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment
            {
                Text = Text,
                PublisedOn = DateTime.Now,
                PostId = PostId,

                UserId = int.Parse(userId ?? ""),
                
            };
            _commentRepository.CreateComment(entity);
            //1. Yöntem: Program.cs de root semasindaki name 1.parametre, 2. parametre program.cs deki url kismi yukarida tanimlanan Url ye esitlenecek
            return RedirectToRoute("post_details",new {url=Url});
            //return Redirect("/posts/details/"+Url);//Yorum yapildiktan sonra Details.cshtml de input icerisinde hidden type ile url tutuluyor olacak ve o url burada tekrar döndürülecek
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _postRepository.CreatePost(
                    new Post
                    {
                        Title=model.Title,
                        Content=model.Content,
                        Url=model.Url,
                        UserId=int.Parse(userId ?? ""),
                        PublishedOn=DateTime.Now,
                        Image="1.jpg",
                        IsActive=false,

                    });
                return RedirectToAction("Index");
            }
           
            return View(model);
        }
    }
}
