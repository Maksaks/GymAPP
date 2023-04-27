using AspNetCore.Unobtrusive.Ajax;
using Course_project_GYMAPP.Domain.Enum;
using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Course_project_GYMAPP.Controllers
{
    public class AdminController : BaseController
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
        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> GetTrainer(int id)
        {
            var resp = await trainerService.GetTrainer(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var trainer = resp.Data;
            var trainerVM = new AdminEditTrainerViewModel()
            {
                ID = trainer.Id,
                Name = trainer.Name,
                Age = trainer.Age,
                Number = trainer.Number,
                ImgPath = trainer.ImgPath,
                AboutInfo = trainer.AboutInfo,
                Password = "",
                ConfirmPassword = ""
            };
            return PartialView("GetTrainer", trainerVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalCard(int id)
        {
            var resp = await personalCardService.GetPersonalCard(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var card = resp.Data;
            var cardVM = new AdminEditPersonalCardViewModel()
            {
                ID = card.ID,
                Name = card.Name,
                Duration = card.Duration,
                Price= card.Price
            };
            return PartialView("GetPersonalCard", cardVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var resp = await adminService.GetAdmin(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var admin = resp.Data;
            var adminVM = new AdminEditAdminViewModel()
            {
                ID = admin.Id,
                Name = admin.Name,
                Password= admin.Password
            };
            return PartialView("GetAdmin", adminVM);
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
            string error = string.Empty;
            foreach(var err in ModelState)
            {
                if(err.Value.Errors.Count > 0)
                {
                    foreach(var er in err.Value.Errors)
                    {
                        error += er.ErrorMessage.ToString();
                    }
                }
                
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonalCard(AdminEditPersonalCardViewModel cardVM)
        {
            if (ModelState.IsValid)
            {
                var resp = await personalCardService.EditPersonalCard(cardVM);
                if (resp.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    return NotFound();
                }
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminEditAdminViewModel adminVM)
        {
            if (ModelState.IsValid)
            {
                var resp = await adminService.EditAdmin(adminVM);
                if (resp.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    return NotFound();
                }
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditTrainer(AdminEditTrainerViewModel trainerVM)
        {
            if (ModelState.IsValid)
            {
                var resp = await trainerService.EditTrainer(trainerVM);
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

        [HttpPost]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var resp = await adminService.DeleteAdmin(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var resp = await trainerService.DeleteTrainer(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonalCard(int id)
        {
            var resp = await personalCardService.DeletePersonalCard(id);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}
