namespace Bookshelf.Controllers
{
    using Bookshelf.Data;
    using Bookshelf.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AuthorsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Author> authors = dbContext.Authors
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Country)
                .ThenBy(a => a.Id)
                .ToList();

            return View(authors);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Author? author = dbContext.Authors
                .Include(a => a.Books)
                .SingleOrDefault(a => a.Id == id);

            if (author == null)
            {
                return BadRequest();
            }

            return View(author);
        }
    }
}
