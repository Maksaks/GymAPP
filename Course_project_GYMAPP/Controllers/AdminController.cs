using AspNetCore.Unobtrusive.Ajax;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Course_project_GYMAPP.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly ITrainerService trainerService;

        public AdminController(IUserService userService, ITrainerService trainerService, IAdminService adminService)
        {
            this.userService = userService;
            this.trainerService = trainerService;
            this.adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersData()
        {
            var users = (await userService.GetUsers()).Data;
            if (users == null)
            {
                return NotFound();
            }
            return PartialView("GetUsersData", users);
        }
    }
}
