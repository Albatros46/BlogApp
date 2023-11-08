using BlogApp.Data.Abstract;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }
            return View();
        }
       
        [HttpPost]
       
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await _userRepository.Users.FirstOrDefaultAsync(x=>x.Email==model.Email && x.Password==model.Password);
                if (user != null)
                {
                    var userClaims=new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));//NameIdentifier model de id ye karsilik geldigi icin secildi. ve bu deger string olarak saklanir.
                    userClaims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, user.Name ?? ""));

                    if (user.Email == "info@sakcadag.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    var claimsIdentity=new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);//yukarida olusturulan cookie leri burada cookie semalarina aktardik
                    var autProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, //Login sayfasindaki 'beni hatirla' butonu her kullanici icin default olarak true secildi.
                    };
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);//daha once olusturulmus cookie ler varsa silinsin

                    //daha sonra giris yap
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity),
                        autProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanici adi veya parola yanlis");
                }
            }
            
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}
