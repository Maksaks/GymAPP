using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Course_project_GYMAPP.Controllers
{
    public class TrainerController : Controller
    {
        private readonly IGymUserService gymUserService;
        private static string ErrorMessage;
        public TrainerController(IGymUserService gymUserService)
        {
            this.gymUserService = gymUserService;
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
    }
}
