using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Models;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Course_project_GYMAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGymUserRepository gymUserRepository;

        public HomeController(IGymUserRepository gymUserRepository)
        {
            this.gymUserRepository = gymUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            await gymUserRepository.Select();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}