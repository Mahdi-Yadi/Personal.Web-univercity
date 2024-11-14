using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Contexts;
using Web.Data.Models.Setting;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SiteController : Controller
    {

        private readonly DBContext _dbContext;

        public SiteController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("site-setting")]
        public IActionResult SiteSetting()
        {
            var site = _dbContext.Sites.FirstOrDefault(s => s.IsActive == true);

            if (site == null)
            {
                Site newSite = new Site();

                newSite.Name = "سایت من ";
                newSite.ImageNameLogo = "";
                newSite.About = "";
                newSite.Bio = "";
                newSite.Email = "";
                newSite.Phone = "";
                newSite.Address = "";
                newSite.IsActive = true;


                _dbContext.Sites.Add(newSite);
                _dbContext.SaveChanges();

                return View(newSite);
            }

            return View(site);
        }

        [HttpPost("site-setting")]
        public IActionResult SiteSetting(Site site, IFormFile imagefile)
        {

            if (ModelState.IsValid)
            {
                var oldSite = _dbContext.Sites.FirstOrDefault(s => s.Id == site.Id);

                if (oldSite != null)
                {
                    oldSite.About = site.About;
                    oldSite.Name = site.Name;
                    oldSite.Bio = site.Bio;
                    oldSite.Address = site.Address;
                    oldSite.Phone = site.Phone;
                    oldSite.Email = site.Email;

                    if (imagefile != null)
                    {
                        var imageName = DateTime.Now.ToString("MMddyyyyhhmmss") + Path.GetExtension(imagefile.FileName);

                        string OriginPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/" + imageName);

                        using (var stream = new FileStream(OriginPath, FileMode.Create))
                        {
                            if (!Directory.Exists(OriginPath)) 
                                imagefile.CopyTo(stream);
                        }

                        if (oldSite.ImageNameLogo != null)
                        {
                            string OldOriginPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");

                            if (System.IO.File.Exists(OldOriginPath + oldSite.ImageNameLogo))
                                System.IO.File.Delete(OldOriginPath + oldSite.ImageNameLogo);
                        }

                        oldSite.ImageNameLogo = imageName;
                    }

                    _dbContext.Sites.Update(oldSite);
                    _dbContext.SaveChanges();
                    return Redirect("/");
                }
            }

            return View(site);
        }

    }
}