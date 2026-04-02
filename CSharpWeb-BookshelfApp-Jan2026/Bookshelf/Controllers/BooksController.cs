namespace Bookshelf.Controllers
{
    using Bookshelf.Data;
    using Bookshelf.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class BooksController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Book> books = dbContext.Books
                .Include(b => b.Author)
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Year)
                .ToList();

            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Author>? authors = dbContext.Authors
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Country)
                .ThenBy(a => a.Id)
                .ToList();

            ViewBag.Authors = authors;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            dbContext.Add(book);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
