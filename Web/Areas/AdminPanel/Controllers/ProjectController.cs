using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Contexts;
using Web.Data.Models.Projects;
using Web.Data.ViewModels;

namespace Web.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
public class ProjectController : Controller
{
    private readonly DBContext _dbContext;

    public ProjectController(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("ProjectList")]
    public IActionResult ProjectList(int pageNumber = 1, int pagesize = 10)
    {
        var data = _dbContext.Projects
            .OrderByDescending(p => p.Id)
            .Skip((pageNumber - 1) * pagesize)
            .Take(pagesize)
            .ToList();

        var total = _dbContext.Projects.Count();

        var viewModel = new PagedListViewModel<Project>
        {
            Items = data,
            PageSize = pagesize,
            PageNumber = pageNumber,
            TotalItem = total
        };

        return View(viewModel);
    }

    [HttpGet("CreateProject")]
    public IActionResult CreateProject()
    {
        return View();
    }

    [HttpPost("CreateProject")]
    public IActionResult CreateProject(CreateProjectViewModel model, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            Project project = new Project();

            if (imageFile != null)
            {
                var imageName = DateTime.Now.ToString("MMddyyyyhhmmss") + Path.GetExtension(imageFile.FileName);

                string OriginPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + imageName);

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath))
                        imageFile.CopyTo(stream);
                }

                project.ImageName = imageName;
            }
            else
            {
                project.ImageName = "noimage.png";
            }

            project.Title = model.Title;
            project.Text = model.Text;

            try
            {
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(ProjectList));
        }

        return View();
    }

    [HttpGet("EditProject/{id}")]
    public IActionResult EditProject(int id)
    {
        return View();
    }

    [HttpGet("DeleteProject/{id}")]
    public IActionResult DeleteProject(int id)
    {
        return View();
    }

}
