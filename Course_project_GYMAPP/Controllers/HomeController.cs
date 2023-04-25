using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Models;
using Course_project_GYMAPP.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Course_project_GYMAPP.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IGymUserRepository gymUserRepository)
        {
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