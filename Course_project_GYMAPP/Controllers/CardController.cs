using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Course_project_GYMAPP.Controllers
{
    public class CardController : BaseController
    {
        private readonly IPersonalCardService personalCard;
        private readonly IUserService userService;

        public CardController(IPersonalCardService personalCard, IUserService userService)
        {
            this.personalCard = personalCard;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("User"))
                {
                    var user = (await userService.GetUserByName(User.Identity.Name)).Data;
                    var card = (await personalCard.GetPersonalCard(id)).Data;
                    var resp = await userService.EditUserCard(user, card);
                    ModelState.AddModelError("", resp.Description);
                }
                else if (!User.IsInRole("User") && User.Identity.IsAuthenticated)
                {
                    ModelState.AddModelError("", "Вам не потрібен абонемент");
                }
                else
                {
                    ModelState.AddModelError("", "Необхідно авторизуватися");
                    return RedirectToAction("Register", "Account");
                }
            }
            return View();
        }
    }
}
