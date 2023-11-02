using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class TagsMenu:ViewComponent
    {//Tag kartinin her sayfada görüntülenebilmesi ve veri cagirma islemini sürekli istek halinde olmadan
    //    bir yerden bütün sayfalara gönderilmesi icin ViewsComponents kullaniyoruz.Daha sonra Views/Shared/Components/TagsMenu/Default.cshtml
    //    https://learn.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-7.0
        private ITagRepository _tagRepository;

        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            //  return View("Test.cshtml",_tagRepository.Tags.ToList());
            //Eger Views/Shared/Components/TagsMenu/Test.cshtml altindaki farkli cshtml kullanmak isterseniz parametre olarak yazmalisiniz.

            return View(await _tagRepository.Tags.ToListAsync());
        }
    }
}
