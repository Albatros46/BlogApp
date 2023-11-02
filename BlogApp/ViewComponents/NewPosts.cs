using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class NewPosts: ViewComponent
    {
        private IPostRepository _postRepository;

        public NewPosts(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _postRepository
                .Posts
                .OrderByDescending(p=>p.PublishedOn)//Postlari yayin tarihine göre sirala
                .Take(5) //ve bunlardan ilk 5 listele
                .ToListAsync()
                );
        }
    }
}
