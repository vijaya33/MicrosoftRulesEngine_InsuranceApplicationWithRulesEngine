using Microsoft.AspNetCore.Mvc;

namespace InsuranceApplicationWithRulesEngine.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
