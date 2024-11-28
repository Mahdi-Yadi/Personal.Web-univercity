using Microsoft.AspNetCore.Mvc;
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
            return View();
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
