using AspNetCore.Unobtrusive.Ajax;
using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Enum;
using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Course_project_GYMAPP.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Course_project_GYMAPP.Controllers
{
    [Authorize(Roles = "Admin")]
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
            if (TempData["id"] == "" || TempData["id"] == null)
            {
                TempData["id"] = "v-pills-users-tab";
            }
            return View();
        }

        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
        public async Task<IActionResult> GetPersonalCardsData()
        {
            var resp = await personalCardService.GetPersonalCardsForAdmin();
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var cards = resp.Data;
            return PartialView("GetPersonalCardsData", cards);
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var resp = await userService.GetStatistics();
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            var stats = resp.Data;
            return PartialView("GetStatistics", stats);
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
            var resp = await userService.EditUser(userVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-users-tab";
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonalCard(AdminEditPersonalCardViewModel cardVM)
        {
            var resp = await personalCardService.EditPersonalCard(cardVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-card-tab";
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminEditAdminViewModel adminVM)
        {
            var resp = await adminService.EditAdmin(adminVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            if ((await adminService.GetAdminByName(User.Identity.Name)).StatusCode == Domain.Enum.StatusCode.UserNotFound)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, adminVM.Name),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin")
                    };
                var clid = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(clid));
            }
            TempData["id"] = "v-pills-admins-tab";
            return RedirectToAction("Index", "Admin");
            
        }

        [HttpPost]
        public async Task<IActionResult> EditTrainer(AdminEditTrainerViewModel trainerVM)
        {
            var resp = await trainerService.EditTrainer(trainerVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-trainers-tab";
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
            TempData["id"] = "v-pills-users-tab";
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var resp = new BaseResponse<bool>();
            if((await adminService.GetAdmins()).Data.Count() != 1)
            {
                resp = await adminService.DeleteAdmin(id);
            }
            else
            {
                TempData["id"] = "v-pills-admins-tab";
                Alert("Не можливо видалити останнього адміністратора!Додайте іншого та спробуйте ще раз.", NotificationType.error);
                return RedirectToAction("Index", "Admin");
            }
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            if ((await adminService.GetAdminByName(User.Identity.Name)).StatusCode != Domain.Enum.StatusCode.OK)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Alert("Ви видалили власний акаунт!", NotificationType.warning);
                return RedirectToAction("Index", "Home");
            }
            TempData["id"] = "v-pills-admins-tab";
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
            TempData["id"] = "v-pills-trainers-tab";
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
            TempData["id"] = "v-pills-card-tab";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> AddUser() => PartialView();
        [HttpGet]
        public async Task<IActionResult> AddTrainer() => PartialView();
        [HttpGet]
        public async Task<IActionResult> AddAdmin() => PartialView();
        [HttpGet]
        public async Task<IActionResult> AddPersonalCard() => PartialView();

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRegisterViewModel userVM)
        {
            var resp = await userService.CreateUser(userVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-users-tab";
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> AddTrainer(TrainerRegisterViewModel trainerVM)
        {
            var resp = await trainerService.CreateTrainer(trainerVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-trainers-tab";
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminRegisterViewModel adminVM)
        {
            var resp = await adminService.CreateAdmin(adminVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-admins-tab";
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> AddPersonalCard(PersonalCardViewModel personalCardVM)
        {
            var resp = await personalCardService.CreatePersonalCard(personalCardVM);
            if (resp.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return NotFound();
            }
            TempData["id"] = "v-pills-card-tab";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> SearchUser(string pattern)
        {
            var resp = await userService.Search(pattern);
            if (resp.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("SearchResultUser", resp.Data);
            }
            return PartialView("SearchResultUser", new List<User>());
        }

        [HttpGet]
        public async Task<IActionResult> SearchPersonalCard(string pattern)
        {
            var resp = await personalCardService.Search(pattern);
            if (resp.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("SearchResultPersonalCard", resp.Data);
            }
            return PartialView("SearchResultPersonalCard", new List<PersonalCard>());
        }

        [HttpGet]
        public async Task<IActionResult> SearchTrainer(string pattern)
        {
            var resp = await trainerService.Search(pattern);
            if (resp.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("SearchResultTrainer", resp.Data);
            }
            return PartialView("SearchResultTrainer", new List<Trainer>());
        }

        [HttpGet]
        public async Task<IActionResult> SearchAdmin(string pattern)
        {
            var resp = await adminService.Search(pattern);
            if (resp.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("SearchResultAdmin", resp.Data);
            }
            return PartialView("SearchResultAdmin", new List<Admin>());
        }
    }
}
