using Microsoft.AspNetCore.Mvc;
using Web.Data.Contexts;
using Web.Data.Models.Projects;
using Web.Data.Models.Setting;

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

            if (site == null)
            {
                Site newsite = new Site();

                newsite.Name = "";
                newsite.Address = "";
                newsite.Bio = "";
                newsite.Phone = "";
                newsite.IsActive = true;

                _dbContext.Sites.Add(new Site());
                _dbContext.SaveChanges();

                site = newsite;
            }

            var projects = _dbContext.Projects
                .OrderByDescending(p => p.Id)
                .Where(p => p.IsDeleted == false)
                .Take(9)
                .ToList();

            if (projects.Count  == 0)
            {
                Project project = new Project();

                project.ImageName = "";
                project.Text = "تست";
                project.Title = "تست";

                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();

                projects.Add(project);
            }

            ViewBag.site = site;
            ViewBag.project = projects;

            return View();
        }



    }
}