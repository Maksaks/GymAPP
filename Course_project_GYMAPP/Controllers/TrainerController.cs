using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Service.Implementations;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Course_project_GYMAPP.Controllers
{
    public class TrainerController : Controller
    {
        private readonly IGymUserService gymUserService;
        private readonly IUserService userService;
        private static string ErrorMessage;
        public TrainerController(IGymUserService gymUserService, IUserService userService)
        {
            this.gymUserService = gymUserService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var res = await gymUserService.GetUsers();
            if(ErrorMessage != null)
            {
                ModelState.AddModelError("", ErrorMessage);
                ErrorMessage = string.Empty;
            }
            if(res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(res.Data);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGymUser(string name)
        {
            var res = await gymUserService.AddUser(name);
            
            ErrorMessage = res.Description;
            return RedirectToAction("Index", "Trainer");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGymUser(int id)
        {
            var res = await gymUserService.DeleteUser(id);
            
            ErrorMessage = res.Description;
            return RedirectToAction("Index", "Trainer");
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.CreateUser(model);
                if (res.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    ErrorMessage = res.Description;
                    return RedirectToAction("Index", "Trainer");
                }
            }
            return View(model);
        }
    }
}
