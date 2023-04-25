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
        private readonly IPersonalCardService personalCardService;

        public AdminController(IUserService userService, ITrainerService trainerService, IAdminService adminService, IPersonalCardService personalCardService)
        {
            this.userService = userService;
            this.trainerService = trainerService;
            this.adminService = adminService;
            this.personalCardService = personalCardService;
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
        [HttpPost]
        public async Task<IActionResult> GetAdminsData()
        {
            var admins = (await adminService.GetAdmins()).Data;
            if (admins == null)
            {
                return NotFound();
            }
            return PartialView("GetAdminsData", admins);
        }
        [HttpPost]
        public async Task<IActionResult> GetTrainersData()
        {
            var trainers = (await trainerService.GetTrainers()).Data;
            if (trainers == null)
            {
                return NotFound();
            }
            return PartialView("GetTrainersData", trainers);
        }
        [HttpPost]
        public async Task<IActionResult> GetPersonalCardsData()
        {
            var cards = (await personalCardService.GetPersonalCards()).Data;
            if (cards == null)
            {
                return NotFound();
            }
            return PartialView("GetPersonalCardsData", cards);
        }
    }
}
