using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace test.Controllers
{
    public class LoginController: Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            var claims = new List<Claim> {
                new Claim("Demo", "Value")
            };
            char[] login = model.UserName.ToCharArray();
            for (int i = 0; i < login.Length; i++)
            {
                login[i] = Char.ToLower(login[i]);
            }
            string Login = new string(login);
            if (Login == "test" && model.Password == "test") {
                var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                
                await HttpContext.SignInAsync("Cookie", claimPrincipal);
                return RedirectToRoute("Action", new { controller = "Action", action = "Index" });
            }
            else
            {
                ViewData["login"] = model.UserName;
                ViewData["errorLoginOrPassword"] = "error";
                return View();
            }
            
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
