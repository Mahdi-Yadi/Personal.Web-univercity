using Microsoft.AspNetCore.Mvc;
using Web.Data.Contexts;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _dbContext;

        public HomeController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var site = _dbContext.Sites.FirstOrDefault(s => s.IsActive);

            var projects = _dbContext.Projects
                .OrderByDescending(p => p.Id)
                .Where(p => p.IsDeleted == false)
                .Take(9)
                .ToList();

            ViewBag.site = site;
            ViewBag.project = projects;

            return View();
        }



    }
}