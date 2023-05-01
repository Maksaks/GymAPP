using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Service.Implementations;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Course_project_GYMAPP.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly IGymUserService gymUserService;
        private readonly IUserService userService;
        private readonly ITrainerService trainerService;
        private readonly IPersonalCardService personalCardService;
        public TrainerController(IGymUserService gymUserService, IUserService userService, ITrainerService trainerService, IPersonalCardService personalCardService)
        {
            this.gymUserService = gymUserService;
            this.userService = userService;
            this.trainerService = trainerService;
            this.personalCardService = personalCardService;
        }
        [Authorize(Roles = "Trainer")]
        [HttpGet]
        public async Task<IActionResult> Index(int i = 0)
        {
            var res = await gymUserService.GetUsers();
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

            if (res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                Alert(res.Description, Domain.Enum.NotificationType.success);
                return Ok();
            }
            Alert(res.Description, Domain.Enum.NotificationType.error);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGymUser(int id)
        {
            var res = await gymUserService.DeleteUser(id);
            if (res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                Alert(res.Description, Domain.Enum.NotificationType.success);
                return RedirectToAction("Index", "Trainer");
            }
            Alert(res.Description, Domain.Enum.NotificationType.error);
            return RedirectToAction("Index", "Trainer");
        }
        [HttpGet]
        public IActionResult Register() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            var res = await userService.CreateUser(model);
            if (res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                Alert(res.Description, Domain.Enum.NotificationType.success);
                return RedirectToAction("Index", "Trainer");
            }
            Alert(res.Description, Domain.Enum.NotificationType.error);
            return RedirectToAction("Index", "Trainer");
        }
        [HttpGet]
        public async Task<IActionResult> NewCard() => PartialView();

        [HttpPost]
        public async Task<IActionResult> NewCard(string userName, int cardId)
        {
            var res = await userService.NewCardForUser(userName, cardId);
            if (res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                Alert(res.Description, Domain.Enum.NotificationType.success);
                return Ok();
            }
            Alert(res.Description, Domain.Enum.NotificationType.warning);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainersInfo()
        {
            var res = await trainerService.GetTrainersInfo();
            if (res.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(res.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetSearchUserResult(string term)
        {
            var res = await userService.Search(term);
            var js = Json(res.Data);
            return js;
        }
        [HttpPost]
        public async Task<IActionResult> GetSearchCardResult(string term)
        {
            var res = await personalCardService.Search(term);
            var js = Json(res.Data);
            return js;
        }
    }
}
