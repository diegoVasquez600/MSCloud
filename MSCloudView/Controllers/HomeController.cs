using Microsoft.AspNetCore.Mvc;
using MSCloudView.DAO;
using MSCloudView.DTO;
using MSCloudView.Models;
using System.Diagnostics;

namespace MSCloudView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        MainBoardDTO boardDTO;
  

        public HomeController(ILogger<HomeController> logger, MSCloudDBContext context)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            boardDTO = new MainBoardDTO();
            return View(boardDTO.MainBoards().ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}