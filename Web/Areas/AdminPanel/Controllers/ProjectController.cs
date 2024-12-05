
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
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        return View(project);
    }

    [HttpPost("EditProject/{id}")]
    public IActionResult EditProject(int id,Project model, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

            if (imageFile != null)
            {
                var imageName = DateTime.Now.ToString("MMddyyyyhhmmss") + Path.GetExtension(imageFile.FileName);

                string OriginPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + imageName);

                if (!string.IsNullOrEmpty(project.ImageName))
                {
                    string OldOriginPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + project.ImageName);

                    if (System.IO.File.Exists(OldOriginPath))
                        System.IO.File.Delete(OldOriginPath);
                }

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath))
                        imageFile.CopyTo(stream);
                }

                project.ImageName = imageName;
            }
            else
            {
                project.ImageName = model.ImageName;
            }

            project.Title = model.Title;
            project.Text = model.Text;

            try
            {
                _dbContext.Projects.Update(project);
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

    [HttpGet("DeleteProject/{id}")]
    public IActionResult DeleteProject(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        return View(project);
    }

    [HttpPost("DeleteProject/{id}")]
    public IActionResult DeleteProject(int id,string title)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        project.IsDeleted = true;

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return RedirectToAction("ProjectList");
    }

    [HttpGet("DeleteProject2/{id}")]
    public IActionResult DeleteProject2(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        return View(project);
    }

    [HttpPost("DeleteProject2/{id}")]
    public IActionResult DeleteProject2(int id, string title)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        if (!string.IsNullOrEmpty(project.ImageName))
        {
            string OldOriginPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + project.ImageName);

            if (System.IO.File.Exists(OldOriginPath))
                System.IO.File.Delete(OldOriginPath);
        }

        _dbContext.Projects.Remove(project);
        _dbContext.SaveChanges();

        return RedirectToAction("ProjectList");
    }

    [HttpGet("RecoveryProject/{id}")]
    public IActionResult RecoveryProject(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        return View(project);
    }

    [HttpPost("RecoveryProject/{id}")]
    public IActionResult RecoveryProject(int id, string title)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return RedirectToAction("ProjectList");

        project.IsDeleted = false;

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return RedirectToAction("ProjectList");
    }


}
