using AspNetCore.Unobtrusive.Ajax;
using Course_project_GYMAPP.Domain.ViewModels;
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
            var resp = await userService.GetUsers();
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var users = resp.Data;
            return PartialView("GetUsersData", users);
        }
        [HttpPost]
        public async Task<IActionResult> GetAdminsData()
        {
            var resp = await adminService.GetAdmins();
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var admins = resp.Data;
            return PartialView("GetAdminsData", admins);
        }
        [HttpPost]
        public async Task<IActionResult> GetTrainersData()
        {
            var resp = await trainerService.GetTrainers();
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var trainers = resp.Data;
            return PartialView("GetTrainersData", trainers);
        }
        [HttpPost]
        public async Task<IActionResult> GetPersonalCardsData()
        {
            var resp = await personalCardService.GetPersonalCards();
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var cards = resp.Data;
            return PartialView("GetPersonalCardsData", cards);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var resp = await userService.GetUser(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var user = resp.Data;
            var userVM = new AdminEditUserViewModel()
            {
                ID = user.Id,
                Name = user.Name,
                Age= user.Age,
                Number= user.Number,
                CardBefore = user.CardBefore,
                Password = "",
                ConfirmPassword = ""
            };
            return PartialView("GetUser", userVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(AdminEditUserViewModel userVM)
        {
            if(ModelState.IsValid)
            {
                var resp = await userService.EditUser(userVM);
                if (resp.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    return NotFound();
                }
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var resp = await userService.DeleteUser(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}
