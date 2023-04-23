using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Course_project_GYMAPP.Service.Interfaces;
using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Domain.Entity;
using System.Data;

namespace Course_project_GYMAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await accountService.Register(model);
                if (res.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(res.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", res.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await accountService.Login(model);
                if (res.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(res.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", res.Description);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var res = await userService.GetUserByName(User.Identity.Name);
            if (res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(new UserEditDataViewModel()
                {
                    Name = res.Data.Name,
                    Age = res.Data.Age,
                    Number= res.Data.Number
                });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await accountService.EditUserData(User.Identity.Name, model);
                if (res.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    if(User.Identity.Name != model.Name)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, model.Name),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
                        };
                        var clid = new ClaimsIdentity(claims, "ApplicationCookie",
                        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(clid));
                    }
                    return RedirectToAction("EditProfile", "Account");
                }
                ModelState.AddModelError("", res.Description);
            }
            return View(model);
        }
    }
}
