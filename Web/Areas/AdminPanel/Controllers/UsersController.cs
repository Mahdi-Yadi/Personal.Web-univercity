using Microsoft.AspNetCore.Mvc;
using Web.Codes;
using Web.Data.Contexts;
using Web.Data.Models.Account;
using Web.Data.ViewModels;

namespace Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UsersController : Controller
    {
        private readonly DBContext _dbContext;

        public UsersController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("UsersList")]
        public IActionResult UsersList(int pageNumber = 1, int pagesize = 10)
        {
            var data = _dbContext.Users
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pagesize)
                .Take(pagesize)
                .ToList();

            var total = _dbContext.Users.Count();

            var viewModel = new PagedListViewModel<User>
            {
                Items = data,
                PageSize = pagesize,
                PageNumber = pageNumber,
                TotalItem = total
            };

            return View(viewModel);
        }

        [HttpGet("EditUser/{id}")]
        public IActionResult EditUser(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(p => p.Id == id);

            if (user == null)
            {
                return RedirectToAction(nameof(UsersList));
            }

            return View(user);
        }

        [HttpPost("EditUser/{id}")]
        public IActionResult EditUser(int id,User user)
        {
            var olduser = _dbContext.Users.SingleOrDefault(p => p.Id == id);

            if (user == null)
            {
                return RedirectToAction(nameof(UsersList));
            }

            olduser.Email = user.Email;
            olduser.IsAdmin = user.IsAdmin;
            olduser.UserName = user.UserName;
            olduser.Password = Hashing.EncodePasswordMd5(user.Password);

            _dbContext.Users.Update(olduser);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(UsersList));
        }


        [HttpGet("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            return View();
        }

        [HttpGet("RecoveryProject/{id}")]
        public IActionResult RecoveryProject(int id)
        {
            return View();
        }

    }
}
