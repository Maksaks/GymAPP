using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Models;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Course_project_GYMAPP.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGymUserService gymUserService;
        public HomeController(IGymUserService gymUserService)
        {
            this.gymUserService = gymUserService;
        }

        public async Task<IActionResult> GetGymUserCount()
        {
            var countUsers = await gymUserService.GetCountOfUsersInGym();
            return PartialView("GetGymUserCount", countUsers.Data);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}