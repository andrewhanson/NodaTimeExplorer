using Microsoft.AspNetCore.Mvc;

namespace NodaTimeExplorer.Controllers
{
    public class HomeController: Controller
    {  
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}