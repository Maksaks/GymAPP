using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Course_project_GYMAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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