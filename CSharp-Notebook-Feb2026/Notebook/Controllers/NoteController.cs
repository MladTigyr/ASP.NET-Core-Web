namespace Notebook.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Notebook.Services.Interfaces;
    using Notebook.ViewModels.Note;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class NoteController : Controller
    {
        private readonly INoteService noteService;
        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> MyNotes()
        {
            string userId = GetUserId();

            IEnumerable<AllNotesViewModel> notes = await noteService
                .AllNotesViewModelOrderedByCreationAndNameAsync(userId);

            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateNoteInputModel model = new CreateNoteInputModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await noteService.CreateNoteToDbWithInputModelAndUserIdParamsAsync(model, GetUserId());

                return RedirectToAction(nameof(MyNotes));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the note. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            EditNoteInputModel? model = await noteService
                .GetEditNoteFromDbWithIdParamAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditNoteInputModel model)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await noteService.CheckIfNoteExistWithIdParamAsync(id) == false)
            {
                return NotFound();
            }

            try
            {
                await noteService.EditNoteInDbWithIdAndEditNoteModelParamsAsync(id, model);

                return RedirectToAction(nameof(MyNotes));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the note. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            DeleteNoteInputModel? model = await noteService
                .GetDeleteInputModelWithIdParamAsync(id);

            if (model == null)
            { 
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteNoteInputModel model)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (await noteService.CheckIfNoteExistWithIdParamAsync(id) == false)
            {
                return NotFound();
            }

            try
            {
                await noteService.DeleteNoteFromDbWithIdParamAsync(id);

                return RedirectToAction(nameof(MyNotes));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the note. Please try again.");
                return View(model);
            }
        }

        private string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
